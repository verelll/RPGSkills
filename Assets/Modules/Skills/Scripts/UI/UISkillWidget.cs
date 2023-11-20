using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPGSkills.Skills
{
    public sealed class UISkillWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _button;

        [Space] 
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _learnedColor;
        
        [Space]
        [SerializeField] private BaseSkillConfig _skill;

        public BaseSkillConfig SkillConfig => _skill;
        public event Action<UISkillWidget> OnWidgetClick;

        public void Init()
        {
            _button.onClick.AddListener(HandleClick);
            UpdateView();
            SelectWidget(false);
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(HandleClick);
        }
        
        public void UpdateView()
        {
            if(_skill == null)
                return;

            var setIcon = _skill.Icon != null;
            _iconImage.gameObject.SetActive(setIcon);
            if(setIcon)
                _iconImage.sprite = _skill.Icon;
            
            _priceText.SetText(_skill.Price.ToString());
        }

        public void SelectWidget(bool selected)
        {
            _frameImage.gameObject.SetActive(selected);
        }

        public void SetLearned(bool isLearned)
        {
            _background.color = isLearned 
                ? _learnedColor 
                : _defaultColor;
        }

        private void HandleClick()
        {
            OnWidgetClick?.Invoke(this);
        }
    }
}