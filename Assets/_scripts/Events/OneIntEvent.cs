
namespace Events
{
    public class OneIntEvent : Event
    {
        public int intVal;

        public OneIntEvent(string type, int intVal) : base(type)
        {
            this.intVal = intVal;
        }
    }
}
