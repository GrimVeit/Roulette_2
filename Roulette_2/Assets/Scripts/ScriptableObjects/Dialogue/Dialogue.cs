using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int IdPerson => idPerson;
    public int IdDesign => idDesign;
    public string Description => description;

    [SerializeField] private int idPerson;
    [SerializeField] private int idDesign;
    [SerializeField] private string description;
}
