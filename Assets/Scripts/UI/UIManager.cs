using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ReloadScene()
    {
        ResetTime();
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
    public void LoadScene(string sceneName)
    {
        ResetTime();
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    private void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
