using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFN : MonoBehaviour
{

    static public EventFN instance;
    static bool move = false;
    static GameObject toClose;

    void Awake()
    {
        instance = this;
    }
    
    public static void LoadFunctions(string _function, string [] _data)
    {
        switch (_function)
        {
            case "Sumar":
                {
                    Debug.Log("Sumar :: " + (int.Parse(_data[0] + int.Parse(_data[1]))));
                }
                break;
            case "Click":
                {
                    Debug.Log("Click");
                }
                break;
            case "Teleport":
                {
                    GameObject.Find("Pivote").transform.position = new Vector3(RaycasterObj.g_objToMove.transform.position.x, 1, RaycasterObj.g_objToMove.transform.position.z);
                }
                break;

            case "Move":
                {
                    if (!move)
                    {
                        move = true;
                        instance.StartCoroutine(c_move());
                    }
                }
                break;

            case "Interaccion":
                {
                    GameObject tmp = RaycasterObj.g_objToAct.transform.GetChild(0).gameObject;
                    toClose = tmp;
                    toClose.SetActive(true);
                    toClose.transform.LookAt(GameObject.Find("Pivote").transform);
                    toClose.transform.Rotate(Vector3.up,180);
                }
                break;
            case "Rotar":
                instance.StartCoroutine(c_rotatorObj(2));
                break;

            default:
                {
                    Debug.LogError("404 "+_function+" no existe");
                }
                break;
        }
    }

    static IEnumerator c_close()
    {
        yield return new WaitForSeconds(0.6f);
        toClose.SetActive(false);
    }

    static IEnumerator c_rotatorObj(float _rotacion)
    {
        instance.StartCoroutine(c_close());

        GameObject objtmp = RaycasterObj.g_objToAct;
        float startRotacion = RaycasterObj.g_objToAct.transform.eulerAngles.y;
        float endRotacion = startRotacion + 360.0f;
        float t = 0.0f;
        while(t < _rotacion)
        {
            t += Time.deltaTime;
            float yRotacion = Mathf.Lerp(startRotacion, endRotacion, t / _rotacion) % 360.0f;
            objtmp.transform.eulerAngles = new Vector3(objtmp.transform.eulerAngles.x, yRotacion, objtmp.transform.eulerAngles.z);
            yield return null;
        }
        toClose.SetActive(true);
    }

    static IEnumerator c_move()
    {
        float elapsedTime = 0.0f;
        float waitTime = 50.0f;

        GameObject objTmp = GameObject.Find("Pivote");
        Transform toMove = RaycasterObj.g_objToMove.transform;
        Vector3 toUse = new Vector3(toMove.position.x, 1, toMove.position.z);

        while(elapsedTime < waitTime)
        {
            objTmp.transform.position = Vector3.Lerp(objTmp.transform.position, toUse, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;

            if (Vector3.Distance(objTmp.transform.position, toUse) <= 0.1f)
                break;
            yield return null;
        }

        objTmp.transform.position = toUse;
        yield return new WaitForSeconds(0.5f);
        move = false;
    }
}
