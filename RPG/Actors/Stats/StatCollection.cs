using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatCollection {
        private Dictionary<StatAsset, float> _stats;

        public StatCollection() {
            this._stats = new Dictionary<StatAsset, float>();
        }

        public bool IsSet(StatAsset key) {
            return this._stats.ContainsKey(key);
        }

        public void Set(StatAsset key, float value) {
            this._stats[key] = value;
        }

        public float Get(StatAsset key) {
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