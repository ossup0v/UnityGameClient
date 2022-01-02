using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientUpdater : MonoBehaviour
{
    [SerializeField] private NetworkClientUpdateableBase[] _networkClientUpdateables;

    private void Awake()
    {
        Disable();
        foreach (var networkClientUpdateable in _networkClientUpdateables)
        {
            networkClientUpdateable.Init();
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        foreach (var networkClientUpdateable in _networkClientUpdateables)
        {
            networkClientUpdateable.OnUpdate();
        }
    }
}