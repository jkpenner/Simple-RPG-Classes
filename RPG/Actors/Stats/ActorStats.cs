using System;
using System.Collections.Generic;

namespace RPG.Actors.Stats {
    public class CalculateStatsArgs {
        public Dictionary<StatAsset, StatModCollection> _mods;

        public CalculateStatsArgs(Dictionary<StatAsset, StatModCollection> mods) {
            this._mods = mods;
        }

        public void Apply(StatAsset stat, StatModAsset mod) {
            if (this._mods.ContainsKey(stat) == false) 
                this._mods[stat] = new StatModCollection();

            this._mods[stat].Apply(mod);
        }
    }

    public class ActorStats { // : MonoBehavour {
        public delegate void StatModEvent(CalculateStatsArgs mods);

        private IStatTemplate statTemplate;

        private StatCollection stats;
        //private StatModCollection mods;

        private Dictionary<StatAsset, StatModCollection> mods;

        private Action onStatsChange;
        private StatModEvent onPreCalculateStats;

        public event StatModEvent OnCalculateStats;

        public ActorStats(IStatTemplate statTemplate, Action onStatsChange, StatModEvent onPreCalculateStats = null) {
            this.statTemplate = statTemplate;

            this.stats = new StatCollection();
            this.mods = new Dictionary<StatAsset, StatModCollection>();//new StatModCollection();

            this.onStatsChange = onStatsChange;
            this.onPreCalculateStats = onPreCalculateStats;
        }

        public float Get(StatAsset stat) {
            return this.stats.Get(stat);
        }

        public void CalculateStats(int level) {
            if (this.statTemplate == null)
                return;

            this.stats.Clear();
            this.mods.Clear();

            InvokeCalculateStats(new CalculateStatsArgs(this.mods));

            this.statTemplate.CalculateStats(
                ref this.stats, level, this.mods);

            InvokeStatChange();
        }

        private void InvokeCalculateStats(CalculateStatsArgs mods) {
            if (this.onPreCalculateStats != null)
                this.onPreCalculateStats.Invoke(this.mods);

            if (this.OnCalculateStats != null)
                this.OnCalculateStats.Invoke(this.mods);
        }

        private void InvokeStatChange() {
            if (this.onStatsChange != null)
                this.onStatsChange.Invoke();
        }
    }
}