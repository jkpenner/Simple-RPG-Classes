using System;
using RPG.Actors;
using RPG.Actors.Stats;

namespace RPG.Spells {
    public class TestBuff : IBuff, IStatModifier {
        public int Stacks { get; private set; }

        public void OnApplied(Actor target) {
            target.Stats.OnCalculateStats += OnCalculateStats;
            target.CalculateStats();
        }
        public void OnRemoved(Actor target) {
            target.Stats.OnCalculateStats -= OnCalculateStats;
            target.CalculateStats();
        }

        public void OnCalculateStats(StatModCollection mods) {
            //Console.WriteLine("Applyed Buff");
            mods.Apply(new StatMod(StatModType.Add, StatKeys.Stamina, 10f));
            //mods.Apply(new StatMod(StatModType.AddSingleMax, StatKeys.Health, 150f));
            //mods.Apply(new StatMod(StatModType.AddSingleMax, StatKeys.Health, 100f));
        }
    }
}