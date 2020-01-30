namespace RPG.Actors.Stats {
    public class NonStackingFlatMod : StatModAsset {
        public int Order => StatModOrder.NonStackingFlatMod;
        public float Amount { get; private set; }
        
        public NonStackingFlatMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new NonStackingFlatModApplier();
        }
    }

    public class NonStackingFlatModApplier : AbstractStatModApplier<NonStackingFlatMod>, IStatModPreApply, IStatModSkipValue {
        public float Amount { get; private set; }
        public int SkipValue { get; private set; }
        public bool IsValueApplied { get; private set; }
        
        public NonStackingFlatModApplier() : base(StatModOrder.NonStackingFlatMod) {}
        
        public override float Apply(float statValue) {
            if (IsValueApplied)
                return statValue + this.Amount;	
            return statValue;
        }
        
        public override void Combine(NonStackingFlatMod other) {
            this.Amount = Math.Max(other.Amount, this.Amount);
        }
            
        public override void Clear() {
            this.Amount = 0f;	
        }
        
        public void OnPreApply(StatModCollection mods) {
            var flatMod = mods.GetMod<FlatMod>(StatModOrder.FlatMod);
            if (flatMod != null) {
                this.IsValueApplied = flatMod.Amount <= this.Amount;
                
            } else {
                this.IsValueApplied = true;	
            }
            
            this.SkipValue = this.IsValueApplied ? StatModOrder.FlatMod : 0;
        }
    }
}