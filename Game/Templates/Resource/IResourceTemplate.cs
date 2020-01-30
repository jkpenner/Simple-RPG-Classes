namespace RPG.Actors.Resources
{
    public interface IResourceTemplate 
    {
        float GetDefaultValue(IActor actor, ResourceAsset resource);
        float GetClampedValue(IActor actor, ResourceAsset resource, float value);

        void ApplyDefaults(IActor actor, ResourceCollection resources);
        void ApplyLimits(IActor actor, ResourceCollection resources);
    }
}