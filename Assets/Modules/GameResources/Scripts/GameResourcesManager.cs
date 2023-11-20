using System.Collections.Generic;
using RPGSkills.Architecture;

namespace RPGSkills.GameResources
{
    public sealed class GameResourcesManager : Shared
    {
        private Dictionary<GameResourceType, BaseGameResource> _gameResources;

        public override void PreInit()
        {
            _gameResources = new Dictionary<GameResourceType, BaseGameResource>();
        }

        public override void Dispose() { }

        public BaseGameResource AddDefaultResource(GameResourceType resourceType, int minValue, int maxValue, int baseValue)
        {
            var newResource = new DefaultGameResource(resourceType, minValue, maxValue, baseValue);
            return AddGameResource(newResource);
        }
        
        public BaseGameResource AddGameResource(BaseGameResource resource)
        {
            var type = resource.ResourceType;
            if(_gameResources.ContainsKey(type))
                return GetGameResource(type);

            return _gameResources[type] = resource;
        }

        public T GetGameResource<T>(GameResourceType type) where T : BaseGameResource
        {
            var resource = GetGameResource(type);
            return resource as T;
        }

        public BaseGameResource GetGameResource(GameResourceType type)
        {
            if (!_gameResources.TryGetValue(type, out var resource))
                return default;

            return resource;
        }
    }
}

