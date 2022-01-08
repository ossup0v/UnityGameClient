using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RatingUI : MonoBehaviour
{
    [SerializeField] private RectTransform _teamsParent;
    [SerializeField] private RatingTeam _teamPrefab;
    [SerializeField] private Image _ratingPanel;
    [SerializeField] private Text _ratingText;

    private List<RatingTeam> _teamsList = new List<RatingTeam>();

    private int _playersCount;

    private void Awake()
    {
       Hide();
       
    }

    private void Start()
    {
        InitTeamsList();
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
        _playersCount = RatingManager.Rating.Values.Count;
        foreach(var item in RatingManager.Rating.Values)
        {
            var itemTeam = _teamsList.Find(team => team.ID == item.Team);
            if(itemTeam != null)
                itemTeam.UpdatePlayerInfo(item);
        }
    }

    public void InitTeamsList()
    {
        foreach (var item in RatingManager.Rating.Values)
        {
            RatingTeam itemTeam = _teamsList.Find(team => team.ID == item.Team);
            if (itemTeam != null)
            {
                itemTeam.AddPlayer(item);
            }
            else
            {
                itemTeam = AddTeam(item.Team);
                itemTeam.AddPlayer(item);
            }
        }
    }

    private RatingTeam AddTeam(int teamID)
    {
        RatingTeam newTeam = Instantiate(_teamPrefab, _teamsParent);
        newTeam.InitTeam(teamID);
        _teamsList.Add(newTeam);
        return newTeam;
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
