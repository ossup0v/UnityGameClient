using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemOnMapManager : MonoBehaviour
{
    public int Id { get; private set; }
    public ItemOnMapKind Kind { get; private set; }

    public void Initialize(int id, ItemOnMapKind kind)
    {
        Id = id;
        Kind = kind;
    }
}
