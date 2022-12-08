using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Mostly stolen from this YouTube Video: https://www.youtube.com/watch?v=CGsEJToeXmA
 * All Credit to the GameDevGuide Channel 
 * 
 */


public class FlexibleLayoutGrid : LayoutGroup
{
    public enum FitType { Uniform, Width, Height, FixedRow, FixedCol }
    public FitType fitType;
    public int rows, columns;
    public Vector2 cellSize;
    public RectOffset spacing; 

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrt = Mathf.Sqrt(transform.childCount);

        rows = Mathf.CeilToInt(sqrt);
        columns = Mathf.CeilToInt(sqrt);

        switch (fitType)
        {
            case FitType.Width:
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
                break;
            case FitType.Height:
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
                break;
            case FitType.Uniform:
                break;
            case FitType.FixedCol:
                break;
            case FitType.FixedRow:
                break;
        }
 
        cellSize = new Vector2(rectTransform.rect.width / Mathf.CeilToInt(sqrt) - spacing.top / columns * 2f - spacing.bottom / columns * 2f - padding.left / columns - padding.right / columns, 
            rectTransform.rect.height / Mathf.CeilToInt(sqrt) - (spacing.top + spacing.bottom) / rows * 2f - (padding.top + padding.bottom) / rows);

        for(int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform item = rectChildren[i];

            SetChildAlongAxis(item, 0, cellSize.x * (i % columns) + (spacing.top + spacing.bottom) * (i % columns) + padding.left, cellSize.x) ;
            SetChildAlongAxis(item, 1, cellSize.y * (i / columns) + (spacing.left + spacing.right) * (i / columns) + padding.top, cellSize.y);

        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
