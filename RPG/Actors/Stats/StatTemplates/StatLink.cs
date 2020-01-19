namespace RPG.Actors.Stats {
    public struct StatLink {
        public StatKeys stat;
        public float percent;

        public StatLink(StatKeys stat, float percent) {
            this.stat = stat;
            this.percent = percent;
        }

        public StatLink(StatLink link) {
            this.stat = link.stat;
            this.percent = link.percent;
        }

        public StatLink Clone() {
            return new StatLink(this);
        }
    }
}