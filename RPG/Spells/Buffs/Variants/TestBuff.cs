using System;
using RPG.Actors;
using RPG.Actors.Stats;
using Game;

namespace RPG.Spells {
    public class TestBuff : IBuff, IStatModifier {
        public int Stacks { get; private set; }
        public StatAsset TargetStat { get; private set; }

        public TestBuff() {
            TargetStat = Stats.Get(StatKeys.Health);
        }

        public void OnApplied(Actor target) {
            target.OnCalculateStats += OnCalculateStats;
            target.CalculateStats();
        }
        public void OnRemoved(Actor target) {
            target.OnCalculateStats -= OnCalculateStats;
            target.CalculateStats();
        }

        public void OnCalculateStats(StatModCollection mods) {
            //Console.WriteLine("Applyed Buff");
            mods.Apply(new StatMod(StatModType.Add, TargetStat, 10f));
        }
    }
}