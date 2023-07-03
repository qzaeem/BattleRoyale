
namespace Events
{
    public abstract class Event
    {
        public string Type { get; private set; }

        protected Event(string type) => Type = type;
    }
}

