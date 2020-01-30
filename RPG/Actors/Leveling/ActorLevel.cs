using System;

// This would be a MonoBehavour
namespace RPG.Actors.Leveling { // : MonoBehavour {
    public class ActorLevel {
        private Action onLevelChange;

        public readonly ExpTemplateAsset ExpTemplate;
        
        public int Exp              { get; private set; }
        public int RequiredExp      { get; private set; }
        
        public bool IsScaled        { get; private set; }
        public int NormalLevel      { get; private set; }
        public int ScaledLevel      { get; private set; }

        public int Current { 
            get { return IsScaled ? ScaledLevel : NormalLevel; }
        }
        
        public bool CanGainExperience { 
            get { return ExpTemplate != null; }
        }

        public ActorLevel(ExpTemplateAsset expTemplate, int initialLevel, Action onLevelChange) {
            this.IsScaled = false;
            this.NormalLevel = initialLevel;
            this.ScaledLevel = 0;
            
            this.Exp = 0;
            this.RequiredExp = 0;

            this.onLevelChange = onLevelChange;
            
            this.ExpTemplate = expTemplate;
            if (this.ExpTemplate != null) {
                this.RequiredExp = this.ExpTemplate.GetRequiredExp(this.Current);
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
            if (this.onLevelChange != null) {
                this.onLevelChange.Invoke();
            }
        }
    }
}