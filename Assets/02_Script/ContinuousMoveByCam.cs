using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ContinuousMoveByCam : MonoBehaviour
{
    [SerializeField] InputAction stickMoveAction;
    [SerializeField] InputAction hideBtnAction;
    [SerializeField] Camera playerCam;
    [SerializeField] Transform camOffset, upPoint, downPoint;
    [SerializeField] float camMoveSpeed = 1;
    

    Vector3 rotateVec;
    bool isUp = false;

    private void Awake()
    {


        stickMoveAction.performed += MoveAction_performed;
        stickMoveAction.Enable();

        hideBtnAction.performed += HideBtnAction_performed;
        hideBtnAction.started += HideBtnAction_started;
        hideBtnAction.canceled += HideBtnAction_canceled;
        hideBtnAction.Enable();


    }

    private void Update()
    {
        if (isUp)
        {
            if (Vector3.Distance(camOffset.position, upPoint.position) > 0.1)
            {
                camOffset.Translate(Vector3.up * Time.deltaTime* camMoveSpeed);
            }
            else
                camOffset.position = upPoint.position;
        }
        else
        {
            if (Vector3.Distance(camOffset.position, downPoint.position) > 0.1)
            {
                camOffset.Translate(-Vector3.up * Time.deltaTime*camMoveSpeed);
            }
            else
                camOffset.position = downPoint.position;
        }
    }

    private void HideBtnAction_canceled(InputAction.CallbackContext obj)
    {
        isUp = false;
    }

    private void HideBtnAction_started(InputAction.CallbackContext obj)
    {
        isUp = true;
    }

    private void HideBtnAction_performed(InputAction.CallbackContext obj)
    {
        //if (obj.ReadValueAsButton())
        //{
        //    print("btn");
        //}
        //else
        //{
        //    print("not");
        //}
        //CamUpDown();
    }

    public void CamUpDown()
    {
        if (isUp)
        {
            isUp = false;
            camOffset.transform.position -= Vector3.up;
        }
        else
        {
            isUp = true;
            camOffset.transform.position += Vector3.up;
        }
    }


    private void MoveAction_performed(InputAction.CallbackContext obj)
    {
        Vector3 temp = (playerCam.transform.forward * obj.ReadValue<Vector2>().y + playerCam.transform.right * obj.ReadValue<Vector2>().x) * Time.deltaTime;
        XRplayer.Instance.transform.Translate(temp.x*Vector3.right+temp.z*Vector3.forward);

    }


}
