using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterObj : MonoBehaviour
{
    public static GameObject g_objToMove;
    public static GameObject g_objToAct;

    RaycastHit r_hit;

    private void Update()
    {
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out r_hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * r_hit.distance, Color.red);
            if (r_hit.collider.gameObject.CompareTag("ToMove"))
                g_objToMove = r_hit.collider.gameObject;

            if (r_hit.collider.gameObject.CompareTag("ToAct"))
                g_objToAct = r_hit.collider.gameObject;
        }
    }
}
