using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        Events.Observer.RegisterCustomEventAction(Events.NextMoveEvent.Name, NextMoveAction);
        Events.Observer.RegisterCustomEventAction(Events.NextMoveEvent.Name, NextMoveAction2);
        StartCoroutine(TestEvents());
    }

    IEnumerator TestEvents()
    {
        yield return new WaitForSeconds(5);

        Events.Observer.DispatchCustomEvent(new Events.NextMoveEvent(true));
        Events.Observer.RemoveCustomEventAction(Events.NextMoveEvent.Name, NextMoveAction);

        print("Removed Now!");

        yield return new WaitForSeconds(3);

        Events.Observer.DispatchCustomEvent(new Events.NextMoveEvent(false));
    }

    void NextMoveAction(Events.Event customEvent)
    {
        print("Next Move Executed with boolean " + ((Events.NextMoveEvent)customEvent).isPlayersTurn);
    }

    void NextMoveAction2(Events.Event customEvent)
    {
        print("Next Move 2 Executed with boolean " + ((Events.NextMoveEvent)customEvent).isPlayersTurn);
    }
}
