using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TelePadMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivate(ActivateEventArgs args)
    {
        print(((XRRayInteractor)args.interactorObject).gameObject.name);

        //if(args.interactorObject.)
        //{
        //    ((XRRayInteractor)args.interactorObject).blendVisualLinePoints = false;
        //}
    }

    public void OnDeActivate(DeactivateEventArgs args)
    {

    }
}
