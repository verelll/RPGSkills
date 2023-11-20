using System.Collections.Generic;
using RPGSkills.Architecture;
using UnityEngine;

namespace RPGSkills.Skills
{
    [CreateAssetMenu(
        menuName = "RPGSkills/Skills/" + nameof(GeneralSkillsConfig),
        fileName = nameof(GeneralSkillsConfig))]
    public sealed class GeneralSkillsConfig : SingletonScriptableObject<GeneralSkillsConfig>
    {
        [SerializeField] private List<BaseSkillConfig> _allSkills;

        public IReadOnlyList<BaseSkillConfig> AllSkills => _allSkills;
    }
}