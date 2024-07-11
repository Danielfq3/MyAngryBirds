using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevel : MonoBehaviour
{
    private GameObject[] CheckForPigs()
    {
        return GameObject.FindGameObjectsWithTag("Pig");
    }
    
    private void GoToNextLevel()
    {
        if (CheckForPigs().Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Update()
    {
        GoToNextLevel();
    }

}
