using System;
using System.Collections.Generic;

namespace Playground.Services.Event
{
    public class EventBus
    {
        #region Variables

        private readonly Dictionary<Type, List<object>> _handlersByEventType = new();

        #endregion

        #region Public methods

        public void Fire<TEvent>(TEvent e = default)
        {
            List<object> handlers = GetHandlers<TEvent>();

            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                object handler = handlers[i];
                if (handler is Action action)
                {
                    action.Invoke();
                }
                else if (handler is Action<TEvent> paramAction)
                {
                    paramAction.Invoke(e);
                }
            }
        }

        public void Subscribe<TEvent>(Action handler)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Add(handler);
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Add(handler);
        }

        public void Unsubscribe<TEvent>(Action handler)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Remove(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Remove(handler);
        }

        #endregion

        #region Private methods

        private List<object> GetHandlers<TEvent>()
        {
            Type type = typeof(TEvent);
            if (!_handlersByEventType.TryGetValue(type, out List<object> handlers))
            {
                handlers = new List<object>();
                _handlersByEventType.Add(type, handlers);
            }

            return handlers;
        }

        #endregion
    }
}