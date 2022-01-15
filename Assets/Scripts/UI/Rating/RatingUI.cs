using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RatingUI : MonoBehaviour
{
    [SerializeField] private RectTransform _teamsParent;
    [SerializeField] private RatingTeam _teamPrefab;
    [SerializeField] private RectTransform _ratingPanel;
    [SerializeField] private StageInfo _stageInfo;

    private List<RatingTeam> _teamsList = new List<RatingTeam>();

    private bool _teamsListInited;

    private void Awake()
    {
       Hide();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            Show();
        else
            Hide();
    }

    public void UpdateInfo()
    {
        if (!_teamsListInited)
        {
            InitTeamsList();
            _teamsListInited = true;
        }
            
        foreach(var item in RatingManager.Rating.Values)
        {
            var itemTeam = _teamsList.Find(team => team.Id == item.Team);
            if (itemTeam != null)
                itemTeam.UpdatePlayerInfo(item);
            else
                Debug.Log("Cannot update info: player team is null");
        }
    }

    public void InitTeamsList()
    {
        foreach (var item in RatingManager.Rating.Values)
        {
            RatingTeam itemTeam = _teamsList.Find(team => team.Id == item.Team);
            AddPlayerToTeam(item, itemTeam);
        }

        _teamsList.Sort((firstTeam, secondTeam) => firstTeam.Id.CompareTo(secondTeam.Id));
    }

    public void SetStage(string stage)
    {
        _stageInfo.SetStage(stage);
    }

    private RatingTeam AddTeam(int teamID)
    {
        RatingTeam newTeam = Instantiate(_teamPrefab, _teamsParent);
        newTeam.InitTeam(teamID);
        _teamsList.Add(newTeam);
        return newTeam;
    }

    private void AddPlayerToTeam(RatingEntity player, RatingTeam team)
    {
        if(team == null)
        {
            team = AddTeam(player.Team);
        }
        team.AddPlayer(player);

        if (player.Id == NetworkManager.Instance.ServerClient.MyId)
            team.SetTeamAsMine();
    }

    private void Show()
    {
        _ratingPanel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _ratingPanel.gameObject.SetActive(false);
    }
}
