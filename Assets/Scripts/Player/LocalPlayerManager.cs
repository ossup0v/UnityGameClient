using System.Text;
using UnityEngine.UI;

internal class LocalPlayerManager : PlayerManager
{
    public Text HPText;
    public Text RatingText;
    public Text GrenadeText;

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

    private void OnGrenadeCountChanged(int count)
    {
        GrenadeText.text = $" G: {count}";
    }

    private void Subscribe()
    {
        HealthChanged += OnHealthChanged;
        GrenadeCountChanged += OnGrenadeCountChanged;
        RatingManager.RatingChaneged += OnRatingChanged;
    }

    private void Unsubcribe()
    {
        HealthChanged -= OnHealthChanged;
        GrenadeCountChanged -= OnGrenadeCountChanged;
        RatingManager.RatingChaneged -= OnRatingChanged;
    }

    private void OnDestroy()
    {
        Unsubcribe();
    }
}
