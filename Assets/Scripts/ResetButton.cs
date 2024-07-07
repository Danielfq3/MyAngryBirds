using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void ResetScene()
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Load the current scene
        SceneManager.LoadScene(currentSceneName);
    }
}
