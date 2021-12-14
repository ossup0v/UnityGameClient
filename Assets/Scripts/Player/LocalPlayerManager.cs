using System.Text;
using UnityEngine.UI;

internal class LocalPlayerManager : PlayerManager
{
    public Text HPText;
    public Text RatingText;
    public Text ItemCountText;

    private void Awake()
    {
        Subscribe();
    }

    private void UpdateHPText(float hp)
    {
        HPText.text = $" HP: {hp}";
    }

    private void UpdateRatingTable()
    {
        var text = new StringBuilder();
        foreach (var item in RatingManager.Rating.Values)
        {
            if (item.Id == NetworkClient.Instance.MyId)
                text.Append($"<color=red>{item.Id}:{item.Username}:K:{item.Killed}:D:{item.Died}</color>");
            else
                text.Append($"{item.Id}:{item.Username}:K:{item.Killed}:D:{item.Died}");

            text.AppendLine();
        }
        RatingText.text = text.ToString();
    }

    private void UpdateItemCountText(int count)
    {
        ItemCountText.text = $"G: {count}";
    }

    private void Subscribe()
    {
        OnHealthChange += UpdateHPText;
        OnItemCountChange += UpdateItemCountText;
        RatingManager.OnRatingChaneged += UpdateRatingTable;
    }

    private void Unsubcribe()
    {
        OnHealthChange -= UpdateHPText;
        OnItemCountChange -= UpdateItemCountText;
        RatingManager.OnRatingChaneged -= UpdateRatingTable;
    }

    private void OnDestroy()
    {
        Unsubcribe();
    }
}
