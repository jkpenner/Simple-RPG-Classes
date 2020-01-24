using System.Collections.Generic;

namespace RPG.Actors.Resources {
    public class ResourceCollection {
        private Dictionary<ResourceKeys, Resource> _resources;

        public ResourceCollection() {
            this._resources = new Dictionary<ResourceKeys, Resource>();
        }

        public void Register(ResourceKeys key) {
            this._resources[key] = new Resource();
        }

        public void UnRegister(ResourceKeys key) {
            this._resources[key].Clear();
            this._resources.RemoveKey(key);
        }
    }
}