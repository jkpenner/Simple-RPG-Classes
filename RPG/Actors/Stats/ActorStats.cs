namespace RPG.Actors.Stats {
    public class ActorStats { // : MonoBehavour {
        private StatCollection stats;
        private StatModCollection mods;
        private IStatTemplate statTemplate;

        public float Health     => this.stats.Get(StatKeys.Health);
        public float Mana       => this.stats.Get(StatKeys.Mana);

        public float Strength   => this.stats.Get(StatKeys.Strength);
        public float Stamina    => this.stats.Get(StatKeys.Stamina);
        public float Wisdom     => this.stats.Get(StatKeys.Wisdom);
        public float Speed      => this.stats.Get(StatKeys.Speed);

        public delegate void StatModCollectionEvent(StatModCollection mods);
        public event StatModCollectionEvent OnCalculateStats;

        public delegate void StatEvent(ActorStats stats);
        public event StatEvent OnStatsChange;

        public ActorStats(IActor owner, IStatTemplate statTemplate) {
            this.statTemplate = statTemplate;

            this.stats = new StatCollection();
            this.mods = new StatModCollection();

            //this.CalculateStats(level);
        }

        public float Get(StatKeys stat) {
            return this.stats.Get(stat);
        }

        public void CalculateStats(int level) {
            if (this.statTemplate == null)
                return;

            this.stats.Clear();
            this.mods.Clear();

            if (this.OnCalculateStats != null)
                this.OnCalculateStats.Invoke(this.mods);

            this.statTemplate.CalculateStats(
                ref this.stats, level, this.mods);

            if (this.OnStatsChange != null)
                this.OnStatsChange.Invoke(this);
        }
    }
}