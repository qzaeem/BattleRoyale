
using UnityEngine;

public static class Constants
{
    public const int MAX_PLAYERS = 10;
    public const int MIN_PLAYERS = 3;
    public const int MAX_X_POSITION = 5;

    public const string PLAYER_EXISTS = "PlayerExists";
    public const string PLAYER_HEALTH = "PlayerHealth";
    public const string PLAYER_ATTACK = "PlayerAttack";
    public const string PLAYER_EXPERIENCE = "PlayerExperience";
    public const string PLAYER_LEVEL = "PlayerLevel";
    public const string PLAYER_COLOR_INDEX = "PlayerColorIndex";
    public const string PLAYER_INDEX = "PlayerIndex";
    public const string BATTLES_PLAYED = "BattlesPlayed";

    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    #region Events
    public const string EVENT_DISPLAY_UI = "Event.DisplayUI";
    public const string EVENT_PLAYER_SELECT = "Event.SelectPlayer";
    public const string EVENT_RELAY_PLAYER_INFO = "Event.RelayPlayerInfo";
    public const string EVENT_SHOW_PLAYER_INFO = "Event.ShowPlayerInfo";
    public const string EVENT_MAX_PLAYERS_SELECTED = "Event.MaxPlayersSelected";
    public const string EVENT_START_BATTLE = "Event.StartBattle";
    public const string EVENT_START_PLAYER_MOVE = "Event.StartPlayerMove";
    public const string EVENT_START_ENEMY_MOVE = "Event.StartEnemyMove";
    public const string EVENT_PLAYER_SELECTED_FOR_ATTACK = "Event.PlayerSelectedForAttack";
    public const string EVENT_GAME_OVER_PLAYER_WIN = "Event.GameOverPlayerWin";
    public const string EVENT_GAME_OVER_PLAYER_LOSE = "Event.GameOverPlayerLose";
    public const string EVENT_CHECK_ALL_PLAYERS_HEALTH = "Event.CheckAllPlayersHealth";
    #endregion
}
