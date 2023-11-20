using System.Collections.Generic;
using UnityEngine;

namespace RPGSkills.UI
{
    public sealed class UIHierarchy : MonoBehaviour
    {
        [SerializeField] private List<UIScreen> _screens;
        
        public IReadOnlyList<UIScreen> Screens => _screens;
    }
}
