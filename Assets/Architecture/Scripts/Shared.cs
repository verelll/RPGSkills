namespace RPGSkills.Architecture
{
    public abstract class Shared : IInjected
    {
        private Container _containerReference;

        Container IInjected.ContainerReference => _containerReference;

        void IInjected.SetContainer(Container container)
        {
            _containerReference = container;
        }

        public virtual void Init() { }
        public virtual void Dispose() { }
    }
}

