using System;
using System.Collections.Generic;
using System.Linq;
using RPGSkills.Architecture;
using RPGSkills.GameResources;

namespace RPGSkills.Skills
{
    public sealed class SkillsModel : Shared
    {
        private const int Added_Points_Count = 1;
        
        private Dictionary<int, BaseSkillConfig> _learnedSkills;
        public BaseSkillConfig SelectedSkill { get; private set; }

        public int PointsValue => _skillPoints.Amount;
        
        public event Action OnSelectedSkillChanged;
        public event Action<BaseSkillConfig> OnSkillLearned;
        public event Action<BaseSkillConfig> OnSkillForgotten;
        
        private GameResourcesManager _gameResourcesManager;
        private DefaultGameResource _skillPoints;

        private GeneralSkillsConfig _generalSkillsConfig;
        
        public SkillsModel()
        {
            _learnedSkills = new Dictionary<int, BaseSkillConfig>();
        }
        
        public override void Init()
        {
            _generalSkillsConfig = GeneralSkillsConfig.Instance;
            
            _gameResourcesManager = ContainerReference.GetShared<GameResourcesManager>();
            _skillPoints = _gameResourcesManager.GetGameResource(GameResourceType.SkillPoints) as DefaultGameResource;
            
            ActivateByDefault();
        }

        private void ActivateByDefault()
        {
            foreach (var skill in _generalSkillsConfig.AllSkills)
            {
                if(!skill.ActiveByDefault)
                    continue;
                
                LearnSkill(skill, false);
            }
        }

#region Main Logic

        internal void SelectSkill(BaseSkillConfig config)
        {
            SelectedSkill = config;
            OnSelectedSkillChanged?.Invoke();
        }

        internal bool AnySkillLearned()
        {
            return _learnedSkills.Count > 0;
        }

        internal bool IsSkillLearned(BaseSkillConfig config)
        {
            if (config == null)
                return false;

            if (!_learnedSkills.ContainsKey(config.ID))
                return false;

            return true;
        }
        
        internal bool CanLearnSkill(BaseSkillConfig skillConfig, bool applyPrice = true)
        {
            if(skillConfig == null)
                return false;

            if(_learnedSkills.ContainsKey(skillConfig.ID))
                return false;

            if(applyPrice)
            {
                if (!_skillPoints.CanRemoveValue(skillConfig.Price))
                    return false;
            }

            if (skillConfig.ActiveByDefault) 
                return true;
            
            var anyPrevSkillLearned = skillConfig.PrevSkills.Any(s => IsSkillLearned(s));
            var anyNextSkillLearned = skillConfig.NextSkills.Any(s => IsSkillLearned(s));
            return anyPrevSkillLearned || anyNextSkillLearned;
        }
        
        internal bool CanForgetSkill(BaseSkillConfig skillConfig, bool applyNextSkills = true)
        {
            if(skillConfig == null)
                return false;

            if(!_learnedSkills.ContainsKey(skillConfig.ID))
                return false;

            if (skillConfig.ActiveByDefault)
                return false;

            if (!applyNextSkills)
                return true;

            var anyNextSkillLearned = skillConfig.NextSkills.Any(s => IsSkillLearned(s));
            return !anyNextSkillLearned;
        }

        private void LearnSkill(BaseSkillConfig skillConfig, bool applyPrice = true)
        {
            if(!CanLearnSkill(skillConfig, applyPrice))
                return;

            var id = skillConfig.ID;
            _learnedSkills[id] = skillConfig;
            
            if(applyPrice)
                _skillPoints.RemoveValue(skillConfig.Price);
            
            OnSkillLearned?.Invoke(skillConfig);
        }
        
        private void ForgetSkill(BaseSkillConfig skillConfig, bool applyCheckNextSkills = true)
        {
            if(!CanForgetSkill(skillConfig, applyCheckNextSkills))
                return;

            var id = skillConfig.ID;
            _learnedSkills.Remove(id);
            
            _skillPoints.AddValue(skillConfig.Price);
            OnSkillForgotten?.Invoke(skillConfig);
        }
        
        private void ForgetAllSkills()
        {
            foreach (var pair in _learnedSkills.ToList())
            {
                ForgetSkill(pair.Value, false);
            }
        }
        
#endregion


#region Handlers

        internal void InvokeAddPoint()
        {
            if(!_skillPoints.CanAddValue(Added_Points_Count))
                return;
                
            _skillPoints.AddValue(Added_Points_Count);
        }

        internal void InvokeLearnSkill()
        {
            LearnSkill(SelectedSkill);
        }
        
        internal void InvokeForgetSkill()
        {
            ForgetSkill(SelectedSkill);
        }
        
        internal void InvokeForgetAllSkills()
        {
            ForgetAllSkills();
        }

#endregion

    }
}