using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BasketModel : IBasketModel
{
    public event Action<int> OnMoveIndex;

    private bool isActive = false;

    private ISoundProvider soundProvider;

    private int[] indexTransforms;
    private int currentIndexTransform = 0;

    public BasketModel(int size, int startIndex, ISoundProvider soundProvider)
    {
        indexTransforms = new int[size];

        this.soundProvider = soundProvider;
        currentIndexTransform = startIndex;
    }

    public void Initialize()
    {
        OnMoveIndex?.Invoke(currentIndexTransform);
    }

    public void Dispose()
    {

    }

    public void MoveRightIndex()
    {
        if (!isActive) return;

        if (currentIndexTransform < indexTransforms.Length - 1)
        {

            currentIndexTransform += 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }

        soundProvider.PlayOneShot("Button_LeftRight");
    }

    public void MoveLeftIndex()
    {
        if(!isActive) return;

        if (currentIndexTransform > 0)
        {
            currentIndexTransform -= 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }

        soundProvider.PlayOneShot("Button_LeftRight");
    }

    public void SetPositionIndex(int index)
    {
        if (!isActive) return;

        if(index > indexTransforms.Length - 1)
        {
            Debug.LogError("Incorrect index");
            return;
        }

        currentIndexTransform = index;
        OnMoveIndex?.Invoke(currentIndexTransform);

        soundProvider.PlayOneShot("Button_LeftRight");
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
