using System;
using System.Collections.Generic;
using RPG.Actors.Stats;

namespace RPG.Actors.Resources
{
    public class ResourceTemplate : IResourceTemplate{
        private Dictionary<ResourceKeys, ResourceContext> contextMap;

        public ResourceTemplate() {
            this.contextMap = new Dictionary<ResourceKeys, ResourceContext>();
        }

        public void SetContext(ResourceContext context) {
            this.contextMap.Add(context.resource, context);
        }

        public float GetDefaultValue(IActor actor, ResourceKeys key)
        {
            if (this.contextMap.ContainsKey(key) == false)
                return 0f;

            return GetDefault(actor, contextMap[key]);
        }

        public float GetClampedValue(IActor actor, ResourceKeys key, float value)
        {
            if (this.contextMap.ContainsKey(key) == false)
                return value;

            return ApplyLimits(actor, contextMap[key], value);
        }

        public void ApplyLimits(IActor actor, ResourceCollection resources)
        {
            foreach (var context in this.contextMap.Values)
            {
                resources.Set(context.resource, ApplyLimits(actor, context, resources.Get(context.resource)));
            }
        }

        public void ApplyDefaults(IActor actor, ResourceCollection resources) {
            foreach (var context in this.contextMap.Values)
            {
                resources.Set(context.resource, GetDefault(actor, context));
            }
        }

        private float GetDefault(IActor actor, ResourceContext context)
        {
            float? value = context.defaultValue.GetValue(actor);
            if (value.HasValue)
                return value.Value;
            else
                return 0f;
        }

        private float ApplyLimits(IActor actor, ResourceContext context, float value)
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