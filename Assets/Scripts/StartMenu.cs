using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [Header("Scenes | INCLUDED NUMEBRS")]
    [Tooltip("Build Index of Tutorial Scene")]
    public int tutorialScene = 1;
    [Tooltip("Build Index Range of Levels (min&max)")]
    public Vector2 levels = Vector2.one;

    [Header("References")]
    [SerializeField] TMP_Text Jubs;
    [SerializeField] TMP_Text Runs;

    private void Start()
    {
        // Display stats
        Jubs.text = $"Current Jubs: {PlayerPrefs.GetFloat("jubs", 0)}@";
        Runs.text = $"Runs: {PlayerPrefs.GetFloat("runs", 0)}";
    }
    public void StartGame()
    {
        // Add a run
        PlayerPrefs.SetFloat("runs", PlayerPrefs.GetFloat("runs", 0) + 1);

        int trolled = PlayerPrefs.GetInt("trolled", 0);

        // Load tutorial or random level
        if (trolled == 0) SceneManager.LoadScene(tutorialScene);
        else SceneManager.LoadScene(Random.Range(Mathf.FloorToInt(levels.x), Mathf.FloorToInt(levels.y) + 1));
    }
}
