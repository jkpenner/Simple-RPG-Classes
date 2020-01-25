namespace RPG.Actors.Resources
{
    public interface IResourceTemplate 
    {
        float GetDefaultValue(IActor actor, ResourceKeys key);
        float GetClampedValue(IActor actor, ResourceKeys key, float value);

        void ApplyDefaults(IActor actor, ResourceCollection resources);
        void ApplyLimits(IActor actor, ResourceCollection resources);
    }
}