using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class FastTurnRoundRight : ContinuousTurnProviderBase
{
    [SerializeField]
    InputActionProperty handTurnAction;

    Vector2 handValue;

    bool isFastTurn = false;
    float currentTime = 0;

    [SerializeField]
    float stickPivot = 0.85f;

    [SerializeField]
    float fastTurnTime = 0.2f;
    [SerializeField]
    float fastTurnSpeed = 3f;

    bool isDelay = false;
    bool isSign = true;
    public InputActionProperty HandTurnAction
    {
        get => handTurnAction;
        set => SetInputActionProperty(ref handTurnAction, value);
    }

    protected void OnEnable()
    {
        handTurnAction.EnableDirectAction();
    }
    protected void OnDisable()
    {
        handTurnAction.DisableDirectAction();
    }

    private void FixedUpdate()
    {

        if (isFastTurn)
            currentTime += Time.deltaTime;

        handValue = handTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        if (handValue.Equals(Vector2.zero))
            isDelay = false;

    }

    protected override Vector2 ReadInput()
    {

        if (isDelay)
            return Vector2.zero;

        if (isFastTurn)
        {

            if (currentTime >= fastTurnTime)
            {
                isFastTurn = false;
                currentTime = 0;
                isDelay = true;
            }

            return new Vector2(isSign ? fastTurnSpeed : -fastTurnSpeed, 0);
        }
        else
        {
            if (Mathf.Abs(handValue.x) > stickPivot)
            {
                isFastTurn = true;
                if (handValue.x > 0)
                    isSign = true;
                else if (handValue.x < 0)
                    isSign = false;
                return new Vector2(isSign ? fastTurnSpeed : -fastTurnSpeed, 0);
            }
            else
            {
                return handValue;
            }

        }



    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }

}