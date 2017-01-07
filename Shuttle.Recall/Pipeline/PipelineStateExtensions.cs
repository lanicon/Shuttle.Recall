﻿using System;
using System.Collections.Generic;
using System.IO;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Recall
{
    public static class PipelineStateExtensions
    {
        public static IThreadState GetThreadState(this IState state)
        {
            return state.Get<IThreadState>(StateKeys.ThreadState);
        }

        public static void SetThreadState(this IState state, IThreadState activeState)
        {
            state.Replace(StateKeys.ThreadState, activeState);
        }

        public static void SetWorking(this IState state)
        {
            state.Replace(StateKeys.Working, true);
        }

        public static bool GetWorking(this IState state)
        {
            return state.Get<bool>(StateKeys.Working);
        }

        public static void SetEventEnvelope(this IState state, EventEnvelope value)
        {
            state.Replace(StateKeys.EventEnvelope, value);
        }

        public static EventEnvelope GetEventEnvelope(this IState state)
        {
            return state.Get<EventEnvelope>(StateKeys.EventEnvelope);
        }

        public static void SetEventEnvelopes(this IState state, IEnumerable<EventEnvelope> value)
        {
            state.Replace(StateKeys.EventEnvelopes, value);
        }

        public static IEnumerable<EventEnvelope> GetEventEnvelopes(this IState state)
        {
            return state.Get<IEnumerable<EventEnvelope>>(StateKeys.EventEnvelopes);
        }

        public static void SetEvent(this IState state, object value)
        {
            state.Replace(StateKeys.Event, value);
        }

        public static object GetEvent(this IState state)
        {
            return state.Get<object>(StateKeys.Event);
        }

        public static void SetEventStream(this IState state, EventStream value)
        {
            state.Replace(StateKeys.EventStream, value);
        }

        public static EventStream GetEventStream(this IState state)
        {
            return state.Get<EventStream>(StateKeys.EventStream);
        }

        public static void SetEventEnvelopeConfigurator(this IState state, EventEnvelopeConfigurator value)
        {
            state.Replace(StateKeys.EventEnvelopeConfigurator, value);
        }

        public static EventEnvelopeConfigurator GetEventEnvelopeConfigurator(this IState state)
        {
            return state.Get<EventEnvelopeConfigurator>(StateKeys.EventEnvelopeConfigurator);
        }

        public static void SetEvents(this IState state, IEnumerable<object> value)
        {
            state.Replace(StateKeys.Events, value);
        }

        public static IEnumerable<object> GetEvents(this IState state)
        {
            return state.Get<IEnumerable<object>>(StateKeys.Events);
        }

        public static void SetVersion(this IState state, int value)
        {
            state.Replace(StateKeys.Version, value);
        }

        public static int GetVersion(this IState state)
        {
            return state.Get<int>(StateKeys.Version);
        }

        public static byte[] GetEventBytes(this IState state)
        {
            return state.Get<byte[]>(StateKeys.EventBytes);
        }

        public static void SetEventBytes(this IState state, byte[] bytes)
        {
            state.Replace(StateKeys.EventBytes, bytes);
        }

        public static DomainEvent GetDomainEvent(this IState state)
        {
            return state.Get<DomainEvent>(StateKeys.DomainEvent);
        }

        public static void SetDomainEvent(this IState state, DomainEvent domainEvent)
        {
            state.Replace(StateKeys.DomainEvent, domainEvent);
        }

        public static void SetPrimitiveEvent(this IState state, PrimitiveEvent primitiveEvent)
        {
            state.Replace(StateKeys.PrimitiveEvent, primitiveEvent);
        }

        public static PrimitiveEvent GetPrimitiveEvent(this IState state)
        {
            return state.Get<PrimitiveEvent>(StateKeys.PrimitiveEvent);
        }

        public static EventProjection GetEventProjection(this IState state)
        {
            return state.Get<EventProjection>(StateKeys.EventProjection);
        }

        public static void SetEventProjection(this IState state, EventProjection eventProjection)
        {
            state.Replace(StateKeys.EventProjection, eventProjection);
        }

        public static Guid GetId(this IState state)
        {
            return state.Get<Guid>(StateKeys.Id);
        }

        public static void SetId(this IState state, Guid id)
        {
            state.Replace(StateKeys.Id, id);
        }
    }
}