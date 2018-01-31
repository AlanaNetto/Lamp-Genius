using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutAspectFitter : MonoBehaviour {

    public GridLayoutGroup gridLayoutGroup;
    public RectTransform rectTransform;
    public Vector2 spriteSize;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();

        SetCellSize();

    }

    void SetCellSize()
    {
        if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            float bordersSize = gridLayoutGroup.spacing.x * (gridLayoutGroup.constraintCount - 1);
            bordersSize += gridLayoutGroup.padding.left + gridLayoutGroup.padding.right;

            float x = (rectTransform.rect.width - bordersSize) / gridLayoutGroup.constraintCount;
            float y = (x * spriteSize.y) / spriteSize.x;

            gridLayoutGroup.cellSize = new Vector2(x, y);
        }
        else if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            float bordersSize = gridLayoutGroup.spacing.y * (gridLayoutGroup.constraintCount - 1);
            bordersSize += gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;

            float y = (rectTransform.rect.height - bordersSize) / gridLayoutGroup.constraintCount;
            float x = (y * spriteSize.x) / spriteSize.y;

            gridLayoutGroup.cellSize = new Vector2(x, y);
        }
    }

    void OnRectTransformDimensionsChange()
    {
        if (gridLayoutGroup != null && rectTransform != null)
        {
            SetCellSize();
        }
    }
}
