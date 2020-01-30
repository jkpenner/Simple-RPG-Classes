namespace RPG.Actors.Stats {
    // Defines the order of which each mod is applied. the
    // defined values are also used as the mod's key.
    public static class StatModOrder {
        // Sets the stats value to highest 'SetMod' mod value.
        // This will override all previous mods.
        public const int SetMod                     = 1;

        // Finds the max value from all mods of type
        // 'NonStackingFlatMod'. this value will override
        // the result of 'FlatMod' mods, if this value
        // is greater.
        public const int NonStackingFlatMod         = 2;

        // Totals all mods of type 'FlatMod' then applies
        // that value to the stat's base value.
        public const int FlatMod                    = 3;

        // Finds the max value from all mods of type
        // 'NonStackingPercentMod'. this value will override
        // the result of 'PercentMod' mods, if this value
        // is greater.
        public const int NonStackingPercentMod      = 4;

        // Totals all mods of type 'PercentMod' then applies
        // that value to the stat's value plus 'Add' mods.
        public const int PercentMod                 = 5;

        // Limits the final result to above the highest
        // applied 'MinLimitMod' mod.
        public const int MinLimitMod                = 6;

        // Limits the final result to below the lowest
        // applied 'MaxLimitMod' mod.
        public const int MaxLimitMod                = 7;
    }
}
