#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneButton : EditorWindow
{
    [MenuItem("Window/Reset Scene Button")]
    public static void ShowWindow()
    {
        GetWindow<ResetSceneButton>("Reset Scene Button");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Reset Scene"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
#endif