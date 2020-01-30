using System;
using System.Collections.Generic;

namespace RPG.Actors.Resources
{
    public class ResourceCollection
    {
        private Dictionary<ResourceAsset, Resource> resources;

        public ResourceCollection()
        {
            this.resources = new Dictionary<ResourceAsset, Resource>();
        }

        public void Set(ResourceAsset resource, float value)
        {
            this.GetResource(resource).Set(value);
        }

        public Resource GetResource(ResourceAsset resource)
        {
            if (this.resources.ContainsKey(resource) == false)
            {
                this.resources[resource] = new Resource();
            }
            return this.resources[resource];
        }

        public float Get(ResourceAsset resource)
        {
            return GetResource(resource).Amount;
        }
    }
}