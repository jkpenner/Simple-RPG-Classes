using System;
using System.Collections.Generic;

namespace RPG.Actors.Resources
{
    public class ActorResources
    {
        private Actor owner;
        private ResourceCollection resources;
        private IResourceTemplate resourceTemplate;

        public ActorResources(Actor actor)
        {
            this.owner = actor;
            this.resources = new ResourceCollection();
            this.resourceTemplate = actor.Class.ResourceTemplate;
            if (this.resourceTemplate != null)
            {
                //Console.WriteLine("{0}: Applying resource Template", this.owner.Name);
                this.resourceTemplate.ApplyDefaults(actor, this.resources);
            }
        }

        public event Action<ResourceKeys, float> OnResourceChanged;

        public float Get(ResourceKeys key)
        {
            return this.resources.Get(key);
        }

        public void Set(ResourceKeys key, float value)
        {
            float newValue = value;
            if (this.resourceTemplate != null) {
                newValue = this.resourceTemplate.ApplyLimits(this.owner, key, value);
                
            }

            if (newValue != this.resources.Get(key))
            {
                this.resources.Set(key, newValue);
                // Trigger event here
                OnResourceChanged?.Invoke(key, newValue);
            }
        }

        public void SetToDefault(ResourceKeys key)
        {
            Set(key, resourceTemplate?.GetDefault(owner, key) ?? 0f);
        }

        public void ApplyLimits() {
            this.resourceTemplate.ApplyLimits(owner, resources);
        }

        public void AddListener(ResourceKeys key, Action<float> callback)
        {
            resources.AddListener(key, callback);
        }

        public void RemoveListener(ResourceKeys key, Action<float> callback)
        {
            resources.RemoveListener(key, callback);
        }
    }
}