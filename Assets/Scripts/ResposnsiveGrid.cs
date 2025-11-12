using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ResponsiveGrid : MonoBehaviour
{
    [Header("Fixed Grid Settings")]
    public int columns = 3;
    public int rows = 4;

   
    [Range(0f, 0.1f)] public float spacingPercent = 0.02f;

    [Header("Card Size Scaling")]
    [Range(0.5f, 1f)] public float sizeMultiplier = 0.85f;

    private GridLayoutGroup grid;
    private RectTransform rectTransform;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        AdjustGrid();
    }

    void OnRectTransformDimensionsChange()
    {
        AdjustGrid();
    }

    void AdjustGrid()
    {
        if (!rectTransform || !grid) return;

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float spacing = parentWidth * spacingPercent;
        grid.spacing = new Vector2(spacing, spacing);

        float totalHorizontalSpacing = spacing * (columns + 1);
        float totalVerticalSpacing = spacing * (rows + 1);

        float availableWidth = (parentWidth - totalHorizontalSpacing) / columns;
        float availableHeight = (parentHeight - totalVerticalSpacing) / rows;

        float cellSize = Mathf.Min(availableWidth, availableHeight);

        
        cellSize *= sizeMultiplier;

        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
        grid.childAlignment = TextAnchor.MiddleCenter;
    }
}
