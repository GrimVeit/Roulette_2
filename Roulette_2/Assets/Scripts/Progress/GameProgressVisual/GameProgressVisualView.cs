using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    
public class GameProgressVisualView : View
{
    [SerializeField] private List<GameProgressVisual> gameProgressVisuals = new List<GameProgressVisual>();

    public void ChangeGameStatus(int id, bool status)
    {
        var visual = GetGameProgressVisualById(id);

        if(visual == null)
        {
            Debug.LogWarning("Not found game progress visual with id - " + id);
            return;
        }

        if (status)
        {
            visual.Open();
        }
        else
        {
            visual.Close();
        }
    }

    private GameProgressVisual GetGameProgressVisualById(int id)
    {
        return gameProgressVisuals.FirstOrDefault(data => data.Id == id);
    }
}
