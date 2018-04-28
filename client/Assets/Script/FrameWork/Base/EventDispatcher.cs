using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum EventMode
{
    Default = 0,
    AllowNoHandler = 1,
    AllowMultiHandler = 2,
    AllowDuplicateHandler = 4,
}

public class EventDispatcher
{
    Dictionary<int, Delegate> _eventMap = new Dictionary<int, Delegate>();
    EventMode _mode = EventMode.Default;

    public void AddListener(int id, Action handler)
    {
       if(CheckNeedAddMutil(id, handler))
       {
            return;
       }
        var eventHandler = _eventMap[id];
        if (eventHandler == null)
            return;

        eventHandler = (Action)eventHandler + handler;
        _eventMap[id] = eventHandler;
    }

    public void AddListener<T>(int id, Action<T> handler)
    {
        if (CheckNeedAddMutil(id, handler))
        {
            return;
        }
        var eventHandler = _eventMap[id];
        if (eventHandler == null)
            return;

        eventHandler = (Action<T>)eventHandler + handler;
        _eventMap[id] = eventHandler;
    }

    public void AddListener<T, U>(int id, Action<T,U> handler)
    {
        if (CheckNeedAddMutil(id, handler))
        {
            return;
        }
        var eventHandler = _eventMap[id];
        if (eventHandler == null)
            return;

        eventHandler = (Action<T, U>)eventHandler + handler;
        _eventMap[id] = eventHandler;
    }

    public void AddListener<T, U, V>(int id, Action<T, U, V> handler)
    {
        if (CheckNeedAddMutil(id, handler))
        {
            return;
        }
        var eventHandler = _eventMap[id];
        if (eventHandler == null)
            return;

        eventHandler = (Action<T, U, V>)eventHandler + handler;
        _eventMap[id] = eventHandler;
    }

    public void AddListener<T, U, V, W>(int id, Action<T, U, V, W> handler)
    {
        if (CheckNeedAddMutil(id, handler))
        {
            return;
        }
        var eventHandler = _eventMap[id];
        if (eventHandler == null)
            return;

        eventHandler = (Action<T, U, V, W>)eventHandler + handler;
        _eventMap[id] = eventHandler;
    }

    public void RemoveListener(int id, Action handler)
    {
        if (handler == null)
            return;
        if(_eventMap.ContainsKey(id))
        {
            //_eventMap[id] -= handler;
        }
    }



    bool CheckNeedAddMutil(int id, Delegate handler)
    {
        if (handler == null)
            return false;
        if (!_eventMap.ContainsKey(id))
        {
            //@todo  check 里面藏着add操作 ？  
            _eventMap.Add(id, handler);
            return false;
        }
        else if ((_mode & EventMode.AllowMultiHandler) == 0)
        {
            Debug.LogErrorFormat("Event {0} not allow Mutil", id);
            return false;
        }
        else if ((_mode & EventMode.AllowDuplicateHandler) == 0 && CheckDuplicate(id, handler))
        {
            Debug.LogErrorFormat("Event {0} not allow Duplicate", id);
            return false;
        }
        return true;
    }



    public bool CheckDuplicate(int id, Delegate handler)
    {
        Delegate eventHandler;
        if (!_eventMap.TryGetValue(id, out eventHandler))
            return false;

        if (eventHandler == null)
            return false;

        var iter = eventHandler.GetInvocationList().GetEnumerator();
        while(iter.MoveNext())
        {
            if ((Delegate)iter.Current == handler)
                return true;
        }
        return false;
    }

}
