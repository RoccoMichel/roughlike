using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogicScript : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNextInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadPreviousInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadSceneByNumber(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadSceneByPlayerPrefInt(string tag)
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(tag));
    }
    public void LoadSceneByPlayerPrefString(string tag)
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(tag));
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
