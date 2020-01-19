using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatContext {
        public StatKeys stat;
        public float baseValue;
        public float valuePerLevel;
        public List<StatLink> links;

        public StatContext() {
            this.stat = StatKeys.None;
            this.baseValue = 0;
            this.valuePerLevel = 0;
            this.links = new List<StatLink>();
        }

        public StatContext(StatContext context) {
            this.stat = context.stat;
            this.baseValue = context.baseValue;
            this.valuePerLevel = context.valuePerLevel;
            this.links = new List<StatLink>();
            foreach(var link in context.links) {
                this.links.Add(new StatLink(link));
            }
        }

        public StatContext(StatKeys key, float baseValue, float perLevel, List<StatLink> links = null) {
            this.stat = key;
            this.baseValue = baseValue;
            this.valuePerLevel = perLevel;
            this.links = links;
            if (this.links == null) { // Maybe set to null if not used
                this.links = new List<StatLink>();
            }
        }

        public float GetValue(int level) {
            return this.baseValue + (System.Math.Max(level - 1, 0) * this.valuePerLevel);
        }

        public StatContext Clone() {
            return new StatContext(this);
        }
    }
}