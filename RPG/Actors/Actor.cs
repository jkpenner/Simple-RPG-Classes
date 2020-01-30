using System;
using RPG.Items;
using RPG.Actors.Stats;
using RPG.Actors.Leveling;
using RPG.Actors.Resources;
using RPG.Spells;
using Game; // ToDo: Remove

namespace RPG.Actors {
    public class Actor : IActor { // : MonoBehaviour {
        private ActorLevel _level;
        private ActorStats _stats;
        private ActorResources _resources;

        private StatAsset expBonusStat;
        private StatAsset healthStat;
        private StatAsset speedStat;

        public string Name { get; private set; }
        public bool IsAlive { get; private set; }
        public ActorClass Class { get; private set; }
        
        private ActorLevel Level { 
            get {
                if (this._level == null)
                    this._level = new ActorLevel(Class?.ExpTemplate, 0, this.OnLevelChange);
                return this._level;
            }
        }

        private ActorStats Stats { 
            get {
                if (this._stats == null)
                    this._stats = new ActorStats(Class?.StatTemplate, this.OnStatsChange, this.HandleCalculateStats);
                return this._stats;
            }
        }

        private ActorResources Resources { 
            get {
                if (this._resources == null)
                    this._resources = new ActorResources(this, Class?.ResourceTemplate);
                return this._resources;
            }
        }

        public event ActorStats.StatModEvent OnCalculateStats {
            add { this.Stats.OnCalculateStats += value; }
            remove { this.Stats.OnCalculateStats -= value; }
        }
        
        // This will be replace with assigning values in the inspector
        // once imported in unity.
        public Actor(string name, int level, ActorClass actorClass, StatAsset health, StatAsset speed, StatAsset expBonusStat)
        {
            this.Name = name;

            // Assign actor's class
            this.Class = actorClass;
            this.Level.SetLevel(level);

            this.Resources.AddListener(Game.Resources.Get(ResourceKeys.Health), (amount) => {
                this.OnResourceChanged(Game.Resources.Get(ResourceKeys.Health), amount);
            });

            // Actor is defaultly alive.
            this.IsAlive = true;
            this.healthStat = health;
            this.speedStat = speed;
            this.expBonusStat = expBonusStat;
        }

        // Get the current value of the resource
        public float Get(ResourceAsset resource) {
            return this.Resources.Get(resource);
        }

        // Sets the current value of the resource
        public void Set(ResourceAsset resource, float value) {
            this.Resources.Set(this, resource, value);
        }

        public void Modify(ResourceAsset resource, float amount) {
            this.Set(resource, this.Get(resource) + amount);
        }

        // Get the current value of the stat
        public float Get(StatAsset stat) {
            return this.Stats.Get(stat);
        }

        public void SetToDefault(ResourceAsset resource) {
            this.Resources.SetToDefault(this, resource);
        }

        public void CalculateStats() {
            this.Stats.CalculateStats(this.Level.Current);
        }

        public void GiveExp(int amount) {
            var bonus = this.Stats.Get(expBonusStat);
            var expAmount = (int)Math.Round(amount + (amount * bonus));
            this.Level.GiveExp(Math.Max(expAmount, 0));
        }

        private void OnLevelChange()
        {
            this.Stats.CalculateStats(Level.Current);

            // Set vital stats to their default values this
            // will restore the actor's health / mana on level up
            this.Resources.SetToDefault(this, Game.Resources.Get(ResourceKeys.Health));
            this.Resources.SetToDefault(this, Game.Resources.Get(ResourceKeys.Mana));
        }
        
        private void OnStatsChange() 
        {
            // Make sure all resources are correctly 
            // restricted when the stats values change.
            this.Resources.ApplyLimitsAll(this);
        }

        private void HandleCalculateStats(CalculateStatsArgs mods) {
        
        }

        private void OnResourceChanged(ResourceAsset resource, float value) {
            if (resource == Game.Resources.Get(ResourceKeys.Health) && IsAlive && value <= 0f) {
                IsAlive = false;
            }
        }

        public void PrintStats() {
            Console.WriteLine("{0}: IsAlive: {5}, Health: {1}/{2}, Speed: {3}/{4}", this.Name,
                this.Get(Game.Resources.Get(ResourceKeys.Health)), this.Get(this.healthStat),
                this.Get(Game.Resources.Get(ResourceKeys.Speed)), this.Get(this.speedStat), IsAlive);
        }
    }
}