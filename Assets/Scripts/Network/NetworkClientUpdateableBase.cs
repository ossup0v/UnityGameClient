using UnityEngine;

// [CreateAssetMenu(fileName = "NetworkClientUpdateableBase", menuName = "Network/NetworkClientUpdateableBase", order = 0)]
public abstract class NetworkClientUpdateableBase : ScriptableObject
{
    public abstract void Init();
    public abstract void OnUpdate();
}