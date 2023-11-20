namespace RPGSkills.Architecture
{
    public abstract class Shared : IInjected
    {
        public Container ContainerReference { get; private set; }

        void IInjected.SetContainer(Container container)
        {
            ContainerReference = container;
        }

        public virtual void PreInit() { }
        public virtual void Init() { }
        public virtual void AfterInit() { }
        public virtual void Dispose() { }

        protected Shared() { }
    }
}

