using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackgroundView : View, IIdentify
{
    public string GetID() => id;
    [SerializeField] private string id;

    [SerializeField] private RawImage rawImage;
    [SerializeField] private RawImage rawImage_2;
    [SerializeField] private RawImage rawImage_3;
    [SerializeField] private RawImage rawImage_4;

    private Rect rect;
    private Rect rect_2;
    private Rect rect_3;
    private Rect rect_4;

    public void Initialize()
    {
        rect = rawImage.uvRect;
        rect_2 = rawImage_2.uvRect;
        rect_3 = rawImage_3.uvRect;
        rect_4 = rawImage_4.uvRect;
    }

    public void Dispose()
    {

    }

    public void SetScrollValue(float scrollValue)
    {
        rect.y = scrollValue;
        rect_2.y = scrollValue;
        rect_3.y = scrollValue;
        rect_4.y = scrollValue;

        rawImage.uvRect = rect;
        rawImage_2.uvRect = rect_2;
        rawImage_3.uvRect = rect_3;
        rawImage_4.uvRect = rect_4;
    }
}
