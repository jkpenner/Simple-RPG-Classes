using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatTemplate : IStatTemplate { // : ScriptableObject, IStatTemplate
        private Dictionary<StatAsset, StatContext> _contexts;

        public StatTemplate() {
            this._contexts = new  Dictionary<StatAsset, StatContext>();
        }

        public void AddContext(StatContext context) {
            this._contexts.Add(context.stat, context);
        }

        public void CalculateStats(ref StatCollection stats, int level, StatModCollection mods) {
            foreach(var key in this._contexts.Keys) {
                CalculateStat(ref stats, key, level, mods);
            }
        }

        private void CalculateStat(ref StatCollection stats, StatAsset key, int level, StatModCollection mods) {
            if (stats.IsSet(key))
                return;

            if (this._contexts.TryGetValue(key, out StatContext context)) {
                var baseValue = this._contexts[key].GetValue(level);
                
                if (context.links != null) {
                    for (var i = 0; i < context.links.Count; i++) {
                        var link = context.links[i];

                        if (stats.IsSet(link.stat) == false)
                            CalculateStat(ref stats, link.stat, level, mods);

                        baseValue += stats.Get(link.stat) * link.percent;
                    }
                }

                // Apply all mods to stat
                stats.Set(context.stat, mods.GetStatValue(context.stat, baseValue));
            } else {
                Console.WriteLine("Stat {0} not found in template, using default value", key.ToString());
                stats.Set(key, 0f);
            }
        }
    }
}