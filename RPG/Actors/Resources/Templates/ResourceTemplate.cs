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

        public float? GetValue(Actor actor)
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

    public class ResourceContext
    {
        public ResourceKeys resource;
        public ResourceValueContext defaultValue;
        public ResourceValueContext minimumValue;
        public ResourceValueContext maximumValue;
    }

    public class ResourceTemplate : IResourceTemplate{
        private Dictionary<ResourceKeys, ResourceContext> contextMap;

        public ResourceTemplate() {
            this.contextMap = new Dictionary<ResourceKeys, ResourceContext>();
        }

        public void SetContext(ResourceContext context) {
            this.contextMap.Add(context.resource, context);
        }

        public float GetDefault(Actor actor, ResourceKeys key)
        {
            if (this.contextMap.ContainsKey(key) == false)
                return 0f;

            return GetDefault(actor, contextMap[key]);
        }

        public float ApplyLimits(Actor actor, ResourceKeys key, float value)
        {
            if (this.contextMap.ContainsKey(key) == false)
                return value;

            return ApplyLimits(actor, contextMap[key], value);
        }

        public void ApplyLimits(Actor actor, ResourceCollection resources)
        {
            ApplyContexts(actor, resources, false, true);
        }

        public void ApplyDefaults(Actor actor, ResourceCollection resources) {
            ApplyContexts(actor, resources, true, false);
        }

        public void ApplyDefaultsAndLimits(Actor actor, ResourceCollection resources)
        {
            ApplyContexts(actor, resources, true, true);
        }

        private void ApplyContexts(Actor actor, ResourceCollection resources, bool setDefaults, bool applyLimits)
        {
            foreach (var context in this.contextMap.Values)
            {
                if (setDefaults)
                    resources.Set(context.resource, GetDefault(actor, context));

                if (applyLimits)
                    resources.Set(context.resource, ApplyLimits(actor, context, resources.Get(context.resource)));
            }
        }

        private float GetDefault(Actor actor, ResourceContext context)
        {
            float? value = context.defaultValue.GetValue(actor);
            if (value.HasValue)
                return value.Value;
            else
                return 0f;
        }

        private float ApplyLimits(Actor actor, ResourceContext context, float value)
        {
            float? min = context.minimumValue.GetValue(actor);
            float? max = context.maximumValue.GetValue(actor);

            float newValue = value;

            if (min.HasValue)
                newValue = Math.Max(newValue, min.Value);
            if (max.HasValue)
                newValue = Math.Min(newValue, max.Value);

            return newValue;
        }
    }
}