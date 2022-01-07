using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniterTest : MonoBehaviour
{
    [SerializeField] private Refactor.NetworkClient _networkClient;
    // Start is called before the first frame update
    void Start()
    {
        _networkClient.Init();
    }
}
