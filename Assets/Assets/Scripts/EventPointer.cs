using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPointer : MonoBehaviour
{
    public int v_timeToAction = 3;
    public bool v_Active = false;

    public string v_function;
    public string[] v_data;

    GvrReticlePointer v_point;
   

    void Start()
    {
        v_point = GameObject.Find("GvrReticlePointer").GetComponent<GvrReticlePointer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator c_actionTime(string _function, string[] _data)
    {
        yield return new WaitForSeconds(1.0f);
        switch (v_timeToAction)
        {
            case 1:
                {
                    v_point.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                break;
            case 2:
                {
                    v_point.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                break;
            case 3:
                {
                    v_point.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                break;
        };

        if (v_timeToAction > 0)
            v_timeToAction--;
        if(v_timeToAction == 0 && v_Active == false)
        {
            Debug.Log("Action");
            v_Active = true;
            EventFN.LoadFunctions(_function, _data);
        }

        StartCoroutine(c_actionTime(_function, _data));
    }

    public void fn_action()
    {
        v_point.GetComponent<MeshRenderer>().material.color = Color.green;
        StartCoroutine(c_actionTime(v_function, v_data));
        //corutina acciones
    }

    public void fn_exitAcctions()
    {
        StopAllCoroutines();
        v_timeToAction = 3;
        v_Active = false;
        v_point.GetComponent<MeshRenderer>().material.color = Color.magenta;
    }
}
