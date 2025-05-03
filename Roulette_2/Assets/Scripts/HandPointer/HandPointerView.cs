using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandPointerView : View
{
    [SerializeField] private HandPointerPositions handPointerPositions;
    [SerializeField] private HandPointer handPointer;

    public void Activate()
    {
        handPointer.Activate();
    }

    public void Deactivate()
    {
        handPointer.Deactivate();
    }

    public void SetData(int id)
    {
        var handPosition = handPointerPositions.GetHandPointerPositionById(id);

        if(handPosition == null)
        {
            Debug.LogError("Not found hand position with id - " + id);
            return;
        }

        handPointer.Move(handPosition.TransformParent.position, handPosition.VectorRotate);
    }
}

[System.Serializable]
public class HandPointerPositions
{
    [SerializeField] private List<HandPointerPosition> handPointerPositions = new List<HandPointerPosition>();

    public HandPointerPosition GetHandPointerPositionById(int id)
    {
        return handPointerPositions.FirstOrDefault(data => data.Id == id);
    }
}

[System.Serializable]
public class HandPointerPosition
{
    public int Id => id;
    public Transform TransformParent => transformParent;
    public Vector3 VectorRotate => vectorRotate;

    [SerializeField] private int id;
    [SerializeField] private Transform transformParent;
    [SerializeField] private Vector3 vectorRotate;
}
