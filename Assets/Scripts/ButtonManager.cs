using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel = null;

    private void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void ReplayButton()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
