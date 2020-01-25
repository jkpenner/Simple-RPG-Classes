namespace RPG.Actors.Resources
{
    public interface IResourceTemplate 
    {
        float GetDefault(Actor actor, ResourceKeys key);
        float ApplyLimits(Actor actor, ResourceKeys key, float value);

        void ApplyDefaults(Actor actor, ResourceCollection resources);
        void ApplyLimits(Actor actor, ResourceCollection resources);
    }
}