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

    private void OnHealthChanged(float hp)
    {
        HPText.text = $" HP: {hp}";
    }

    private void OnRatingChanged()
    {
        var text = new StringBuilder();
        int index = 1;
        foreach (var item in RatingManager.Rating.Values)
        {
            if (item.Id == NetworkClient.Instance.MyId)
                text.Append($"<color=red>{index}:{item.Username}:K:{item.Killed}:M:{item.KilledBots}:D:{item.Died}</color>");
            else
                text.Append($"{index}:{item.Username}:K:{item.Killed}:M:{item.KilledBots}:D:{item.Died}");

            index++;

            text.AppendLine();
        }
        RatingText.text = text.ToString();
    }

    private void OnGrenadeCountChanged(int count)
    {
        ItemCountText.text = $" G: {count}/3";
    }

    private void Subscribe()
    {
        healthManager.HealthChanged += OnHealthChanged;
        GrenadeCountChanged += OnGrenadeCountChanged;
        RatingManager.RatingChanged += OnRatingChanged;
    }

    private void Unsubcribe()
    {
        healthManager.HealthChanged -= OnHealthChanged;
        GrenadeCountChanged -= OnGrenadeCountChanged;
        RatingManager.RatingChanged -= OnRatingChanged;
    }

    private void OnDestroy()
    {
        Unsubcribe();
    }
}
