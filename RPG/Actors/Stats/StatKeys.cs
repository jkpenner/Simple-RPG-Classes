namespace RPG.Actors.Stats {
    public enum StatKeys {
        None    = 0,

        [RequiredStat] Health  = 1,
        [RequiredStat] Mana    = 2,

        [RequiredStat] Strength    = 10,
        [RequiredStat] Stamina     = 11,
        [RequiredStat] Wisdom      = 12,
        [RequiredStat] Speed       = 13,

        ExpBonus    = 20,
    }
}