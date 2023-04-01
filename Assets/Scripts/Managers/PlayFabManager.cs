using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    public GameObject leaderboardItem;
    public Transform leaderboardItemHolder;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login or signup.");
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error while trying to login or signup.");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate()
                {
                    StatisticName = "OverallScore",
                    Value = score,
                }
            }
        };
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "OverallScore",
            StartPosition = 0,
            MaxResultsCount = 10,
        };
        
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in leaderboardItemHolder)
            Destroy(item.gameObject);

        foreach (var item in result.Leaderboard)
        {
            GameObject gameObject = Instantiate(leaderboardItem, leaderboardItemHolder);
            TMP_Text[] texts = gameObject.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId.ToString();
            texts[2].text = item.StatValue.ToString();
            
            Debug.Log($"{item.Position} {item.PlayFabId} {item.StatValue}");
        }   
    }
}
