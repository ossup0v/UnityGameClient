using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int SpawnerId;
    public bool HasItem;
    public MeshRenderer Model;

    public float ItemRotationSpeed = 50f;
    public float ItemBobSpeed = 25f;

    private Vector3 basePosition;
    private float RandRotatinDilimeter = 100f;

    private void Update()
    {
        if (HasItem)
        {
            transform.Rotate(Vector3.up, ItemRotationSpeed * Time.deltaTime, Space.World);
            transform.position = basePosition + new Vector3(0f, 0.25f * Mathf.Sin(Time.time / RandRotatinDilimeter * ItemBobSpeed), 0f);
        }
    }

    public void Initialize(int id, bool hasItem)
    {
        SpawnerId = id;
        HasItem = hasItem;

        RandRotatinDilimeter = Random.Range(50f, 100f);
        Model.enabled = true;
        basePosition = transform.position;
    }

    public void ItemSpawned()
    {
        HasItem = true;
        Model.enabled = true;
    }

    public void ItemPickup()
    {
        HasItem = false;
        Model.enabled = false;
    }
}
