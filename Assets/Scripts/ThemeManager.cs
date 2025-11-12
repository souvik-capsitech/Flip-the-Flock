using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;

    public enum ThemeType { Forest, Arctic }
    public ThemeType currentTheme;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectTheme(string themeName)
    {
        if (themeName == "Forest")
            currentTheme = ThemeType.Forest;
        else if (themeName == "Arctic")
            currentTheme = ThemeType.Arctic;
    }
}
