namespace RPG.Actors.Stats {
    public class PercentMod : StatModAsset { // ScriptableObject
        public int Order => StatModOrder.PercentMod;
        public float Amount { get; private set; }
        
        public PercentMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new PercentModApplier();
        }
    }

    public class PercentModApplier : AbstractStatModApplier<PercentMod> {
        public float Amount { get; private set; }
        
        public PercentModApplier() : base(StatModOrder.PercentMod) {}
        
        public override float Apply(float statValue) {
            return statValue + (statValue * this.Amount);	
        }
        
        public override void Combine(PercentMod other) {
            this.Amount += other.Amount;
        }
        
        public override void Clear() {
            this.Amount = 0f;	
        }
    }
}