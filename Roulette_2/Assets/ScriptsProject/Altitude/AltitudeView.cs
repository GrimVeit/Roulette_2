using TMPro;
using UnityEngine;

public class AltitudeView : View
{
    [SerializeField] private TextMeshProUGUI textAltitude;

    public void SetAltitude(int altitude)
    {
        textAltitude.text = $"{altitude}m";
    }
}
