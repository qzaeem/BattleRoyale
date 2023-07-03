using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    private enum MovementAxis { Horizontal, Vertical }

    [SerializeField] private MovementAxis movementAxis;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 screenPosition;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        var anchoredPos = rectTransform.anchoredPosition;
        screenPosition = anchoredPos;
        rectTransform.anchoredPosition = new Vector2(movementAxis == MovementAxis.Horizontal ? 0 : anchoredPos.x,
            movementAxis == MovementAxis.Vertical ? 0 : anchoredPos.y);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    protected void GoOutsideScreen()
    {
        var anchoredPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(movementAxis == MovementAxis.Horizontal ? 0 : anchoredPos.x,
            movementAxis == MovementAxis.Vertical ? 0 : anchoredPos.y);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void Open()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        rectTransform.DOKill();
        rectTransform.DOAnchorPos(screenPosition, 0.3f);
    }

    public virtual void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        rectTransform.DOKill();
        var anchoredPos = rectTransform.anchoredPosition;
        var targetPos = new Vector2(movementAxis == MovementAxis.Horizontal ? 0 : anchoredPos.x,
            movementAxis == MovementAxis.Vertical ? 0 : anchoredPos.y);
        rectTransform.DOAnchorPos(targetPos, 0.3f);
    }
}
