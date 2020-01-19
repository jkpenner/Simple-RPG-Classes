using System;
using System.Collections.Generic;

namespace RPG.Actors.Resources {
    public class ActorResources {
        private Dictionary<ResourceKeys, float> _resources;

        public float Health {
            get { return Get(ResourceKeys.Health); }
            set { Set(ResourceKeys.Health, value); }
        }

        public float Mana {
            get { return Get(ResourceKeys.Mana); }
            set { Set(ResourceKeys.Mana, value); }
        }

        public float Speed {
            get { return Get(ResourceKeys.Speed); }
            set { Set(ResourceKeys.Speed, value); }
        }

        public ActorResources() {
            this._resources = new Dictionary<ResourceKeys, float>();
        }

        public void Set(ResourceKeys key, float value) {
            this._resources[key] = value;
        }

        public float Get(ResourceKeys key) {
            if (this._resources.ContainsKey(key)) {
                return this._resources[key];
            }
            return 0f;
        }

        public void Clear() {
            this._resources.Clear();
        }

        public void ClampBelow(ResourceKeys key, float value) {
            this.Set(key, Math.Clamp(this.Get(key), 0f, value));
        }
    }
}