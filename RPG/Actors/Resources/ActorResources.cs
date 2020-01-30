using System;
using System.Collections.Generic;

namespace RPG.Actors.Resources
{
    public class ActorResources
    {
        private ResourceCollection resources;
        private IResourceTemplate resourceTemplate;

        public event Action<ResourceAsset, float> OnResourceChanged;

        public ActorResources(IActor actor, IResourceTemplate template)
        {
            this.resources = new ResourceCollection();

            this.resourceTemplate = template;
            if (this.resourceTemplate != null)
            {
                this.resourceTemplate.ApplyDefaults(actor, this.resources);
            }
        }

        public float Get(ResourceAsset resource)
        {
            return this.resources.Get(resource);
        }

        public void Set(IActor actor, ResourceAsset resource, float value)
        {
            float newValue = value;
            if (this.resourceTemplate != null) {
                newValue = this.resourceTemplate
                    .GetClampedValue(actor, resource, value);
                
            }

            if (newValue != this.resources.Get(resource))
            {
                this.resources.Set(resource, newValue);
                // Trigger event here
                OnResourceChanged?.Invoke(resource, newValue);
            }
        }

        public void SetToDefault(IActor actor, ResourceAsset resource)
        {
            Set(actor, resource, this.resourceTemplate?.GetDefaultValue(actor, resource) ?? 0f);
        }

        public void SetDefaults(IActor actor)
        {
            this.resourceTemplate.ApplyDefaults(actor, this.resources);
        }

        public void ApplyLimits(IActor actor, ResourceAsset resource) {
            if (this.resourceTemplate == null)
                return;

            var newValue = this.resourceTemplate.GetClampedValue(
                actor, resource, Get(resource));

            Set(actor, resource, newValue);
        }

        public void ApplyLimitsAll(IActor actor)
        {
            this.resourceTemplate.ApplyLimits(actor, this.resources);
        }

        public void AddListener(ResourceAsset resource, Action<float> callback)
        {
            this.resources.GetResource(resource).OnChange += callback;
        }

        public void RemoveListener(ResourceAsset resource, Action<float> callback)
        {
            this.resources.GetResource(resource).OnChange -= callback;
        }
    }
}