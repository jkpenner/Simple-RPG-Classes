namespace RPG.Actors.Resources {
    public class Resource {
        public float Amount { get; private set; }
        public float DefaultAmount { get; private set; }

        public Resource() {
            this.DefaultAmount = 0f;
            this.Amount = 0f;
        }

        public void SetAmount(float amount) {
            this.Amount = amount;
        }

        public void SetDefaultAmount(float amount) {
            this.DefaultAmount = amount;
        }

        public void SetToDefault() {
            this.Amount = this.DefaultAmount;
        }
    }
}