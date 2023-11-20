using RPGSkills.Architecture;
using RPGSkills.GameResources;
using RPGSkills.Skills;
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
            _mainContainer.AddShared<GameResourcesManager>();
            
            _mainContainer.AddShared<SkillsModel>();
            _mainContainer.AddShared<SkillsManager>();
            _mainContainer.AddShared<SkillsSwitchingManager>();
            
            _mainContainer.InitShared();
        }

        private void OnDestroy()
        {
            _mainContainer.DisposeShared();
        }
    }
}

