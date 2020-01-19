using RPG.Actors;
using RPG.Actors.Stats;
using System.Collections.Generic;

namespace RPG.Spells {
    public class BuffCollection : IStatModifier {
        private Actor owner;
        private List<IBuff> buffs;

        public BuffCollection(Actor owner) {
            this.owner = owner;
            this.buffs = new List<IBuff>();
        }

        public void Apply(IBuff buff) {
            if (buff == null)
                return;

            var index = this.buffs.IndexOf(buff);
            if (index >= 0) {

            } else {
                this.buffs.Add(buff);
                buff.OnApplied(this.owner);
            }
        }

        public void Remove(IBuff buff) {
            if (buff == null)
                return;

            var index = this.buffs.IndexOf(buff);
            if (index >= 0) {
                this.buffs.RemoveAt(index);
                buff.OnRemoved(this.owner);
            }
        }
    }
}