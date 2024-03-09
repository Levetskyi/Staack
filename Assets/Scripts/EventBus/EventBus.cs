using System.Collections.Generic;

public static class EventBus<T> where T : IEvent
{
    private static readonly HashSet<IEventBinding<T>> _eventBindings = new();

    public static void Register(IEventBinding<T> eventBinding) => _eventBindings.Add(eventBinding);
    public static void Deregister(IEventBinding<T> eventBinding) => _eventBindings.Remove(eventBinding);

    public static void Raise(T action)
    {
        foreach (var binding in _eventBindings)
        {
            binding.OnEvent.Invoke(action);
            binding.OnEventNoArgs.Invoke();
        }
    }

    private static void Clear() => _eventBindings.Clear();
}