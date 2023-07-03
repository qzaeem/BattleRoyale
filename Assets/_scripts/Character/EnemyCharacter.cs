

public class EnemyCharacter : Character
{
    protected override void AttackHasEnded()
    {
        if (Globals.gameState == Globals.GameState.UI || Globals.gameState == Globals.GameState.GameOver)
            return;

        Globals.gameState = Globals.GameState.PlayerMove;
    }

    protected override void GetTargetTransform()
    {
        TargetTranform = Globals.targetPlayerTransform;
    }

    protected override void SendGameOverEvent()
    {
        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_GAME_OVER_PLAYER_WIN));
    }
}
