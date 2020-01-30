namespace RPG.Actors.Stats {
    public interface IStatModPreApply {
        void OnPreApply(StatModCollection mods);	
    }
}