using UnityEngine;

namespace RPG.Actors.Leveling {
    public abstract class ExpTemplateAsset : ScriptableObject {
        public abstract int GetRequiredExp(int level);
    }
}