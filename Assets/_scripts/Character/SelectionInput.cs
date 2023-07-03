using System.Collections;
using UnityEngine;

public class SelectionInput : MonoBehaviour
{
    public int CharacterIndex { get; set; }
    bool isSelected;

    private void Start()
    {
        isSelected = false;
    }

    private void OnMouseDown()
    {
        if (Globals.gameState == Globals.GameState.EnemyMove || Globals.gameState == Globals.GameState.GameOver)
            return;

        isSelected = true;
        Events.Observer.DispatchCustomEvent(new Events.OneIntEvent(Constants.EVENT_PLAYER_SELECT, CharacterIndex));

        if (Globals.gameState == Globals.GameState.UI)
        {
            StartCoroutine("HoldClick");
        }
    }

    private void OnMouseUp()
    {
        isSelected = false;
        StopCoroutine("HoldClick");
    }

    private void OnMouseExit()
    {
        isSelected = false;
        StopCoroutine("HoldClick");
    }

    private IEnumerator HoldClick()
    {
        yield return new WaitForSeconds(3);

        if (isSelected)
        {
            Events.Observer.DispatchCustomEvent(new Events.OneIntEvent(Constants.EVENT_RELAY_PLAYER_INFO, CharacterIndex));
        }
    }
}
