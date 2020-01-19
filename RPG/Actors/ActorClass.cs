using RPG.Actors.Stats;
using RPG.Actors.Leveling;

namespace RPG.Actors {
    // Use as scriptable object, single instance shared between actors
    public class ActorClass { // : ScriptableObject
            public readonly string Name;
            public readonly IStatTemplate StatTemplate;
            public readonly IExpTemplate ExpTemplate;
            
            public ActorClass(string name, IStatTemplate stat, IExpTemplate exp) {
                this.Name = name;
                this.StatTemplate = stat;
                this.ExpTemplate = exp;
            }
    }
}