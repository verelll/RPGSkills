using System.Collections.Generic;
using RPGSkills.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPGSkills.Skills
{
    public sealed class UISkillsScreen : UIScreen
    {
        [SerializeField] private List<UISkillWidget> _skillWidgets;
        
        public override void Init()
        {
            
        }
    }
}

