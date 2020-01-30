namespace RPG.Actors.Stats {
    public interface StatModAsset {
        int Order { get; }
        IStatModApplier GetModApplier();
    }
}