namespace RPGSkills.GameResources
{
    public sealed class DefaultGameResource : BaseGameResource
    {
        public DefaultGameResource(GameResourceType resourceType, int minValue, int maxValue, int baseValue) 
            : base(resourceType, minValue, maxValue, baseValue) { }
    }
}