using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int Id;
    public GameObject ExplosionPrefab;

    public void Initialize(int id)
    {
        Id = id;
    }

    public void Explode(Vector3 position)
    {
        transform.position = position;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        GameManager.Proectiles.Remove(Id);
        Destroy(gameObject);
    }
}
