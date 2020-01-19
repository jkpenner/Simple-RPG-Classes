using System.Collections.Generic;
using RPG.Actors;
using RPG.Actors.Stats;

namespace RPG.Items {
  public interface IEquipment {
    void OnEquip(Actor actor);
    void OnUnequip(Actor actor);
  }

  public enum EquipSlot {

  }

  public class Equipment {
    private Actor owner;
    private Dictionary<EquipSlot, IEquipment> items;

    public Equipment(Actor owner) {
      this.owner = owner;
      this.items = new Dictionary<EquipSlot, IEquipment>();
    }

    public void OnCalculateStats(StatModCollection mods) {
        foreach(var item in items.Values) {
            var modifier = item as IStatModifier;
            if (modifier != null) {
                modifier.OnCalculateStats(mods);
            }
        }
    }
  }
}