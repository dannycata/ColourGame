using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontrarObjetosConScript : MonoBehaviour
{
    public string scriptNameToFind = "NombreDelScript";

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        List<GameObject> objectsWithScript = new List<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent(scriptNameToFind) != null)
            {
                objectsWithScript.Add(obj);
            }
        }
        foreach (GameObject obj in objectsWithScript)
        {
            Debug.Log("Objeto con el script " + scriptNameToFind + ": " + obj.name);
        }
    }
}
