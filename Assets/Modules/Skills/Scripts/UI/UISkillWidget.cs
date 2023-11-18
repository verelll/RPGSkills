using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGSkills.Skills
{
    public sealed class UISkillWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _idText;
        [SerializeField] private Image _iconImage;
        
        [SerializeField] private BaseSkillConfig _skill;
    }
}