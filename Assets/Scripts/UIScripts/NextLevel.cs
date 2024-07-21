using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _ui;

    private GameObject[] CheckForPigs()
    {
        return GameObject.FindGameObjectsWithTag("Pig");
    }
    
    private void ShowButton()
    {
        foreach (Transform transform in gameObject.transform)
        {
            transform.gameObject.SetActive(true);
        }
    }

    private void HideUI()
    {
        _ui.gameObject.SetActive(false);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (CheckForPigs().Length == 0)
        {
            ShowButton();
            HideUI();
        }
    }

}
