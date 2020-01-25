using System;
using RPG.Items;
using RPG.Actors.Stats;
using RPG.Actors.Leveling;
using RPG.Actors.Resources;
using RPG.Spells;

namespace RPG.Actors {
    public class Actor : IActor { // : MonoBehaviour {
        private ActorLevel _level;
        private ActorStats _stats;
        private ActorResources _resources;

        public string Name { get; private set; }
        public bool IsAlive { get; private set; }
        public ActorClass Class { get; private set; }
        
        public ActorLevel Level { 
            get {
                if (this._level == null) {
                    this._level = new ActorLevel(this, Class?.ExpTemplate);
                }
                return this._level;
            }
        }

        public ActorStats Stats { 
            get {
                if (this._stats == null) {
                    this._stats = new ActorStats(this, Class?.StatTemplate);
                }
                return this._stats;
            }
        }

        public ActorResources Resources { 
            get {
                if (this._resources == null) {
                    this._resources = new ActorResources(this, Class?.ResourceTemplate);
                }
                return this._resources;
            }
        }
        
        // This will be replace with assigning values in the inspector
        // once imported in unity.
        public Actor(string name, int level, ActorClass actorClass)
        {
            this.Name = name;

            // Assign actor's class
            this.Class = actorClass;

            // Actor is defaultly alive.
            this.IsAlive = true;

            // Test Methods
            this.Awake();
            this.OnEnable();
        }

        // Unity Method
        private void Awake() {
            // Set the actor's level
            this.Level.SetLevel(1);
        }

        // Unity Method
        private void OnEnable() {
            // Start listening to all required events
            this.Level.LevelChangeEvent         += OnLevelChange;
            this.Stats.OnStatsChange            += OnStatsChanged;
            this.Resources.OnResourceChanged    += OnResourceChanged;

            // Trigger level change with current level
            this.OnLevelChange(this.Level);
        }

        // Unity Method
        private void OnDisable() {
            // Stop listening to all required events
            this.Level.LevelChangeEvent         -= OnLevelChange;
            this.Stats.OnStatsChange            -= OnStatsChanged;
            this.Resources.OnResourceChanged    -= OnResourceChanged;
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