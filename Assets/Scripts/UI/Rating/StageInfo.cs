using UnityEngine;
using TMPro;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageName;

    public void SetStage(string stage)
    {
        _stageName.text = stage;
    }
   
}
