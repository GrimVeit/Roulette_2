using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CourseDisplacementModel
{
    public event Action<int> OnChangeCourse;

    public List<CourseData> courseDatas = new List<CourseData>()
    {
        new CourseData(0, -48, -4, 4),
        new CourseData(1, -36, -3, 3),
        new CourseData(2, -24, -2, 2),
        new CourseData(3, -12, -1, 1),
        new CourseData(4, 0, 0, 0),
        new CourseData(5, 12, -1, 1),
        new CourseData(6, 24, -2, 2),
        new CourseData(7, 36, -3, 3),
        new CourseData(8, 48, -4, 4)
    };

    private readonly float changeSpeed = 4f;
    private int currentRoute;
    private CourseData currentCourseData;
    private int currentCourse = 0;

    private IEnumerator currentCoroutine;

    public void Left(int courseRoute)
    {
        currentRoute = courseRoute;

        currentCourseData = courseDatas.FirstOrDefault(cd => cd.Number == currentRoute);

        if(currentCoroutine != null) Coroutines.Stop(currentCoroutine);
        currentCoroutine = ChangeCourse(currentCourseData.GetRandomCourse());
        Coroutines.Start(currentCoroutine);
    }

    public void Right(int courseRoute)
    {
        currentRoute = courseRoute;

        currentCourseData = courseDatas.FirstOrDefault(cd => cd.Number == currentRoute);

        if (currentCoroutine != null) Coroutines.Stop(currentCoroutine);
        currentCoroutine = ChangeCourse(currentCourseData.GetRandomCourse());
        Coroutines.Start(currentCoroutine);
    }

    public void Clear()
    {
        if (currentCoroutine != null) Coroutines.Stop(currentCoroutine);

        currentCourse = 0;
        currentRoute = 4;

        OnChangeCourse?.Invoke(currentCourse);
    }

    private IEnumerator ChangeCourse(int newCourse)
    {
        int startCourseValue = currentCourse;
        float progress = 0f;

        while(startCourseValue != newCourse)
        {
            progress += Time.deltaTime * changeSpeed;

            currentCourse = (int)Mathf.Lerp(startCourseValue, newCourse, Mathf.Clamp01(progress));
            //Debug.Log(currentCourse);
            OnChangeCourse.Invoke(currentCourse);
            yield return null;
        }

        currentCourse = newCourse;
        OnChangeCourse?.Invoke(currentCourse);
        Debug.Log(currentCourse);
    }
}

public class CourseData
{
    public int Number { get; private set; }

    private readonly int _baseValue;
    private readonly int _minDeviation;
    private readonly int _maxDeviation;

    public CourseData(int number, int baseValue, int minDeviation, int maxDeviation)
    {
        Number = number;
        _baseValue = baseValue;
        _minDeviation = minDeviation;
        _maxDeviation = maxDeviation;
    }

    public int GetRandomCourse()
    {
        int deviation = Random.Range(_minDeviation, _maxDeviation);

        return _baseValue + deviation;
    }
}
