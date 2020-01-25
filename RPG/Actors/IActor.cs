using RPG.Actors.Leveling;
using RPG.Actors.Stats;
using RPG.Actors.Resources;

namespace RPG.Actors {
    public interface IActor {
        bool IsAlive { get; }
        ActorLevel Level { get; }
        ActorStats Stats { get; }
        ActorResources Resources { get; }
    }
}