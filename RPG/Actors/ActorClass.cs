using RPG.Actors.Stats;
using RPG.Actors.Leveling;
using RPG.Actors.Resources;

namespace RPG.Actors {
    // Use as scriptable object, single instance shared between actors
    public class ActorClass { // : ScriptableObject
            public readonly string Name;
            public readonly IStatTemplate StatTemplate;
            public readonly IExpTemplate ExpTemplate;
            public readonly IResourceTemplate ResourceTemplate;
            
            public ActorClass(string name, IStatTemplate stat, IResourceTemplate resource, IExpTemplate exp) {
                this.Name = name;
                this.StatTemplate = stat;
                this.ResourceTemplate = resource;
                this.ExpTemplate = exp;
            }
    }
}