using UnityEngine;

public static class Globals
{
    public static Transform targetEnemyTransform;
    public static Transform targetPlayerTransform;

    public static PlayerColorSO playerColors;

    public enum GameState { UI, PlayerMove, EnemyMove, GameOver }
    public static GameState gameState;

    public static Color SELECT_COLOR = Color.yellow;
    public static Color ENEMY_COLOR = Color.red;
}
