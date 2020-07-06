﻿using System;

namespace Radomiej.JavityBus.ResolverQueue
{
    public class ResolveEvent
    {
        public readonly Type EventTypeToResolve;

        public ResolveEvent(Type eventTypeToResolve)
        {
            this.EventTypeToResolve = eventTypeToResolve;
        }
    }
}