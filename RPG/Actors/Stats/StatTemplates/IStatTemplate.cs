namespace RPG.Actors.Stats {
    public interface IStatTemplate {
        void CalculateStats(ref StatCollection stats, int level, StatModCollection mods);
    }
}