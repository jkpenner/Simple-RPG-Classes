using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatModCollection {
        private Dictionary<StatModType, Dictionary<StatKeys, float>> _mods;
        
        public StatModCollection() {
            this._mods = new Dictionary<StatModType, Dictionary<StatKeys, float>>();
            foreach(StatModType type in Enum.GetValues(typeof(StatModType))) {
                this._mods[type] = new Dictionary<StatKeys, float>();	
            }
        }
        
        public void Apply(StatMod mod) {		
            var dict = this._mods[mod.Type];
            if (dict.ContainsKey(mod.Stat)) {	
                if (mod.Type == StatModType.Limit) {
                    dict[mod.Stat] = Math.Min(dict[mod.Stat], mod.Amount);	
                } else if (mod.Type == StatModType.Set 
                    || mod.Type == StatModType.AddSingleMax 
                    || mod.Type == StatModType.MultiSingleMax) {
                    dict[mod.Stat] = Math.Max(dict[mod.Stat], mod.Amount);
                } else {
                    dict[mod.Stat] += mod.Amount;	
                }
            } else {
                dict[mod.Stat] = mod.Amount;
            }
        }

        public void Apply(StatMod[] mods) {
            foreach(var mod in mods) {
                this.Apply(mod);
            }
        }
        
        public void Clear() {
            foreach(var modType in this._mods.Values) {
                modType.Clear();	
            }
        }
        
        public float GetStatValue(StatKeys stat, float baseValue) {
            var setMod = GetModValue(stat, StatModType.Set);
            if (setMod >= 0)
                return setMod;

            var limitMod = GetModValue(stat, StatModType.Limit);

            var multiValue = GetModValue(stat, StatModType.Multi);
            var multiSingleValue = GetModValue(stat, StatModType.MultiSingleMax);

            // MultiSignleMax mods override multiValue if value is higher
            if (multiSingleValue >= 0 && multiSingleValue > multiValue) {
                multiValue = multiSingleValue;
            }

            var addValue = GetModValue(stat, StatModType.Add);
            var addSingleValue = GetModValue(stat, StatModType.AddSingleMax);

            // AddSignleMax mods override addValue if value is higher
            if (addSingleValue >= 0 && addSingleValue > addValue) {
                addValue = addSingleValue;
            }

            var result = baseValue + addValue;
            result = result + (result * multiValue);

            if (limitMod >= 0 && result > limitMod) {
                return limitMod;
            } else {
                return result;
            }
        }
        
        public float GetModValue(StatKeys stat, StatModType type) {
            var dict = this._mods[type];
            if (dict.ContainsKey(stat)) {
                return dict[stat];	
            }
            
            // No value set, assign default value
            return GetDefaultValue(type);
        }
        
        public float GetDefaultValue(StatModType type) {
            switch(type) {
                case StatModType.Set:
                case StatModType.Limit:
                case StatModType.AddSingleMax:
                case StatModType.MultiSingleMax:
                    return -1;
                default:
                    return 0f;
            }
        }
    }
}