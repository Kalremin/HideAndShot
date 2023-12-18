using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ContinuousMoveByCam : ContinuousMoveProviderBase
{
    [SerializeField] InputActionProperty handMoveAction;
    [SerializeField] Camera playerCam;

    public InputActionProperty HandMoveAction
    {
        get => handMoveAction;
        set => SetInputActionProperty(ref handMoveAction, value);
    }

    protected void OnEnable()
    {
        handMoveAction.EnableDirectAction();
    }

    protected void OnDisable()
    {
        handMoveAction.DisableDirectAction();
    }


    protected override Vector2 ReadInput()
    {
        var handMoveValue = 
            handMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        return playerCam.transform.forward * handMoveValue.y + playerCam.transform.right * handMoveValue.x;
        
    }
    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }

    //[SerializeField] InputAction stickMoveAction;
    //[SerializeField] InputAction hideBtnAction;
    //[SerializeField] Transform camOffset, upPoint, originPoint;
    //[SerializeField] float camMoveSpeed = 1;


    //Vector3 rotateVec;
    //bool isUp = false;

    //private void Awake()
    //{


    //    stickMoveAction.performed += MoveAction_performed;
    //    stickMoveAction.Enable();

    //    hideBtnAction.started += HideBtnAction_started;
    //    hideBtnAction.canceled += HideBtnAction_canceled;
    //    hideBtnAction.Enable();


    //}

    //private void Update()
    //{
    //    if (isUp)
    //    {
    //        if (Vector3.Distance(camOffset.position, upPoint.position) > 0.1)
    //        {
    //            camOffset.Translate(Vector3.up * Time.deltaTime* camMoveSpeed);
    //        }
    //        else
    //            camOffset.position = upPoint.position;
    //    }
    //    else
    //    {
    //        if (Vector3.Distance(camOffset.position, originPoint.position) > 0.1)
    //        {
    //            camOffset.Translate(-Vector3.up * Time.deltaTime*camMoveSpeed);
    //        }
    //        else
    //            camOffset.position = originPoint.position;
    //    }
    //}

    //private void HideBtnAction_canceled(InputAction.CallbackContext obj)
    //{
    //    isUp = false;
    //}

    //private void HideBtnAction_started(InputAction.CallbackContext obj)
    //{
    //    isUp = true;
    //}


    //private void MoveAction_performed(InputAction.CallbackContext obj)
    //{
    //    Vector3 temp = (playerCam.transform.forward * obj.ReadValue<Vector2>().y + playerCam.transform.right * obj.ReadValue<Vector2>().x) * Time.deltaTime;
    //    XRplayer.Instance.transform.Translate(temp.x*Vector3.right+temp.z*Vector3.forward);

    //}


    //protected override Vector2 ReadInput()
    //{
    //    Vector3 temp = (playerCam.transform.forward * stickMoveAction.ReadValue<Vector2>().y + playerCam.transform.right * stickMoveAction.ReadValue<Vector2>().x) * Time.deltaTime;
    //    return temp.x * Vector3.right + temp.z * Vector3.forward;
    //}
}
