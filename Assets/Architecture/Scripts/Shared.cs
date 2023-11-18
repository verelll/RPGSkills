namespace RPGSkills.Architecture
{
    public abstract class Shared : IInjected
    {
        public Container ContainerReference { get; private set; }

        void IInjected.SetContainer(Container container)
        {
            ContainerReference = container;
        }

        public virtual void Init() { }
        public virtual void Dispose() { }
    }
}

