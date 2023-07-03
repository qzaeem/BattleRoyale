using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter enemyCharacterPrefab;
    [SerializeField] private float health = 100;
    [SerializeField] private float attackDamage = 30;

    private EnemyCharacter _enemyCharacter;

    private void OnEnable()
    {
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_START_BATTLE, SpawnEnemy);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_START_ENEMY_MOVE, StartEnemyAttack);
    }

    private void OnDisable()
    {
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_START_BATTLE, SpawnEnemy);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_START_ENEMY_MOVE, StartEnemyAttack);
    }

    private void SpawnEnemy(Events.Event eventInfo)
    {
        _enemyCharacter = Instantiate(enemyCharacterPrefab, transform);
        var enemyTransform = _enemyCharacter.transform;
        enemyTransform.localPosition = Vector3.zero;
        enemyTransform.forward = -Vector3.right;
        var enemyData = new EnemyData();
        enemyData.health = health;
        enemyData.attackDamage = attackDamage;
        _enemyCharacter.Init(enemyData, Globals.ENEMY_COLOR, false);
        _enemyCharacter.InitializeValues();
        _enemyCharacter.ShowHealthBar();
        _enemyCharacter.healthCanvas.worldCamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();

        Globals.targetEnemyTransform = enemyTransform;
    }

    private void StartEnemyAttack(Events.Event eventInfo)
    {
        if (Globals.gameState == Globals.GameState.UI || Globals.gameState == Globals.GameState.GameOver)
            return;

        Globals.gameState = Globals.GameState.EnemyMove;
        _enemyCharacter.StartAttack();
    }
}
