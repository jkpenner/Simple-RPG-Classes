using System;
using System.Collections.Generic;

namespace RPG.Actors.Resources {
    public class ResourceCollection
    {
        private Dictionary<ResourceKeys, float> resources;
        private Dictionary<ResourceKeys, Action<float>> events;

        public ResourceCollection() {
            this.resources = new Dictionary<ResourceKeys, float>();
            this.events = new Dictionary<ResourceKeys, Action<float>>();
        }

        public void Set(ResourceKeys key, float value)
        {
            bool didChange = true;
            if (resources.ContainsKey(key))
            {
                if (value == resources[key])
                {
                    didChange = false;
                }
            }

            resources[key] = value;
            if (didChange && events.ContainsKey(key))
            {
                events[key]?.Invoke(value);
            }
        }

        public float Get(ResourceKeys key)
        {
            if (this.IsSet(key))
            {
                return this.resources[key];
            }
            return 0f;
        }

        public bool IsSet(ResourceKeys key)
        {
            return this.resources.ContainsKey(key);
        }

        public void AddListener(ResourceKeys key, Action<float> callback)
        {
            if (this.events.ContainsKey(key))
            {
                this.events[key] += callback;
            }
            else
            {
                this.events[key] = callback;
            }
        }

        public void RemoveListener(ResourceKeys key, Action<float> callback)
        {
            if (this.events.ContainsKey(key))
            {
                this.events[key] -= callback;

                if (this.events[key] == null)
                    this.events.Remove(key);
            }
        }
    }
}