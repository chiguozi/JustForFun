using System;

namespace IDK.Runtime
{
    public interface ISystemManager
    {
        void RegistSystem(ISystem sys);
    }

    public interface IEntityManager
    {
        IEntity CreateEntity();
        IEntity GetEntity(uint entityId);

        void DelEntity(uint entityId);
        void DelEntity(IEntity entity); 
    }

    public interface ISystem
    {
    }

    public interface IComponent
    {
    }

    public interface IEntity
    {
        uint EntityId { get; }

        void AddComponent<T>(T com) where T : IComponent;
        void RemoveComponent<T>(T com) where T: IComponent;


        bool HasComponent<T>() where T : IComponent;

        void Dispose();
    }
}

