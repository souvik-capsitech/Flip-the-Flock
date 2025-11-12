using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelectUI : MonoBehaviour
{
    public SceneFader fader;

    public void ChooseForest()
    {
        ThemeManager.instance.SelectTheme("Forest");
        fader.FadeToScene("GamePlay");
    }

    public void ChooseArctic()
    {
        ThemeManager.instance.SelectTheme("Arctic");
        fader.FadeToScene("GamePlay");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
