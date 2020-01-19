namespace RPG.Actors.Stats {
    public struct StatMod {
        public readonly StatKeys Stat;
        public readonly float Amount;
        public readonly StatModType Type;
        
        public StatMod(StatModType type, StatKeys stat, float amount) {
            this.Stat = stat;
            this.Type = type;
            this.Amount = amount;
        }
    }
}