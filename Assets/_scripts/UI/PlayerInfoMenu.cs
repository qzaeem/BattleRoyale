using UnityEngine;
using TMPro;

public class PlayerInfoMenu : Menu
{
    [SerializeField] TextMeshProUGUI nameTMP, healthTMP, attackTMP, experienceTMP, levelTMP;

    public void OpenPlayerInfo(PlayerData playerData)
    {
        nameTMP.text = playerData.name;
        healthTMP.text = playerData.health.ToString("0.00");
        attackTMP.text = playerData.attackDamage.ToString("0.00");
        experienceTMP.text = playerData.experience.ToString();
        levelTMP.text = playerData.level.ToString();

        GoOutsideScreen();
        Open();
    }
}
