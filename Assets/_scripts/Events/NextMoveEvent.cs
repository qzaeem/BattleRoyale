
namespace Events
{
    public class NextMoveEvent : Event
    {
        public const string Name = "NextMoveEvent";
        public bool isPlayersTurn = false;

        public NextMoveEvent(bool isPlayersTurn) : base(Name)
        {
            this.isPlayersTurn = isPlayersTurn;
        }
    }
}
