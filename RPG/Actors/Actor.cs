using System;
using RPG.Items;
using RPG.Actors.Stats;
using RPG.Actors.Leveling;
using RPG.Actors.Resources;
using RPG.Spells;

namespace RPG.Actors {
    public class Actor { // : MonoBehaviour {
        public readonly string Name;
        public readonly ActorClass Class;
        public readonly ActorLevel Level;
        public readonly ActorStats Stats;
        public readonly ActorResources Resources;

        public BuffCollection Buffs { get; private set; }
        public Inventory Inventory { get; private set; }
        public Equipment Equipment { get; private set; }
        
        public Actor(string name, int level, ActorClass actorClass)
        {
            this.Name = name;
                
            this.Class = actorClass;
            
            this.Level = new ActorLevel(level, Class.ExpTemplate);
            this.Level.LevelChangeEvent += OnLevelChange;
            
            this.Stats = new ActorStats(this.Class.StatTemplate, level);
            this.Stats.OnStatsChange += OnStatsChange;
            this.Stats.OnCalculateStats += OnCalculateStats;

            this.Resources = new ActorResources();
            this.Resources.Set(ResourceKeys.Health,   this.Stats.Health);
            this.Resources.Set(ResourceKeys.Mana,     this.Stats.Mana);
            this.Resources.Set(ResourceKeys.Speed,    this.Stats.Speed);

            this.Buffs = new BuffCollection(this);
            this.Inventory = new Inventory();     //    GetComponent<Inventory>();
            this.Equipment = new Equipment(this);
        }

        public void CalculateStats()
        {
            this.Stats.CalculateStats(this.Level.Current);
        }

        public void GiveExp(int amount)
        {
            var bonus = this.Stats.Get(StatKeys.ExpBonus);
            var expAmount = (int)Math.Round(amount + (amount * bonus));

            this.Level.GiveExp(Math.Max(expAmount, 0));
        }

        private void OnLevelChange(ActorLevel level)
        {
            this.Stats.CalculateStats(level.Current);

            this.Resources.Set(ResourceKeys.Health, this.Stats.Health);
            this.Resources.Set(ResourceKeys.Mana, this.Stats.Mana);
        }

        private void OnCalculateStats(StatModCollection mods)
        {
            this.Equipment.OnCalculateStats(mods);
            //this.Buffs.OnCalculateStats(mods);
        }
        
        private void OnStatsChange(ActorStats stats) 
        {
            this.Resources.ClampBelow(ResourceKeys.Health, this.Stats.Health);
            this.Resources.ClampBelow(ResourceKeys.Mana, this.Stats.Mana);
        }
    }
}