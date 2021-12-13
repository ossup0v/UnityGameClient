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
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);

        GameManager.Proectiles.Remove(Id);
        Destroy(gameObject);
    }
}
