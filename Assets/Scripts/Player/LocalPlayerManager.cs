using TMPro;

internal class LocalPlayerManager : PlayerManager
{
    public TextMeshPro HPText;

    private void Awake()
    {
        OnHealthChange += UpdateHPText;
    }

    private void UpdateHPText(float hp)
    {
        HPText.text = $"HP: {hp}";
    }
}
