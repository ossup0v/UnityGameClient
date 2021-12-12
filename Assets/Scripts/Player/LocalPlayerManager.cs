using System.Text;
using UnityEngine.UI;

internal class LocalPlayerManager : PlayerManager
{
    public Text HPText;
    public Text RatingText;

    private void Awake()
    {
        OnHealthChange += UpdateHPText;

        RatingManager.OnRatingChaneged += UpdateRatingTable;
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
                text.Append($"<color=red>{item.Id}:Username:{item.Username}:K:{item.Killed}:D:{item.Died}</color>");
            else
                text.Append($"{item.Id}:Username:{item.Username}:K:{item.Killed}:D:{item.Died}");

            text.AppendLine();
        }
        RatingText.text = text.ToString();
    }
}
