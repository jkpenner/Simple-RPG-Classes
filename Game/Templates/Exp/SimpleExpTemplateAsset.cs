using System;

namespace RPG.Actors.Leveling {
  public class SimpleExpTemplateAsset : ExpTemplateAsset {
    public override int GetRequiredExp(int level) {
      return 100 + (int)Math.Pow(level - 1, 2) * 25;
    }
  }

  
}