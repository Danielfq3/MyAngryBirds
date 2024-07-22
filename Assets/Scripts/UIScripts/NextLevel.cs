using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
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
    
    private void ShowWinMenu(bool _bool)
    {
        foreach (Transform transform in gameObject.transform)
        {
            transform.gameObject.SetActive(_bool);
        }
    }

    private void HideUI()
    {
        _ui.gameObject.SetActive(false);
        print(FindAllPigs().Length);
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
        if (FindAllPigs().Length > 0)
        {
            if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy == true)
            {
                ShowWinMenu(false);
                ShowUI();
            }
            return;
        }
        ShowWinMenu(true);
        HideUI();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
