using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGSkills.Skills
{
    public abstract class BaseSkillConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _desciption;
        [SerializeField] private Sprite _icon;

        [SerializeField] private int _skillPrice;

        [SerializeField] private bool _activeByDefault;
        
        [SerializeField] private List<BaseSkillConfig> _nextSkills;
        [SerializeField] private List<BaseSkillConfig> _prevSkills;

        public int ID => _id;
        public string Description => _desciption;
        public Sprite Icon => _icon;
        public int Price => _skillPrice;
        public bool ActiveByDefault => _activeByDefault;
        
        public IReadOnlyList<BaseSkillConfig> NextSkills => _nextSkills;
        public IReadOnlyList<BaseSkillConfig> PrevSkills => _prevSkills;

        public abstract BaseSkillModel CreateModel();
    }
}