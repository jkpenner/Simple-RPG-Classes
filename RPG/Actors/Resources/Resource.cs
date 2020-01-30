using System;

namespace RPG.Actors.Resources {
    public class Resource {
        public float Amount { get; private set; }
        public event Action<float> OnChange;

        public Resource() {
            this.Amount = 0f;
        }

        public void Set(float amount) {
            if (this.Amount != amount) {
                this.Amount = amount;
                if (this.OnChange != null)
                    this.OnChange.Invoke(this.Amount);
            }
        }
    }
}