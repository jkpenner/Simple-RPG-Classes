using System;
using RPG.Actors;
using RPG.Actors.Stats;

namespace RPG.Spells {
    public interface IBuff {
        void OnApplied(Actor target);
        void OnRemoved(Actor target);
    }
}