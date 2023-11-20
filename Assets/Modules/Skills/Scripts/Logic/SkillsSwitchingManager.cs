using System.Collections.Generic;
using RPGSkills.Architecture;

namespace RPGSkills.Skills
{
    public sealed class SkillsSwitchingManager : Shared
    {
        private Dictionary<int, BaseSkillModel> _activeSkills;
        private SkillsModel _skillsModel;
        
        public override void PreInit()
        {
            _activeSkills = new Dictionary<int, BaseSkillModel>();
            _skillsModel = ContainerReference.GetShared<SkillsModel>();
        }

        public override void Init()
        {
            _skillsModel.OnSkillLearned += HandleSkillLearned;
            _skillsModel.OnSkillForgotten += HandleSkillForgotten;
        }

        public override void Dispose()
        {
            _skillsModel.OnSkillLearned -= HandleSkillLearned;
            _skillsModel.OnSkillForgotten -= HandleSkillForgotten;
        }

        private void HandleSkillLearned(BaseSkillConfig skillConfig)
        {
            var id = skillConfig.ID;
            if(_activeSkills.ContainsKey(id))
                return;

            var model = skillConfig.CreateModel();
            _activeSkills[id] = model;
            ContainerReference.InjectAt(model);
            model.ActivateSkill();
        }
        
        private void HandleSkillForgotten(BaseSkillConfig skillConfig)
        {
            var id = skillConfig.ID;
            if(!_activeSkills.TryGetValue(id, out var model))
                return;

            model.DeactivateSkill();
            _activeSkills.Remove(id);
        }
    }
}