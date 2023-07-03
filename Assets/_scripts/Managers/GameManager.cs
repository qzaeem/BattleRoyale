using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UI_Manager uiManager;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_DISPLAY_UI, ShowSelectionMessage);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_SHOW_PLAYER_INFO, ShowPlayerInfo);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_MAX_PLAYERS_SELECTED, MaxPlayersForBattleSelected);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_PLAYER_SELECTED_FOR_ATTACK,
            ShowStartAttackButton);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_WIN, PlayerWins);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_LOSE, PlayerLoses);
    }

    private void OnDisable()
    {
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_DISPLAY_UI, ShowSelectionMessage);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_SHOW_PLAYER_INFO, ShowPlayerInfo);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_MAX_PLAYERS_SELECTED, MaxPlayersForBattleSelected);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_PLAYER_SELECTED_FOR_ATTACK,
            ShowStartAttackButton);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_WIN, PlayerWins);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_LOSE, PlayerLoses);
    }

    private void Start()
    {
        Globals.gameState = Globals.GameState.UI;
    }

    private void ShowSelectionMessage(Events.Event eventInfo)
    {
        uiManager.DisplaySelectionMsg(true);
    }

    private void ShowPlayerInfo(Events.Event eventInfo)
    {
        var playerInfo = ((Events.PlayerInfoEvent)eventInfo).PlayerInfo;
        uiManager.DisplayPlayerInfo(true, playerInfo);
    }

    private void MaxPlayersForBattleSelected(Events.Event eventInfo)
    {
        uiManager.DisplayStartButton(true);
    }

    private void ShowStartAttackButton(Events.Event eventInfo)
    {
        uiManager.DisplayAttackButton(true);
    }

    public void PlayerWins(Events.Event eventInfo)
    {
        Globals.gameState = Globals.GameState.GameOver;
        uiManager.ShowGameoverPanel(true);
        GameData.BattleEnded();
    }

    public void PlayerLoses(Events.Event eventInfo)
    {
        Globals.gameState = Globals.GameState.GameOver;
        uiManager.ShowGameoverPanel(false);
        GameData.BattleEnded();
    }

    public void StartBattleMode()
    {
        Globals.gameState = Globals.GameState.PlayerMove;
        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_START_BATTLE));
    }

    public void StartAttack()
    {
        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_START_PLAYER_MOVE));
    }

    private void OnDestroy()
    {
        Events.Observer.RemoveAllCustomEvents();
    }
}
