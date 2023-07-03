using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerColorSO playerColors;
    [SerializeField] private PlayerCharacter playerCharacterPrefab;
    [SerializeField] private Camera canvasCamera;

    private Dictionary<int, PlayerCharacter> _players = new Dictionary<int, PlayerCharacter>();
    private Dictionary<int, PlayerCharacter> _playersForBattle = new Dictionary<int, PlayerCharacter>();
    private Queue<PlayerCharacter> _selectedPlayers = new Queue<PlayerCharacter>();
    private int _currentPlayerIndex;

    private void Start()
    {
        Globals.playerColors = playerColors;
        GetPlayersDataAndSpawn();
    }

    private void OnEnable()
    {
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_PLAYER_SELECT, SelectPlayerForBattle);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_RELAY_PLAYER_INFO, RelayPlayerInfo);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_START_BATTLE, ArrangePlayersForBattle);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_START_PLAYER_MOVE, StartPlayerAttack);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_CHECK_ALL_PLAYERS_HEALTH, CheckAllPlayersHealth);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_WIN, PlayerWon);
    }

    private void OnDisable()
    {
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_PLAYER_SELECT, SelectPlayerForBattle);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_RELAY_PLAYER_INFO, RelayPlayerInfo);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_START_BATTLE, ArrangePlayersForBattle);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_PLAYER_SELECT, SelectPlayerForAttack);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_START_PLAYER_MOVE, StartPlayerAttack);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_CHECK_ALL_PLAYERS_HEALTH, CheckAllPlayersHealth);
        Events.Observer.RemoveCustomEventAction(Constants.EVENT_GAME_OVER_PLAYER_WIN, PlayerWon);
    }

    private void SelectPlayerForBattle(Events.Event eventInfo)
    {
        var playerSelectionEvent = (Events.OneIntEvent)eventInfo;
        int playerIndex = playerSelectionEvent.intVal;

        if (_playersForBattle.ContainsKey(playerIndex)) return;

        if (_selectedPlayers.Count >= Constants.MIN_PLAYERS)
        {
            var firstElement = _selectedPlayers.Dequeue();
            firstElement.Deselect();
            _playersForBattle.Remove(((PlayerData)firstElement.Data).playerIndex);
        }

        SelectPlayerForBattle(playerIndex);
    }

    private void SelectPlayerForBattle(int playerIndex)
    {
        var player = _players[playerIndex];
        _playersForBattle.Add(playerIndex, player);
        _selectedPlayers.Enqueue(player);
        player.Select(Globals.SELECT_COLOR);

        if(_playersForBattle.Count >= 3)
        {
            Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_MAX_PLAYERS_SELECTED));
        }
    }

    private void SelectPlayerForAttack(Events.Event eventInfo)
    {
        var playerSelectionEvent = (Events.OneIntEvent)eventInfo;
        int playerIndex = playerSelectionEvent.intVal;

        _playersForBattle[_currentPlayerIndex].Deselect();
        _currentPlayerIndex = playerIndex;
        _playersForBattle[_currentPlayerIndex].Select(Globals.SELECT_COLOR);

        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_PLAYER_SELECTED_FOR_ATTACK));
    }

    private void RelayPlayerInfo(Events.Event eventInfo)
    {
        var oneIntEvent = (Events.OneIntEvent)eventInfo;
        int playerIndex = oneIntEvent.intVal;

        Events.Observer.DispatchCustomEvent(
            new Events.PlayerInfoEvent(Constants.EVENT_SHOW_PLAYER_INFO, (PlayerData)_players[playerIndex].Data));
    }

    private Color GetColorAtIndex(int index)
    {
        if (index >= playerColors.colors.Length) return playerColors.colors[0];
        else return playerColors.colors[index];
    }

    private void GetPlayersDataAndSpawn()
    {
        var playersData = GameData.GetAllPlayersData();

        while(playersData.Count < 3)
        {
            playersData.Add(GameData.CreateNewPlayerData(new PlayerData
            {
                health = playerColors.health,
                attackDamage = playerColors.attackDamage,
                experience = playerColors.experience,
                level = playerColors.level
            }));
        }

        SpawnPlayers(playersData);
    }

    private void SpawnPlayers(List<PlayerData> playersData)
    {
        for(int i = 0; i < playersData.Count; i++)
        {
            var playerCharacter = Instantiate(playerCharacterPrefab, transform);
            var playerTransform = playerCharacter.transform;
            playerTransform.localPosition = Vector3.zero;
            playerTransform.position = new Vector3(-Constants.MAX_X_POSITION + i,
                playerTransform.position.y, playerTransform.position.z);
            playerTransform.forward = -Vector3.forward;
            var playerData = playersData[i];
            playerData.name = "Player" + (playerData.playerIndex + 1);
            playerTransform.GetComponent<SelectionInput>().CharacterIndex = playerData.playerIndex;
            playerCharacter.Init(playerData, GetColorAtIndex(playerData.colorIndex), true);
            _players.Add(playerData.playerIndex, playerCharacter);

            playerCharacter.healthCanvas.worldCamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
        }

        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_DISPLAY_UI));
    }

    private void ArrangePlayersForBattle(Events.Event eventInfo)
    {
        int posIndex = 0;
        foreach (var kvp in _players)
        {
            if (_playersForBattle.ContainsKey(kvp.Key))
            {
                var player = kvp.Value;
                player.Deselect();
                player.transform.localPosition = Vector3.forward * (posIndex - 1);
                posIndex++;
                player.transform.forward = Vector3.right;
                player.ShowHealthBar();
                player.InitializeValues();
                continue;
            }

            kvp.Value.gameObject.SetActive(false);
        }

        Events.Observer.RemoveCustomEventAction(Constants.EVENT_PLAYER_SELECT, SelectPlayerForBattle);
        Events.Observer.RegisterCustomEventAction(Constants.EVENT_PLAYER_SELECT, SelectPlayerForAttack);
    }

    private void StartPlayerAttack(Events.Event eventInfo)
    {
        var player = _playersForBattle[_currentPlayerIndex];
        player.Deselect();
        player.StartAttack();

        var index = Random.Range(0, _playersForBattle.Count);
        Globals.targetPlayerTransform =
            _playersForBattle.ElementAt(index).Value.transform;

        //float minHealth = float.MaxValue;
        //foreach(var ch in _playersForBattle.Values)
        //{
        //    if(ch.Health < minHealth)
        //    {
        //        minHealth = ch.Health;
        //        Globals.targetPlayerTransform = ch.transform;
        //    }
        //}
    }

    private void CheckAllPlayersHealth(Events.Event eventInfo)
    {
        if (!_playersForBattle.Values.All(p => p.Health == 0)) return;

        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_GAME_OVER_PLAYER_LOSE));
    }

    private void PlayerWon(Events.Event eventInfo)
    {
        foreach(var player in _playersForBattle.Values)
        {
            if (player.Health > 0)
            {
                var playerData = (PlayerData)player.Data;
                playerData.experience++;

                if(playerData.experience % 5 == 0)
                {
                    playerData.level++;
                    playerData.attackDamage += playerColors.attackDamage * 0.1f;
                    playerData.health += playerColors.health * 0.1f;
                }
            }
        }

        var playersData = _playersForBattle.Values.Select(p => (PlayerData)p.Data).ToList();
        GameData.SaveAllPlayersData(playersData);
    }
}
