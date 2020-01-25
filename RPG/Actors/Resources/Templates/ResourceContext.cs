using System;
using System.Collections.Generic;
using RPG.Actors.Stats;

namespace RPG.Actors.Resources
{
    public class ResourceContext
    {
        public ResourceKeys resource;
        public ResourceValueContext defaultValue;
        public ResourceValueContext minimumValue;
        public ResourceValueContext maximumValue;
    }
}