using System.Collections.Generic;
using UnityEngine;

namespace RPGSkills.UI
{
    public sealed class UIHierarchy : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private Canvas _uiCanvas;
        
        [SerializeField] private List<UIScreen> _screens;

        public Camera UICamera => _uiCamera;
        public Canvas UICanvas => _uiCanvas;

        public IReadOnlyList<UIScreen> Screens => _screens;
    }
}
