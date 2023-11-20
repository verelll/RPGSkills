using System;

namespace RPGSkills.GameResources
{
    public abstract class BaseGameResource
    {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int Amount => _amount;
        public GameResourceType ResourceType { get; }

        public event Action OnValueChanged;
            
        private int _amount;

        public bool CanAddValue(int value)
        {
            return _amount + value <= MaxValue;
        }
        
        public void AddValue(int value)
        {
            if (_amount + value > MaxValue)
            {
                _amount = MaxValue;
                
            }
            else
            {
                _amount += value;
            }

            OnValueChanged?.Invoke();
        }
        
        public bool CanRemoveValue(int value)
        {
            return _amount >= value;
        }
        
        public void RemoveValue(int value)
        {
            if (_amount - value < MinValue)
            {
                _amount = MinValue;
            }
            else
            {
                _amount -= value;
            }
            
            OnValueChanged?.Invoke();
        }

        internal BaseGameResource(GameResourceType resourceType, int minValue, int maxValue, int baseValue)
        {
            ResourceType = resourceType;
            MinValue = minValue;
            MaxValue = maxValue;
            _amount = baseValue;
        }
    }
}