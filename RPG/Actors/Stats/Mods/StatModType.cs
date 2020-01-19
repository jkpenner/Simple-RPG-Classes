namespace RPG.Actors.Stats {
    public enum StatModType {
        // Totals all mods of type 'Add' then applies
        // that value to the stat's base value.
        Add,
        // Finds the max value from all mods of type
        // 'AddSingleMax'. this value will override
        // the result of 'Add' mods, if this value
        // is greater.
        AddSingleMax,
        // Totals all mods of type 'Multi' then applies
        // that value to the stat's value plus 'Add' mods.
        Multi,
        // Finds the max value from all mods of type
        // 'MultiSingleMax'. this value will override
        // the result of 'Multi' mods, if this value
        // is greater.
        MultiSingleMax,
        // Limits the final result to below the lowest
        // applied 'Limit' mod.
        Limit,
        // Sets the stats value to highest 'Set' mod value.
        // This will override all previous mods.
        Set,
    }
}