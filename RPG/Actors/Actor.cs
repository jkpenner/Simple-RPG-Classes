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

        public bool IsAlive { get; private set; }

        // Move out of actor class
        public BuffCollection Buffs { get; private set; }
        public Inventory Inventory { get; private set; }
        public Equipment Equipment { get; private set; }
        
        public Actor(string name, int level, ActorClass actorClass)
        {
            this.Name = name;
            this.Class = actorClass;
            this.IsAlive = true;
            
            this.Level = new ActorLevel(level, Class.ExpTemplate);
            this.Level.LevelChangeEvent += OnLevelChange;
            
            this.Stats = new ActorStats(this.Class.StatTemplate, level);
            this.Stats.OnStatsChange += OnStatsChanged;
            this.Stats.OnCalculateStats += OnCalculateStats;

            this.Resources = new ActorResources(this);
            this.Resources.OnResourceChanged += OnResourceChanged;

            // Move out of actor class
            this.Buffs = new BuffCollection(this);
            this.Inventory = new Inventory();     //    GetComponent<Inventory>();
            this.Equipment = new Equipment(this);
        }

        public void CalculateStats() {
            this.Stats.CalculateStats(this.Level.Current);
        }

        public void GiveExp(int amount) {
            var bonus = this.Stats.Get(StatKeys.ExpBonus);
            var expAmount = (int)Math.Round(amount + (amount * bonus));
            this.Level.GiveExp(Math.Max(expAmount, 0));
        }

        private void OnLevelChange(ActorLevel level)
        {
            this.Stats.CalculateStats(level.Current);

            // Set vital stats to their default values this
            // will restore the actor's health / mana on level up
            this.Resources.SetToDefault(ResourceKeys.Health);
            this.Resources.SetToDefault(ResourceKeys.Mana);
        }

        private void OnCalculateStats(StatModCollection mods)
        {
            // Should be moved out of the actor class
            this.Equipment.OnCalculateStats(mods);
            //this.Buffs.OnCalculateStats(mods);
        }
        
        private void OnStatsChanged(ActorStats stats) 
        {
            // Make sure all resources are correctly 
            // restricted when the stats values change.
            this.Resources.ApplyLimits();
        }

        private void OnResourceChanged(ResourceKeys key, float value) {
            if (key == ResourceKeys.Health && IsAlive && value <= 0f) {
                IsAlive = false;
            }
        }

        public void PrintStats() {
            Console.WriteLine("{0}: IsAlive: {5}, Health: {1}/{2}, Speed: {3}/{4}", this.Name,
                this.Resources.Get(ResourceKeys.Health), this.Stats.Get(StatKeys.Health),
                this.Resources.Get(ResourceKeys.Speed), this.Stats.Get(StatKeys.Speed), IsAlive);
        }
    }
}