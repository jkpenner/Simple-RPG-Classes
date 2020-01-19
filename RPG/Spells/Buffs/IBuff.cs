using System;
using RPG.Actors;
using RPG.Actors.Stats;

namespace RPG.Spells {
    public interface IBuff {
        void OnApplied(Actor target);
        void OnRemoved(Actor target);
    }

    public class Buff : IBuff, IStatModifier {
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

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TwoDPoint);
        }

        public bool Equals(Buff other)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return this.Id == other.Id &&;

            return (X == p.X) && (Y == p.Y);
        }
    }
}