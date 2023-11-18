using RPGSkills.Architecture;
using UnityEngine;

namespace RPGSkills.UI
{
    public sealed class UIManager : Shared
    {
        public UIHierarchy Hierarchy
        {
            get
            {
                if (_uiHierarchy == null)
                    _uiHierarchy = Object.FindObjectOfType<UIHierarchy>();

                return _uiHierarchy;
            }
        }

        private UIHierarchy _uiHierarchy;


        public override void Init()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}

