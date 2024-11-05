using UnityEngine;

public class PauseMenuManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] KeyCode pauseKey;

    void Update()
    {
        if (pauseMenu.activeInHierarchy == true && Input.GetKeyDown(pauseKey))
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
