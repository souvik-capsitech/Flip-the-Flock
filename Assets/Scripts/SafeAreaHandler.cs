using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaHandler : MonoBehaviour
{
    private RectTransform panelSafeArea;
    private Rect currentSafeArea = new Rect(0, 0, 0, 0);
    private ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;

    void Awake()
    {
        panelSafeArea = GetComponent<RectTransform>();
        Refresh();
    }

    void OnEnable()
    {
        Refresh();
    }

    void Update()
    {
        if (currentOrientation != Screen.orientation || currentSafeArea != Screen.safeArea)
            Refresh();
    }

    void Refresh()
    {
        ApplySafeArea(Screen.safeArea);
    }

    void ApplySafeArea(Rect safeArea)
    {
        if (panelSafeArea == null)
            panelSafeArea = GetComponent<RectTransform>();

        currentSafeArea = safeArea;
        currentOrientation = Screen.orientation;

        
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

  
        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
        panelSafeArea.offsetMin = Vector2.zero;
        panelSafeArea.offsetMax = Vector2.zero;

        
         Debug.Log($"Safe area applied: {safeArea}, orientation: {currentOrientation}");
    }
}
