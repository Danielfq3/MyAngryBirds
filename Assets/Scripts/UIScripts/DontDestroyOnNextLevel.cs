using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class DontDestroyOnNextLevel : MonoBehaviour
{

    private static List<string> persistentObjectNames = new List<string>();

    void Awake()
    {
        string objectName = gameObject.name;

        if (!persistentObjectNames.Contains(objectName))
        {
            persistentObjectNames.Add(objectName);
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
