using RPG.Actors;
using RPG.Actors.Stats;
using System.Collections.Generic;

namespace RPG.Spells {
    public class BuffCollection {
        private List<IBuff> buffs;

        public Actor Owner { 
            get; private set; 
        }

        public IList<IBuff> Buffs { 
            get { return buffs.AsReadOnly(); } 
        }

        public delegate void BuffEvent(Actor actor, IBuff buff);
        public event BuffEvent OnBuffApplied;
        public event BuffEvent OnBuffRemoved;
    
        public BuffCollection(Actor owner) {
            this.Owner = owner;
            this.buffs = new List<IBuff>();
        }

        public void Apply(IBuff buff) {
            if (buff == null)
                return;

            var index = this.buffs.IndexOf(buff);
            if (index >= 0) {

            } else {
                this.buffs.Add(buff);
                buff.OnApplied(this.Owner);
            }
        }

        public void Remove(IBuff buff) {
            if (buff == null)
                return;

            var index = this.buffs.IndexOf(buff);
            if (index >= 0) {
                this.buffs.RemoveAt(index);
                buff.OnRemoved(this.Owner);
            }
        }
    }
}