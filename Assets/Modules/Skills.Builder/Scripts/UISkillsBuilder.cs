using System.Collections.Generic;
using System.Linq;
using RPGSkills.Architecture;
using RPGSkills.Skills;
using UnityEditor;
using UnityEngine;

namespace RPGSkills.SkillsBuilder
{
    [RequireComponent(typeof(UISkillsScreen))]
    public sealed class UISkillsBuilder : MonoBehaviour
    {
        [SerializeField] private UILineRenderer _transitionPrefab;
        [SerializeField] private Transform _transitionContainer;

        private UISkillsScreen Screen
        {
            get
            {
                if (_screen == null)
                    _screen = GetComponent<UISkillsScreen>();

                return _screen;
            }
        }

        private Dictionary<int, UISkillWidget> _widgetsById;
        
        private HashSet<Vector2Int> _transitionPairs;
        private List<UISkillWidget> _widgets;
        private UISkillsScreen _screen;

        public void Rebuild()
        {
            RebuildWidgets();
            BuildTransitions();
        }
        
        private void RebuildWidgets()
        {
            _widgetsById = new Dictionary<int, UISkillWidget>();
            
            _widgets = GetComponentsInChildren<UISkillWidget>().ToList();
            Screen.SetSkillWidgets(_widgets);
            
            foreach (var widget in _widgets)
            {
                if(widget.SkillConfig == null)
                    continue;

                widget.gameObject.name = $"Skill_Widget_{widget.SkillConfig.ID}";
                widget.UpdateView();
                _widgetsById[widget.SkillConfig.ID] = widget;
            }
        }

        private void BuildTransitions()
        {
            ClearTransitions();

            _transitionPairs = new HashSet<Vector2Int>();
            foreach (var widget in _widgets)
            {
                var config = widget.SkillConfig;
                if(config == null)
                    continue;

                var nextSkillConfigs = config.NextSkills;
                var fromRect = (RectTransform) widget.transform;
                foreach (var nextSkill in nextSkillConfigs)
                {
                    var idPairs = new Vector2Int(config.ID, nextSkill.ID);
                    if(_transitionPairs.Contains(idPairs))
                        continue;
                    
                    if(!_widgetsById.TryGetValue(nextSkill.ID, out var nextSkillWidget))
                        continue;

                    var toRect = (RectTransform)nextSkillWidget.transform;
                    
                    CreateTransition(fromRect.anchoredPosition, toRect.anchoredPosition);
                    _transitionPairs.Add(idPairs);
                }
            }
        }

        private void CreateTransition(Vector2 from, Vector2 to)
        {
            var createdTransition = Instantiate(_transitionPrefab, _transitionContainer);
            createdTransition.AddPoint(from, to);
        }

        private void ClearTransitions()
        {
            var transitions = _transitionContainer.GetComponentsInChildren<UILineRenderer>();
            foreach (var transition in transitions)
            {
                DestroyImmediate(transition.gameObject, true);
            }
        }
    }
}