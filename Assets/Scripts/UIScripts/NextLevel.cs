using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
/*using UnityEditor.VersionControl;*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _ui;

    private GameObject[] FindAllPigs()
    {
        return GameObject.FindGameObjectsWithTag("Pig");
    }

    private GameObject[] FindAllBirds()
    {
        return GameObject.FindGameObjectsWithTag("Bird");
    }
    
    private void ShowWinMenu(bool _bool)
    {
        foreach (Transform transform in gameObject.transform)
        {
            transform.gameObject.SetActive(_bool);
        }
    }

    private void ShowWinMenuExeptNextLevelButton(bool status)
    {
        foreach (Transform transform in gameObject.transform)
        {
            if (transform.gameObject.name == "NextLevel")
            {
                continue;
            }
            transform.gameObject.SetActive(status);
        }
    }

    private void HideUI()
    {
        _ui.gameObject.SetActive(false);
    }

    private void ShowUI()
    {
        _ui.gameObject.SetActive(true);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Update()
    {
        if (FindAllPigs().Length < 1)
        {
            ShowWinMenu(true);
            HideUI();
            return;
        }
        if (FindAllBirds().Length < 1)
        {
            ShowWinMenuExeptNextLevelButton(true);
            HideUI();
            return;
        }
        if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            ShowWinMenu(false);
            ShowUI();
        }
    }
}
