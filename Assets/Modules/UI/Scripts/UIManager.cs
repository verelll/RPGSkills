using System.Collections.Generic;
using RPGSkills.Architecture;
using UnityEngine;

namespace RPGSkills.UI
{
    public sealed class UIManager : Shared
    {
        private Dictionary<UIScreenType, UIScreen> _screens;

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
            _screens = new Dictionary<UIScreenType, UIScreen>();
            
            InitScreens();
        }

        public override void Dispose()
        {
            DisposeScreens();
        }

        private void InitScreens()
        {
            var screenObjects = Hierarchy.Screens;
            foreach (var screen in screenObjects)
            {
                _screens[screen.ScreenType] = screen;
            }

            foreach (var pair in _screens)
            {
                var screen = pair.Value;
                ContainerReference.InjectAt(screen);
                screen.Init();
            }
        }

        private void DisposeScreens()
        {
            foreach (var pair in _screens)
            {
                var screen = pair.Value;
                screen.Dispose();
            }
        }
    }
}

