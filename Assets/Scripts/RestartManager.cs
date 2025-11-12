using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public SceneFader fader;  

    public void HomeButton()
    {
        Time.timeScale = 1f;   
        fader.FadeToScene("MainMenu");
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        fader.FadeToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
