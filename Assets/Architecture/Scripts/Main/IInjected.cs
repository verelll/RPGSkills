﻿namespace RPGSkills.Architecture
{
    public interface IInjected
    {
        public Container ContainerReference { get; }

        protected internal void SetContainer(Container container);

        protected T GetShared<T>() where T : Shared
        {
            return ContainerReference.GetShared<T>();
        }
    }
}