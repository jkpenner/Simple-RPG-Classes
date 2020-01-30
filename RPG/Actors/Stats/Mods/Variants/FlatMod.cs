namespace RPG.Actors.Stats {
    public class FlatMod : StatModAsset { // ScriptableObject
        public int Order => StatModOrder.FlatMod;
        public float Amount { get; private set; }
        
        public FlatMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new FlatModApplier();
        }
    }

    public class FlatModApplier : AbstractStatModApplier<FlatMod> {
        public float Amount { get; private set; }
        
        public FlatModApplier() : base(StatModOrder.FlatMod) {}
        
        public override float Apply(float statValue) {
            return statValue + this.Amount;	
        }
        
        public override void Combine(FlatMod other) {
            this.Amount += other.Amount;
        }
        
        public override void Clear() {
            this.Amount = 0f;	
        }
    }
}