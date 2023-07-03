using System;
using System.Collections.Generic;

namespace Events
{
    public static class Observer
    {
        private static Dictionary<string, List<Action<Event>>> customEvents = new Dictionary<string, List<Action<Event>>>();

        private static List<Action<Event>> GetCustomEvent(string type)
        {
            if (customEvents.ContainsKey(type))
            {
                return customEvents[type];
            }

            customEvents[type] = new List<Action<Event>>();
            return customEvents[type];
        }

        public static void RegisterCustomEventAction(string type, Action<Event> customAction)
        {
            var customEvent = GetCustomEvent(type);
            customEvent.Add(customAction);
        }

        public static void RemoveCustomEventAction(string type, Action<Event> customAction)
        {
            if (!customEvents.ContainsKey(type)) return;

            var customEvent = customEvents[type];
            customEvent.Remove(customAction);
        }

        public static void DispatchCustomEvent(Event eventInfo)
        {
            if (!customEvents.ContainsKey(eventInfo.Type)) return;

            GetCustomEvent(eventInfo.Type).ForEach(action => action?.Invoke(eventInfo));
        }

        public static void RemoveCustomEvents(string type)
        {
            if (!customEvents.ContainsKey(type)) return;

            customEvents.Remove(type);
        }

        public static void RemoveAllCustomEvents()
        {
            customEvents.Clear();
        }
    }
}
