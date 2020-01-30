using System.Collections.Generic;
using RPG.Actors.Stats;
using RPG.Actors.Resources;

namespace Game {
    public enum StatKeys {
        Health, Mana, Strength, Stamina,
        Wisdom, Speed, ExpBonus
    }

    public static class Stats {
        private static Dictionary<StatKeys, StatAsset> stats;

        private static void Initialize() {
            stats = new Dictionary<StatKeys, StatAsset>();
            stats.Add(StatKeys.Health, new StatAsset("Health"));
            stats.Add(StatKeys.Mana, new StatAsset("Mana"));
            stats.Add(StatKeys.Strength, new StatAsset("Strength"));
            stats.Add(StatKeys.Stamina, new StatAsset("Stamina"));
            stats.Add(StatKeys.Wisdom, new StatAsset("Wisdom"));
            stats.Add(StatKeys.Speed, new StatAsset("Speed"));
            stats.Add(StatKeys.ExpBonus, new StatAsset("ExpBonus"));
        }

        public static StatAsset Get(StatKeys key) {
            if(stats == null)
                Initialize();

            return stats[key];
        }
    }

    public enum ResourceKeys {
        None,
        Health,
        Mana,
        Speed
    }

    public static class Resources {
        private static Dictionary<ResourceKeys, ResourceAsset> resources;

        private static void Initialize() {
            resources = new Dictionary<ResourceKeys, ResourceAsset>();
            resources.Add(ResourceKeys.Health,  new ResourceAsset("Health"));
            resources.Add(ResourceKeys.Mana,    new ResourceAsset("Mana"));
            resources.Add(ResourceKeys.Speed,   new ResourceAsset("Speed"));
        }

        public static ResourceAsset Get(ResourceKeys key) {
            if(resources == null)
                Initialize();

            return resources[key];
        }
    }
}