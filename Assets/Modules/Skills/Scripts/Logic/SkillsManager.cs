using RPGSkills.Architecture;
using RPGSkills.GameResources;

namespace RPGSkills.Skills
{
    public sealed class SkillsManager : Shared
    {
        private const int Min_Points_Value = 0;
        private const int Max_Points_Value = 200;
        private const int Base_Points_Value = 0;
        
        private GameResourcesManager _gameResourcesManager;
        private DefaultGameResource _skillPoints;
        
        public override void PreInit()
        {
            _gameResourcesManager = ContainerReference.GetShared<GameResourcesManager>();
            RegisterPoints();
        }

        public override void Dispose() { }

        private void RegisterPoints()
        {
            var resource = _gameResourcesManager.AddDefaultResource(
                GameResourceType.SkillPoints,
                Min_Points_Value,
                Max_Points_Value,
                Base_Points_Value);
            
            _skillPoints = resource as DefaultGameResource;
        }
    }
}