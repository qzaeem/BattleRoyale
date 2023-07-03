using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static PlayerData GetPlayerData(int index)
    {
        return new PlayerData
        {
            health = PlayerPrefs.GetFloat(Constants.PLAYER_HEALTH + index.ToString()),
            attackDamage = PlayerPrefs.GetFloat(Constants.PLAYER_ATTACK + index.ToString()),
            experience = PlayerPrefs.GetInt(Constants.PLAYER_EXPERIENCE + index.ToString()),
            level = PlayerPrefs.GetInt(Constants.PLAYER_LEVEL + index.ToString()),
            colorIndex = PlayerPrefs.GetInt(Constants.PLAYER_COLOR_INDEX + index.ToString()),
            playerIndex = PlayerPrefs.GetInt(Constants.PLAYER_INDEX + index.ToString())
        };
    }

    public static List<PlayerData> GetAllPlayersData()
    {
        List<PlayerData> playersData = new List<PlayerData>();

        for(int i = 0; i < Constants.MAX_PLAYERS; i++)
        {
            if(PlayerPrefs.HasKey(Constants.PLAYER_EXISTS + i.ToString()))
            {
                playersData.Add(GetPlayerData(i));
            }
        }

        return playersData;
    }

    public static PlayerData CreateNewPlayerData(PlayerData playerData)
    {
        for (int i = 0; i < Constants.MAX_PLAYERS; i++)
        {
            if (PlayerPrefs.GetInt(Constants.PLAYER_EXISTS + i.ToString()) == 1)
            {
                continue;
            }

            PlayerPrefs.SetInt(Constants.PLAYER_EXISTS + i.ToString(), 1);
            playerData.playerIndex = i;
            playerData.colorIndex = i;
            SavePlayerData(playerData);
            return playerData;
        }

        return null;
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        PlayerPrefs.SetFloat(Constants.PLAYER_HEALTH + playerData.playerIndex.ToString(), playerData.health);
        PlayerPrefs.SetFloat(Constants.PLAYER_ATTACK + playerData.playerIndex.ToString(), playerData.attackDamage);
        PlayerPrefs.SetInt(Constants.PLAYER_EXPERIENCE + playerData.playerIndex.ToString(), playerData.experience);
        PlayerPrefs.SetInt(Constants.PLAYER_LEVEL + playerData.playerIndex.ToString(), playerData.level);
        PlayerPrefs.SetInt(Constants.PLAYER_COLOR_INDEX + playerData.playerIndex.ToString(), playerData.colorIndex);
        PlayerPrefs.SetInt(Constants.PLAYER_INDEX + playerData.playerIndex.ToString(), playerData.playerIndex);
    }

    public static void SaveAllPlayersData(List<PlayerData> playersData)
    {
        playersData.ForEach((playerData) => SavePlayerData(playerData));
    }

    public static void BattleEnded()
    {
        int battlesPlayed = PlayerPrefs.GetInt(Constants.BATTLES_PLAYED, 0) + 1;
        PlayerPrefs.SetInt(Constants.BATTLES_PLAYED, battlesPlayed);

        if (battlesPlayed < 5) return;

        battlesPlayed = 0;
        PlayerPrefs.SetInt(Constants.BATTLES_PLAYED, battlesPlayed);

        CreateNewPlayerData(new PlayerData
        {
            health = Globals.playerColors.health,
            attackDamage = Globals.playerColors.attackDamage,
            experience = Globals.playerColors.experience,
            level = Globals.playerColors.level
        });
    }

    public static void RemovePlayerData(int playerIndex)
    {
        // Code to remove player save data
    }
}