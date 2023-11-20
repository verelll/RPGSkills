using System.Collections.Generic;
using RPGSkills.Architecture;
using UnityEngine;

namespace RPGSkills.UI
{
    public sealed class UIManager : Shared
    {
        private Dictionary<UIScreenType, UIScreen> _uiScreens;

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

        public UIManager()
        {
            _uiScreens = new Dictionary<UIScreenType, UIScreen>();
        }
        
        public override void AfterInit()
        {
            InitScreens();
        }

        public override void Dispose()
        {
            DisposeScreens();
        }

        private void InitScreens()
        {
            var screens = Hierarchy.Screens;
            foreach (var layer in screens)
            {
                ContainerReference.InjectAt(layer);
                _uiScreens[layer.ScreenType] = layer;
                layer.Init();
            }
        }
        
        private void DisposeScreens()
        {
            foreach (var pair in _uiScreens)
            {
                pair.Value.Dispose();
            }
        }
    }
}

