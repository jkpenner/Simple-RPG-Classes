namespace RPG.Actors.Stats {
    public class NonStackingPercentMod : StatModAsset {
        public int Order => StatModOrder.NonStackingPercentMod;
        public float Amount { get; private set; }
        
        public NonStackingPercentMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new NonStackingPercentModApplier();
        }
    }

    public class NonStackingPercentModApplier : AbstractStatModApplier<NonStackingPercentMod>, IStatModPreApply, IStatModSkipValue {
        public float Amount { get; private set; }
        public int SkipValue { get; private set; }
        public bool IsValueApplied { get; private set; }
        
        public NonStackingPercentModApplier() : base(StatModOrder.NonStackingPercentMod) {}
        
        public override float Apply(float statValue) {
            if (IsValueApplied)
                return statValue + (statValue * this.Amount);	
            return statValue;
        }
        
        public override void Combine(NonStackingPercentMod other) {
            this.Amount = Math.Max(other.Amount, this.Amount);
        }
            
        public override void Clear() {
            this.Amount = 0f;	
        }
        
        public void OnPreApply(StatModCollection mods) {
            var percentMod = mods.GetMod<PercentMod>(StatModOrder.PercentMod);
            if (percentMod != null) {
                this.IsValueApplied = percentMod.Amount <= this.Amount;
            } else {
                this.IsValueApplied = true;	
            }
            
            this.SkipValue = this.IsValueApplied ? StatModOrder.PercentMod : 0;
        }
    }
}