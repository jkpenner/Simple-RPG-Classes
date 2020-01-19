using System;

// This would be a MonoBehavour
namespace RPG.Actors.Leveling { // : MonoBehavour {
    public class ActorLevel {
        public readonly bool CanGainExperience;
        public readonly IExpTemplate ExpTemplate;
        
        public int Current { get { 
            return IsScaled ? ScaledLevel : NormalLevel; 
        }}
        
        public int Exp { get; private set; }
        public int RequiredExp { get; private set; }
        
        public bool IsScaled { get; private set; }
        public int NormalLevel { get; private set; }
        public int ScaledLevel { get; private set; }

        public event Action<ActorLevel> LevelChangeEvent;
        
        public ActorLevel(int level, IExpTemplate expTemplate) {
            this.IsScaled = false;
            this.NormalLevel = level;
            this.ScaledLevel = 0;
            
            this.Exp = 0;
            this.ExpTemplate = expTemplate;
            
            if (this.ExpTemplate != null) {
                this.RequiredExp = this.ExpTemplate.GetRequiredExp(this.Current);
                this.CanGainExperience = true;
            } else {
                this.RequiredExp = 0;
                this.CanGainExperience = false;
            }
        }
        
        public void GiveExp(int amount) {
            if (!CanGainExperience) return;
            
            this.Exp += amount;
            while (this.Exp >= RequiredExp) {
                this.Exp -= RequiredExp;
                this.SetLevel(Current + 1);
            }
        }
        
        public void SetLevel(int level) {
            NormalLevel = level;
            if (CanGainExperience)
                RequiredExp = ExpTemplate.GetRequiredExp(Current);
            InvokeLevelChangeEvent();
        }
        
        public void SetScaledLevel(int level) {
            IsScaled = true;
            ScaledLevel = level;
            InvokeLevelChangeEvent();
        }
        
        public void ClearScaledLevel() {
            IsScaled = true;
            ScaledLevel = 0;
            InvokeLevelChangeEvent();
        }
        
        private void InvokeLevelChangeEvent() {
            if (LevelChangeEvent != null) {
                LevelChangeEvent.Invoke(this);
            }
        }
    }
}