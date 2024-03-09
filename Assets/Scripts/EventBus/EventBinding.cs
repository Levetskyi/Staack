using System;

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    private Action<T> _onEvent = _ => { };
    private Action _onEventNoArgs = () => { };

    public EventBinding(Action<T> action) => _onEvent = action;
    public EventBinding(Action action) => _onEventNoArgs = action;

    Action<T> IEventBinding<T>.OnEvent 
    { 
        get => _onEvent;
        set => _onEvent = value;
    }

    Action IEventBinding<T>.OnEventNoArgs 
    { 
        get => _onEventNoArgs; 
        set => _onEventNoArgs = value; 
    }

    public void Add(Action<T> action) => _onEvent += action;
    public void Remove(Action<T> action) => _onEvent -= action;

    public void Add(Action action) => _onEventNoArgs += action;
    public void Remove(Action action) => _onEventNoArgs -= action;
}
