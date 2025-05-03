using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightVisual : MonoBehaviour
{
    [SerializeField] private GameObject objectVisual;

    public void Unselect()
    {
        objectVisual.SetActive(false);
    }

    public void Select()
    {
        objectVisual.SetActive(true);
    }
}
