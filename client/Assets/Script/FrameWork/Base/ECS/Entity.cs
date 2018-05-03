using System;

namespace IDK.Runtime
{
    public class Entity : IEntity
    {
        public uint EntityId
        {
            get { return 0; }
        }
        public void AddComponent<T>(T com) where T : IComponent
        { }

        public void RemoveComponent<T>(T com) where T : IComponent
        { }

        public bool HasComponent<T>() where T:IComponent
        {
            return true;
        }

        public void Dispose()
        { }

        protected virtual void OnDispose()
        { }
    }

}
