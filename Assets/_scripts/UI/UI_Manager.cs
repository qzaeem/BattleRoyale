using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Menu selectionMsg, startButton, attackButton;
    [SerializeField] private PlayerInfoMenu playerInfoMenu;
    [SerializeField] private GameObject GameoverPanel;
    [SerializeField] private TextMeshProUGUI statusTMP;

    public void DisplaySelectionMsg(bool open)
    {
        if (open) selectionMsg.Open();
        else selectionMsg.Close();
    }

    public void DisplayStartButton(bool open)
    {
        if (open) startButton.Open();
        else startButton.Close();
    }

    public void DisplayAttackButton(bool open)
    {
        if (open) attackButton.Open();
        else attackButton.Close();
    }

    public void DisplayPlayerInfo(bool open, PlayerData playerData)
    {
        if (open) playerInfoMenu.OpenPlayerInfo(playerData);
        else playerInfoMenu.Close();
    }

    public void StartBattle()
    {
        DisplaySelectionMsg(false);
        DisplayStartButton(false);
        DisplayPlayerInfo(false, null);

        GameManager.instance.StartBattleMode();
    }

    public void StartAttack()
    {
        DisplayAttackButton(false);
        GameManager.instance.StartAttack();
    }

    public void ShowGameoverPanel(bool isWin)
    {
        StartCoroutine("ShowGameover", isWin);
    }

    private IEnumerator ShowGameover(bool isWin)
    {
        yield return new WaitForSeconds(4);

        statusTMP.text = isWin ? "YOU WIN" : "YOU LOSE";
        GameoverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
