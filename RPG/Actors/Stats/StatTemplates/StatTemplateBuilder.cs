using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class StatTemplateBuilder {
        private Dictionary<StatKeys, StatContext> _contexts;

        public StatTemplateBuilder() {
            this._contexts = new Dictionary<StatKeys, StatContext>();
        }

        private StatContext Get(StatKeys key) {
            if(this._contexts.ContainsKey(key) == false) {
                this._contexts[key] = new StatContext();
            }
            return this._contexts[key];
        }

        public StatTemplateBuilder Set(StatKeys key, float value, float perLevel = 0) {
            var context = Get(key);
            context.stat = key;
            context.baseValue = value;
            context.valuePerLevel = perLevel;
            return this;
        }

        public StatTemplateBuilder Link(StatKeys key, StatKeys link, float percent) {
            var context = Get(key);
            context.links.Add(new StatLink(link, percent));
            return this;
        }

        public StatTemplate Build() {
            var template = new StatTemplate();
            foreach(var context in this._contexts.Values) {
                // Creates new copies of each context
                template.AddContext(context.Clone()); 
            }
            return template;
        }
    }
}