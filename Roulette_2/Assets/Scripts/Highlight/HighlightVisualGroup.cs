using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighlightVisualGroup
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private List<HighlightVisual> highlightVisuals = new List<HighlightVisual>();

    public void Select()
    {
        highlightVisuals.ForEach(highlightVisual => highlightVisual.Select());
    }

    public void Deselect()
    {
        highlightVisuals.ForEach(highlightVisual => highlightVisual.Unselect());
    }
}
