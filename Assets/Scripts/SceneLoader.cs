using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// Scene loader class to manage scenes
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadEndGame()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
