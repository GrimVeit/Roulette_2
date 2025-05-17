using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private List<TopRecord> topRecords = new List<TopRecord>();
    [SerializeField] private SpriteAvatars spriteAvatars;

    [SerializeField] private Transform transformParentDeactivate;
    [SerializeField] private Transform transformParent;
    [SerializeField] private UserGrid userGridPrefab;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    [SerializeField] private List<UserGrid> userGrids = new List<UserGrid>();

    private int currentRank = 0;
    private IEnumerator coroutineTimer;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(() => OnClickToLeft?.Invoke());
        buttonRight.onClick.AddListener(() => OnClickToRight?.Invoke());
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(() => OnClickToLeft?.Invoke());
        buttonRight.onClick.RemoveListener(() => OnClickToRight?.Invoke());
    }

    public void GetTopPlayers(List<UserData> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            topRecords[i].SetData(users[i].Nickname, users[i].Record, spriteAvatars.GetSpriteById(users[i].Avatar));
        }
    }

    public void GetPagedPlayers(List<UserData> users, int rankOffset)
    {
        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(users, rankOffset);
        Coroutines.Start(coroutineTimer);
    }

    private IEnumerator Timer(List<UserData> users, int rankOffset)
    {
        if(userGrids.Count > 0)
        {
            foreach (var grid in userGrids)
            {
                grid.transform.SetParent(transformParentDeactivate);
            }

            foreach (var grid in userGrids)
            {
                if(currentRank > rankOffset)
                {
                    grid.DeactivateLeft();
                }
                else
                {
                    grid.DeactivateRight();
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        userGrids.Clear();

        int currentRankOffset = rankOffset;

        foreach (var player in users)
        {
            var grid = Instantiate(userGridPrefab, transformParent);

            grid.SetData(currentRankOffset, player.Nickname, player.Record, spriteAvatars.GetSpriteById(player.Avatar));

            if(currentRank > rankOffset)
            {
                grid.ActivateLeft();
            }
            else
            {
                grid.ActivateRight();
            }

            userGrids.Add(grid);

            currentRankOffset += 1;

            yield return new WaitForSeconds(0.05f);
        }

        OnEndShow?.Invoke();

        currentRank = rankOffset;
    }

    #region Output

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    public event Action OnEndShow;

    #endregion
}
