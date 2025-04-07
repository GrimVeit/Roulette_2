using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourseDisplacementView : View
{
    [SerializeField] private TextMeshProUGUI textCourse;

    public void SetCourse(int course)
    {
        textCourse.text = $"{course}m";
    }
}
