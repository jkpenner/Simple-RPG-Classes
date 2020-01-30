namespace RPG.Actors.Stats
{
    public abstract class AbstractStatModApplier<T> : IStatModApplier where T : class, StatModAsset  {
        public int Order { get; private set; }
        
        public AbstractStatModApplier(int order) {
            this.Order = order;
        }
        
        public void Combine(StatModAsset other) {
            if (other == null || other.Order != this.Order)
                return;
            
            if (other is T statMod) {
                this.Combine(statMod);	
            }
        }
        
        public abstract void Combine(T other);
        public abstract float Apply(float statValue);
        public abstract void Clear();
    }
}