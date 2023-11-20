using System.Collections.Generic;
using RPGSkills.GameResources;
using RPGSkills.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGSkills.Skills
{
    public sealed class UISkillsScreen : UIScreen
    {
        [SerializeField] private TMP_Text _pointText;
        [SerializeField] private Button _addPointButton;

        [SerializeField] private Button _learnButton;
        [SerializeField] private Button _forgetButton;
        [SerializeField] private Button _forgetAllButton;
        
        [SerializeField] private List<UISkillWidget> _skillWidgets;

        private Dictionary<int, UISkillWidget> _skillWidgetById;

        private SkillsModel _skillsModel;
        private UISkillWidget _selectedWidget;
        
        private BaseGameResource _skillPoints;
        
        public void SetSkillWidgets(IEnumerable<UISkillWidget> widgets)
        {
            _skillWidgets = new List<UISkillWidget>();
            _skillWidgets.AddRange(widgets);
        }

        public override void Init()
        {
            _skillWidgetById = new Dictionary<int, UISkillWidget>();
            _skillsModel = ContainerReference.GetShared<SkillsModel>();
            _skillPoints = ContainerReference.GetShared<GameResourcesManager>()
                .GetGameResource(GameResourceType.SkillPoints);

            foreach (var widget in _skillWidgets)
            {
                _skillWidgetById[widget.SkillConfig.ID] = widget;
            }
            
            _addPointButton.onClick.AddListener(HandleAddPoint);
            _learnButton.onClick.AddListener(HandleLearnClick);
            _forgetButton.onClick.AddListener(HandleForgetClick);
            _forgetAllButton.onClick.AddListener(HandleForgetAllClick);

            _skillsModel.OnSkillLearned += HandleSkillLearned;
            _skillsModel.OnSkillForgotten += HandleSkillForgotten;
            _skillPoints.OnValueChanged += HandlePointsChanged;
            
            foreach (var widget in _skillWidgets)
            {
                widget.Init();
                widget.OnWidgetClick += HandleWidgetClicked;

                if (_skillsModel.IsSkillLearned(widget.SkillConfig))
                    HandleSkillLearned(widget.SkillConfig);
            }

            UpdateButtons();
            HandlePointsChanged();
        }

        public override void Dispose()
        {
            _addPointButton.onClick.RemoveListener(HandleAddPoint);
            _learnButton.onClick.RemoveListener(HandleLearnClick);
            _forgetButton.onClick.RemoveListener(HandleForgetClick);
            _forgetAllButton.onClick.RemoveListener(HandleForgetAllClick);
            
            _skillsModel.OnSkillLearned -= HandleSkillLearned;
            _skillsModel.OnSkillForgotten -= HandleSkillForgotten;
            _skillPoints.OnValueChanged -= HandlePointsChanged;
            
            foreach (var widget in _skillWidgets)
            {
                widget.OnWidgetClick -= HandleWidgetClicked;
                widget.Dispose();
            }
        }

        private void UpdateButtons()
        {
            var selectedSkill = _skillsModel.SelectedSkill;
            
            var showLearnButton = _skillsModel.CanLearnSkill(selectedSkill);
            _learnButton.interactable = showLearnButton;

            var showForgetButton = _skillsModel.CanForgetSkill(selectedSkill);
            _forgetButton.interactable = showForgetButton;

            var showForgetAllButton = _skillsModel.AnySkillLearned();
            _forgetAllButton.interactable = showForgetAllButton;
        }
        
        private void HandlePointsChanged()
        {
            _pointText.SetText(_skillsModel.PointsValue.ToString());
            UpdateButtons();
        }

        private void HandleWidgetClicked(UISkillWidget widget)
        {
            _selectedWidget?.SelectWidget(false);

            _selectedWidget = widget;
            _skillsModel.SelectSkill(_selectedWidget?.SkillConfig);
            
            UpdateButtons();

            _selectedWidget.SelectWidget(true);
        }

        private void HandleSkillLearned(BaseSkillConfig skillConfig)
        {
            if(!_skillWidgetById.TryGetValue(skillConfig.ID, out var widget))
                return;
            
            widget.SetLearned(true);
        }
        
        private void HandleSkillForgotten(BaseSkillConfig skillConfig)
        {
            if(!_skillWidgetById.TryGetValue(skillConfig.ID, out var widget))
                return;
            
            widget.SetLearned(false);
        }

#region Button Handlers

        private void HandleAddPoint()
        {
            _skillsModel.InvokeAddPoint();
        }
        
        private void HandleLearnClick()
        {
            _skillsModel.InvokeLearnSkill();
        }

        private void HandleForgetClick()
        {
            _skillsModel.InvokeForgetSkill();
        }
        
        private void HandleForgetAllClick()
        {
            _skillsModel.InvokeForgetAllSkills();
        }
        
#endregion

    }
}

