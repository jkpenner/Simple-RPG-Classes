namespace RPG.Actors.Stats
{
    public interface IStatModApplier {
        int Order { get; }
        
        void Combine(StatModAsset other);
        float Apply(float statValue);
        void Clear();
    }
}