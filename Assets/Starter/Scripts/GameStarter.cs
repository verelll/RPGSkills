using RPGSkills.Architecture;
using RPGSkills.UI;
using UnityEngine;

namespace RPGSkills.Starter
{
    internal sealed class GameStarter : MonoBehaviour
    {
        private Container _mainContainer;
        
        private void Start()
        {
            _mainContainer = new Container();
            _mainContainer.AddShared<UIManager>();
            
            _mainContainer.InitShared();
        }

        private void OnDestroy()
        {
            _mainContainer.DisposeShared();
        }
    }
}

