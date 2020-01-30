using RPG.Actors.Leveling;
using RPG.Actors.Stats;
using RPG.Actors.Resources;

namespace RPG.Actors {
    public interface IActor {
        bool IsAlive { get; }
        
        float Get(StatAsset stat);

        float Get(ResourceAsset resource);
        void Set(ResourceAsset resource, float value);
    }
}