using UnityEngine;

namespace RPGSkills.Skills
{
    [CreateAssetMenu(
        menuName = "RPGSkills/Skills/" + nameof(ExampleSkillConfig),
        fileName = nameof(ExampleSkillConfig))]
    public class ExampleSkillConfig : BaseSkillConfig
    {
        [SerializeField, TextArea(5, 5)]
        private string _skillAction;

        public string SkillAction => _skillAction;
        
        public override BaseSkillModel CreateModel()
        {
            return new ExampleSkillModel(this);
        }
    }
}