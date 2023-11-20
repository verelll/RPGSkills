using RPGSkills.Architecture;
using UnityEngine;

namespace RPGSkills.UI
{
    public abstract class UIScreen : MonoBehaviour, IInjected
    {
        [SerializeField]
        private UIScreenType _uiScreenType;

        public UIScreenType ScreenType => _uiScreenType;
        
        public Container ContainerReference { get; private set; }

        void IInjected.SetContainer(Container container)
        {
            ContainerReference = container;
        }
        
        public virtual void Init() { }
        public virtual void Dispose() { }
    }
}