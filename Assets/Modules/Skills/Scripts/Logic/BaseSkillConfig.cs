using System.Collections.Generic;
using UnityEngine;

namespace RPGSkills.Skills
{
    [CreateAssetMenu(
        menuName = "RPGSkills/Skills/" + nameof(BaseSkillConfig),
        fileName = nameof(BaseSkillConfig))]
    public class BaseSkillConfig : ScriptableObject
    {
        [SerializeField] private string _desciption;
        [SerializeField] private Sprite _icon;

        [SerializeField] private List<BaseSkillConfig> _parents;
        [SerializeField] private List<BaseSkillConfig> _childs;

        public string ID => name;
        public string Description => _desciption;
        public Sprite Icon => _icon;

        public IReadOnlyList<BaseSkillConfig> Parents => _parents;
        public IReadOnlyList<BaseSkillConfig> Childs => _childs;
    }
}