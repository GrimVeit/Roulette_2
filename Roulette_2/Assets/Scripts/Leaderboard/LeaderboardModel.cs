using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardModel
{
    private IDatabaseRecordsEvents _databaseRecordsEvents;

    private List<UserData> pagedPlayers = new();
    private const int topCount = 3;
    private const int playersPerPage = 7;

    private int currentPage = 0;
    private int totalPages => Mathf.CeilToInt(pagedPlayers.Count / (float)playersPerPage);

    private bool isActive = true;

    public LeaderboardModel(IDatabaseRecordsEvents databaseRecordsEvents)
    {
        _databaseRecordsEvents = databaseRecordsEvents;
        _databaseRecordsEvents.OnGetUsersRecords += GetUsers;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _databaseRecordsEvents.OnGetUsersRecords -= GetUsers;
    }

    private void GetUsers(List<UserData> users)
    {
        var top = users.Take(topCount).ToList();
        OnGetTopPlayers?.Invoke(top);

        pagedPlayers = users.Skip(topCount).ToList();

        var pageData = pagedPlayers
            .Skip(currentPage * playersPerPage)
            .Take(playersPerPage)
            .ToList();

        int rankOffset = topCount + currentPage * playersPerPage + 1;

        OnGetPagePlayers?.Invoke(pageData, rankOffset);
    }

    public void NextPage()
    {

        if(!isActive || pagedPlayers.Count == 0) return;

        if (currentPage < totalPages - 1)
        {
            currentPage += 1;
            ShowCurrentPage();
            isActive = false;
        }
    }

    public void PreviousPage()
    {
        if (!isActive || pagedPlayers.Count == 0) return;

        if (currentPage > 0)
        {
            currentPage -= 1;
            ShowCurrentPage();
            isActive = false;
        }
    }

    private void ShowCurrentPage()
    {
        var pageData = pagedPlayers
            .Skip(currentPage * playersPerPage)
            .Take(playersPerPage)
            .ToList();

        int rankOffset = topCount + currentPage * playersPerPage + 1;

        OnGetPagePlayers?.Invoke(pageData, rankOffset);
    }

    public void Activate()
    {
        isActive = true;
    }

    #region Output

    public event Action<List<UserData>> OnGetTopPlayers;
    public event Action<List<UserData>, int> OnGetPagePlayers;

    #endregion
}
