using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TutorialDescriptionModel
{
    public event Action<TutorialDescription> OnActivateTutorial;
    public event Action<TutorialDescription> OnDeactivateTutorial;
    public event Action<TutorialDescription> OnLockTutorial;


    private TutorialDescriptionGroup tutorialDescriptionGroup;
    private TutorialDescription currentTutorialDescription;

    private List<TutorialDescriptionData> tutorialDescriptionDatas = new List<TutorialDescriptionData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Tutorial.json");

    public TutorialDescriptionModel(TutorialDescriptionGroup group)
    {
        this.tutorialDescriptionGroup = group;

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            TutorialDescriptionDatas tutorialDescriptionDatas = JsonUtility.FromJson<TutorialDescriptionDatas>(loadedJson);

            Debug.Log("Load data");

            this.tutorialDescriptionDatas = tutorialDescriptionDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            tutorialDescriptionDatas = new List<TutorialDescriptionData>();

            for (int i = 0; i < tutorialDescriptionGroup.TutorialDescriptions.Count; i++)
            {
                tutorialDescriptionDatas.Add(new TutorialDescriptionData(true));
            }
        }

        for (int i = 0; i < tutorialDescriptionGroup.TutorialDescriptions.Count; i++)
        {
            tutorialDescriptionGroup.TutorialDescriptions[i].SetData(tutorialDescriptionDatas[i]);
        }
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new TutorialDescriptionDatas(tutorialDescriptionDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }



    public void ActivateTutorial(string id)
    {
        var tutorial = tutorialDescriptionGroup.GetTutorialById(id);

        if (tutorial == null)
        {
            Debug.LogError($"Not found tutorial by id - {id}");
            return;
        }

        if(!tutorial.Data.IsActive) return;

        OnActivateTutorial?.Invoke(tutorial);
    }

    public void DeactivateTutorial(string id)
    {
        var tutorial = tutorialDescriptionGroup.GetTutorialById(id);

        if (tutorial == null)
        {
            Debug.LogError($"Not found tutorial by id - {id}");
            return;
        }

        if (!tutorial.Data.IsActive) return;

        OnDeactivateTutorial?.Invoke(tutorial);
    }

    public void LockTutorial(string id)
    {
        var tutorial = tutorialDescriptionGroup.GetTutorialById(id);

        if (tutorial == null)
        {
            Debug.LogError($"Not found tutorial by id - {id}");
            return;
        }

        if (!tutorial.Data.IsActive) return;

        OnLockTutorial?.Invoke(tutorial);

        tutorial.Data.IsActive = false;
    }
}

[Serializable]
public class TutorialDescriptionDatas
{
    public TutorialDescriptionData[] Datas;

    public TutorialDescriptionDatas(TutorialDescriptionData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class TutorialDescriptionData
{
    public bool IsActive;

    public TutorialDescriptionData(bool isActive)
    {
        this.IsActive = isActive;
    }
}
