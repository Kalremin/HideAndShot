using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportInteractorSwap : MonoBehaviour
{
    [SerializeField]
    InputAction triggerController;

    [SerializeField]
    XRRayInteractor activeInteractor;

    private void OnEnable()
    {
        triggerController.started += TriggerController_started;
        triggerController.canceled += TriggerController_canceled;
        triggerController.Enable();
    }
    private void OnDisable()
    {
        triggerController.started -= TriggerController_started;
        triggerController.canceled -= TriggerController_canceled;
        triggerController.Disable();
    }

    

    private void TriggerController_canceled(InputAction.CallbackContext obj)
    {
        if (activeInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitinfo))
        {
            XRplayer.Instance.SetMovePosition(hitinfo.transform.position);
        }

        activeInteractor.gameObject.SetActive(false);
        
    }

    private void TriggerController_started(InputAction.CallbackContext obj)
    {


        activeInteractor.gameObject.SetActive(true);
        
    }

}
