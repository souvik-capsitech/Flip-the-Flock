using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader fader;

    public void PlayGame()
    {
        Debug.Log("PLAY BUTTON CLICKED!");
        fader.FadeToScene("ThemeSelect");
    }
    public GameObject settingsPanel;
    public GameObject gameInfoPanel;
    public GameObject factsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenGameInfo()
    {
        gameInfoPanel.SetActive(true);
    }

    public void CloseGameInfo()
    {
        gameInfoPanel.SetActive(false);
    }
    public void OpenFunFacts()
    {
        factsPanel.SetActive(true);
    }

    public void CloseFunFacts()
    {
        factsPanel.SetActive(false);
    }

    public void QuitGame()

    {
        Debug.Log("Quit Game");


        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
