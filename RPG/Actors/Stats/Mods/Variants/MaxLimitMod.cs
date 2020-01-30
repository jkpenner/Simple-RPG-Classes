namespace RPG.Actors.Stats {
    public class MaxLimitMod : StatModAsset { // ScriptableObject
        public int Order => StatModOrder.MaxLimitMod;
        public float Amount { get; private set; }
        
        public MaxLimitMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new MaxLimitModApplier();
        }
    }

    public class MaxLimitModApplier : AbstractStatModApplier<MaxLimitMod> {
        public float? Amount { get; private set; }
        
        public MaxLimitModApplier() : base(StatModOrder.MaxLimitMod) {}
        
        public override float Apply(float statValue) {
            if (this.Amount.HasValue)
                return Math.Min(statValue, this.Amount.Value);
            return statValue;
        }
        
        public override void Combine(MaxLimitMod other) {
            if (this.Amount.HasValue)			
                this.Amount = Math.Min(other.Amount, this.Amount.Value);
            else
                this.Amount = other.Amount;
        }
        
        public override void Clear() {
            this.Amount = 0f;	
        }
    }
}