using UnityEngine;

public class PauseMenuManagerScript : MonoBehaviour
{
    public Canvas pauseMenu;
    [SerializeField] KeyCode pauseKey;

    void Update()
    {
        if (pauseMenu.gameObject.activeInHierarchy == true && Input.GetKeyDown(pauseKey))
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
