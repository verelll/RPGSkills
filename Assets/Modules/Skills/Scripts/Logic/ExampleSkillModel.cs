using UnityEngine;

namespace RPGSkills.Skills
{
    public sealed class ExampleSkillModel : BaseSkillModel
    {
        private ExampleSkillConfig _skillConfig;
        
        public override void ActivateSkill()
        {
            Debug.Log($"Skill Activated: {_skillConfig.ID}. SkillAction: {_skillConfig.SkillAction}");
        }

        public override void DeactivateSkill()
        {
            Debug.Log($"Skill Deactivated: {_skillConfig.ID}.");
        }

        public ExampleSkillModel(ExampleSkillConfig config) : base(config)
        {
            _skillConfig = config;
        }
    }
}