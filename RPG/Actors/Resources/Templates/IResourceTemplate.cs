namespace RPG.Actors.Resources
{
    public interface IResourceTemplate 
    {
        // Get the resource amount
        float Get(ResourceKey key);

        // Set the resource amount
        void Set(ResourceKey key, float value);
    }
}