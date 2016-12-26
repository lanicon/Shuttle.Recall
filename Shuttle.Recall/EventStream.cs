using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Recall
{
    public class EventStream
    {
        private ICanSnapshot _canSnapshot;
        private readonly IEnumerable<object> _events;
        private readonly List<object> _newEvents = new List<object>();

        public EventStream(Guid id)
        {
            Id = id;
            Version = 0;
        }

        public EventStream(Guid id, int version, IEnumerable<object> events)
        {
            Guard.AgainstNull(events,"events");

            Id = id;
            Version = version;

            _events = events;
        }

        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public object Snapshot { get; private set; }
        public bool Removed { get; private set; }

        public EventStream Remove()
        {
            Removed = true;

            return this;
        }

        public int Count {
            get { return (_events == null ? 0 : _events.Count()) + _newEvents.Count; }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public EventStream AddEvent(object @event)
        {
            Guard.AgainstNull(@event, "@event");

            _newEvents.Add(@event);

            return this;
        }

        public EventStream AddSnapshot(object snapshot)
        {
            Guard.AgainstNull(snapshot, "snapshot");

            Snapshot = snapshot;

            return this;
        }

        public bool ShouldSnapshot(int snapshotEventCount)
        {
            return Count >= snapshotEventCount;
        }

        public bool AttemptSnapshot(int snapshotEventCount)
        {
            if (!CanSnapshot || !ShouldSnapshot(snapshotEventCount))
            {
                return false;
            }

            AddSnapshot(_canSnapshot.GetSnapshotEvent());

            return true;
        }

        public bool CanSnapshot
        {
            get { return _canSnapshot != null; }
        }

        public void Apply(object instance)
        {
            Apply(instance, "On");
        }

        public void Apply(object instance, string eventHandlingMethodName)
        {
            Guard.AgainstNull(instance, "instance");

            if (_events == null)
            {
                return;
            }

            _canSnapshot = instance as ICanSnapshot;

            var instanceType = instance.GetType();

            foreach (var @event in _events)
            {
                var method = instanceType.GetMethod(eventHandlingMethodName, new[] { @event.GetType() });

                if (method == null)
                {
                    throw new UnhandledEventException(string.Format(RecallResources.UnhandledEventException,
                        instanceType.AssemblyQualifiedName, eventHandlingMethodName, @event.GetType().AssemblyQualifiedName));
                }

                method.Invoke(instance, new[] { @event });
            }
        }

        public bool HasSnapshot
        {
            get { return Snapshot != null; }
        }

        public void ConcurrencyInvariant(int expectedVersion)
        {
            if (expectedVersion != Version)
            {
                throw new EventStreamConcurrencyException(string.Format(RecallResources.EventStreamConcurrencyException, Id, Version, expectedVersion));
            }
        }
    }
}