namespace RPG.Actors.Stats {
    public interface IStatModifier {
        void OnCalculateStats(StatModCollection mods);
    }
}