using System;
using System.Collections.Generic;
using RPG.Actors.Stats;

namespace RPG.Actors.Resources
{
    public enum ResourceValueType
    {
        None, Value, Stat
    }

    public class ResourceValueContext
    {
        private ResourceValueType type;
        private float value;
        private StatKeys stat;

        public ResourceValueContext() {
            this.type = ResourceValueType.None;
        }

        public ResourceValueContext(float value) {
            this.type = ResourceValueType.Value;
            this.value = value;
        }

        public ResourceValueContext(StatKeys stat) {
            this.type = ResourceValueType.Stat;
            this.stat = stat;
        }

        public float? GetValue(IActor actor)
        {
            switch (type)
            {
                case ResourceValueType.Stat:
                    //Console.WriteLine("(Stat): Getting Value of: {0}", actor.Stats.Get(this.stat));
                    return actor.Stats.Get(this.stat);
                case ResourceValueType.Value:
                    //Console.WriteLine("(Value): Getting Value of: {0}", value);
                    return value;
                default:
                    return null;
            }
        }
    }
}