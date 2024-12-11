using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public int rows;
    public int cols;
    public Vector2 cellSize;
    public Vector2 cellSpace;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)cols) - ((cellSpace.x / (float)cols) * (cols - 1)) - (padding.left / (float)cols) - (padding.right / (float)cols);
        float cellHeight = (parentHeight / (float)rows) - ((cellSpace.y / (float)rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int colCount = 0;
        int rowCount = 0;


        for(int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / cols;
            colCount = i % cols;

            var item = rectChildren[i];
            var xPos = (cellSize.x * colCount) + (cellSpace.x * colCount);
            var yPos = (cellSize.y * rowCount) + (cellSpace.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
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
