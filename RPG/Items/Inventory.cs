using System.Collections.Generic;

namespace RPG.Items {
  public class Inventory {
    private List<Item> items;

    public Inventory() {
      this.items = new List<Item>();
    }
  }
}