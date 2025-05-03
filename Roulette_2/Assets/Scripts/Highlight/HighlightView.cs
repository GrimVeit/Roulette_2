using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightView : View
{
    [SerializeField] private List<HighlightVisualGroup> highlightVisualGroups = new List<HighlightVisualGroup>();

    public void Select(int id)
    {
        var group = GetHighlightVisualGroupById(id);

        if(group == null)
        {
            Debug.LogWarning("Not found highlight group with id - " + id);
            return;
        }

        group.Select();
    }

    public void Deselect(int id)
    {
        var group = GetHighlightVisualGroupById(id);

        if (group == null)
        {
            Debug.LogWarning("Not found highlight group with id - " + id);
            return;
        }

        group.Deselect();
    }

    public void DeselectAll()
    {
        highlightVisualGroups.ForEach(data => data.Deselect());
    }

    private HighlightVisualGroup GetHighlightVisualGroupById(int id)
    {
        return highlightVisualGroups.FirstOrDefault(data => data.Id == id);
    }
}
