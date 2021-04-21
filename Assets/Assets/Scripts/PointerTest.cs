using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerTest : MonoBehaviour
{
    public GameObject esfera;


    public void EnterPointer()
    {
        Debug.Log("Enter");
        esfera.SetActive(true);
    }

    public void ExitPointer()
    {
        Debug.Log("Exit");
        esfera.SetActive(false);
    }

}

