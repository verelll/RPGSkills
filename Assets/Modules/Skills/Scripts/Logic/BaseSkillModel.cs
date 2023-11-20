using RPGSkills.Architecture;

namespace RPGSkills.Skills
{
    public abstract class BaseSkillModel : IInjected
    {
        public Container ContainerReference { get; private set; }
        void IInjected.SetContainer(Container container)
        {
            ContainerReference = container;
        }
        
        public abstract void ActivateSkill();
        public abstract void DeactivateSkill();

        internal BaseSkillModel(BaseSkillConfig config) { }
    }
}