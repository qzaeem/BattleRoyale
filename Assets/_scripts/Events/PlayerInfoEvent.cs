
namespace Events
{
    public class PlayerInfoEvent : Event
    {
        public PlayerData PlayerInfo { get; private set; }

        public PlayerInfoEvent(string type, PlayerData playerData) : base(type)
        {
            PlayerInfo = playerData;
        }
    }
}
