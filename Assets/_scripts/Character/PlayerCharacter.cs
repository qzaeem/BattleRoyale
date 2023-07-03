using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionInput))]
public class PlayerCharacter : Character, ISelectableCharacter
{
    public void Deselect()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        bodyRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", _color);
        bodyRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void Select(Color selectedColor)
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        bodyRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", selectedColor);
        bodyRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    protected override void AttackHasEnded()
    {
        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_START_ENEMY_MOVE));
    }

    protected override void GetTargetTransform()
    {
        TargetTranform = Globals.targetEnemyTransform;
    }

    protected override void SendGameOverEvent()
    {
        Events.Observer.DispatchCustomEvent(new Events.GenericEvent(Constants.EVENT_CHECK_ALL_PLAYERS_HEALTH));
    }
}
