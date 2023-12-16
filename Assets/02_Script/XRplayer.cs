using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRplayer : MonoBehaviour
{
    static XRplayer instance;
    public static XRplayer Instance => instance;

    bool isMove = false;
    Vector3 movePosition;
    Rigidbody rb;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveDistance = 0.1f;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic)
        {
            
            if (Vector3.Distance(transform.position, movePosition) > moveDistance)
                transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime* moveSpeed);
            else
                rb.isKinematic = false;
        }
    }

    public void SetMovePosition(Vector3 val)
    {
        movePosition = val;
        rb.isKinematic = true;

    }
}
