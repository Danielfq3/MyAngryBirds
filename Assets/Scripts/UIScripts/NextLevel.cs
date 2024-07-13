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
    
    private void ShowButton()
    {
        if (CheckForPigs().Length == 0)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        ShowButton();
    }

}
