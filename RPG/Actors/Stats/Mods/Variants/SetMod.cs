namespace RPG.Actors.Stats {
    public class SetMod : StatModAsset { // ScriptableObject
        public int Order => StatModOrder.SetMod;
        public float Amount { get; private set; }
        
        public SetMod(float amount) {
            this.Amount = amount;
        }
        
        public IStatModApplier GetModApplier() {
            return new SetModApplier();
        }
    }

    public class SetModApplier : AbstractStatModApplier<SetMod>, IStatModStopAfterApplied {
        public float Amount { get; private set; }
        public bool StopAfterApplied { get; private set; }
        
        public SetModApplier() : base(StatModOrder.SetMod) {
            this.StopAfterApplied = true;
        }
        
        public override float Apply(float statValue) {
            return this.Amount;
        }
        
        public override void Combine(SetMod other) {
            this.Amount = Math.Max(other.Amount, this.Amount);
        }
        
        public override void Clear() {
            this.Amount = 0f;	
        }
    }
}