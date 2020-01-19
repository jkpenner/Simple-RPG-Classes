using System;

namespace RPG.Actors.Leveling {
  public class PlayerExpTemplate : IExpTemplate {
    public int GetRequiredExp(int level) {
      return 100 + (int)Math.Pow(level - 1, 2) * 25;
    }
  }
}