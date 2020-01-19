using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatCollection {
        private Dictionary<StatKeys, float> _stats;

        public StatCollection() {
            this._stats = new Dictionary<StatKeys, float>();
        }

        public bool IsSet(StatKeys key) {
            return this._stats.ContainsKey(key);
        }

        public void Set(StatKeys key, float value) {
            this._stats[key] = value;
        }

        public float Get(StatKeys key) {
            if (this.IsSet(key)) {
                return this._stats[key];
            }
            return 0f;
        }

        public void Clear() {
            this._stats.Clear();
        }
    }
}