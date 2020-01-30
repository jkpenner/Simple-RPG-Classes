using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatModCollection {
        private HashSet<int> _skip;
        public Dictionary<int, IStatModApplier> _mods;
        
        public StatModCollection() {
            this._skip = new HashSet<int>();
            this._mods = new Dictionary<int, IStatModApplier>();	
        }
        
        public void Apply(StatModAsset mod) {
            if (mod == null)
                return;
            
            if (_mods.ContainsKey(mod.Order) == false)
                _mods[mod.Order] = mod.GetModApplier();	
            
            _mods[mod.Order].Combine(mod);
        }
        
        public void Clear(bool removeAppliers = false) {
            if (removeAppliers) {
                this._mods.Clear();	
            } else {
                foreach(var applier in this._mods.Values) {
                    applier.Clear();	
                }
            }
        }
        
        public float GetValue(float statValue) {
            var keys = new T[dict.Keys.Count];
            this._mods.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            foreach(var key in keys) {
                // Checks if the stat mod handles the pre apply event.
                if (_mods[key] is IStatModPreApply mod) {
                    mod.OnPreApply(this);
                }
            }
            
            this._skip.Clear();
            foreach(var key in keys) {
                var mod = this._mods[key];
            
                // Check if mod is skipped.
                if (this._skip.Contains(key))
                    continue;
                
                // Apply the mod to the stat's value
                statValue = mod.Apply(statValue);
                
                // Checks if the mod stops the apply
                if (mod is IStatModStopAfterApplied stop)
                {
                    if(stop.StopAfterApplied)
                        break;
                }
                
                // Check if the mod skips a value
                if (mod is IStatModSkipValue skip)
                {
                    if (skip.SkipValue >= 0)
                        this._skip.Add(skip.SkipValue);
                }
            }
            return statValue;
        }
        
        public T GetMod<T>(int key) where T : class, StatModAsset {
            if (_mods.TryGetValue(key, out var mod)) {
                return mod as T;
            }
            return null;
        }
    }
}