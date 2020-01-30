namespace RPG.Actors.Stats {
    public class MinLimitMod : StatModAsset { // ScriptableObject
        public int Order => StatModOrder.MinLimitMod;
        public float Amount { get; private set; }
        
        public MinLimitMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new MinLimitModApplier();
        }
    }

    public class MinLimitModApplier : AbstractStatModApplier<MinLimitMod> {
        public float? Amount { get; private set; }
        
        public MinLimitModApplier() : base(StatModOrder.MinLimitMod) {}
        
        public override float Apply(float statValue) {
            if (this.Amount.HasValue)
                return Math.Max(statValue, this.Amount.Value);
            return statValue;
        }
        
        public override void Combine(MinLimitMod other) {
            if (this.Amount.HasValue)			
                this.Amount = Math.Max(other.Amount, this.Amount.Value);
            else
                this.Amount = other.Amount;
        }
        
        public override void Clear() {
            this.Amount = 0f;	
        }
    }
}