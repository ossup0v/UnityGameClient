using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject MapEntityPrefab;
    public Material[] Materials;
    private List<GameObject> _mapEntities = new List<GameObject>();
    private Dictionary<int, GameObject> _destroyMapEntities = new Dictionary<int, GameObject>();

    public static MapManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(MapManager)} singletone error!");
            Destroy(this);
        }
    }

    public void InitializeMap(string rawData)
    {
        rawData = rawData.Replace('.', ',');
        var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator = ",";
        var objects = rawData.Split(';');
        
        foreach (var obj in objects)
        {
            try
            {
                var indexOfPosition = obj.IndexOf("P:");
                var indexOfRotation = obj.IndexOf("R:");
                var indexOfScale = obj.IndexOf("S:");
                var indexOfMatirealId = obj.IndexOf("M:");

                var id = 0;
                if (obj.Contains("Id:"))
                {
                    var indexOfId = obj.IndexOf("Id:");
                    var idText = obj.Substring(indexOfId + 3, indexOfPosition - indexOfId - 3);
                    id = int.Parse(idText);
                }

                var positionText = obj.Substring(indexOfPosition + 2, indexOfRotation - indexOfPosition - 2).Split('!');

                var position = new Vector3(float.Parse(positionText[0], ci), float.Parse(positionText[1], ci), float.Parse(positionText[2], ci));

                var rotationText = obj.Substring(indexOfRotation + 2, indexOfScale - indexOfRotation - 2).Split('!');

                var rotation = new Quaternion(float.Parse(rotationText[0], ci), float.Parse(rotationText[1], ci), float.Parse(rotationText[2], ci), float.Parse(rotationText[3], ci));

                var scaleText = obj.Substring(indexOfScale + 2, indexOfMatirealId - indexOfScale - 2).Split('!');

                var scale = new Vector3(float.Parse(scaleText[0], ci), float.Parse(scaleText[1], ci), float.Parse(scaleText[2], ci));

                var @object = Instantiate(MapEntityPrefab, position, rotation);

                if (id != 0)
                {
                    _destroyMapEntities.Add(id, @object);
                }
                
                _mapEntities.Add(@object);
                
                @object.transform.localScale = scale;

                var materialText = obj.Substring(indexOfMatirealId + 2);

                if (int.TryParse(materialText, out var materialId))
                {
                    @object.GetComponent<MeshRenderer>().material = Materials[materialId];
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }

    public void DestroyById(int id)
    {
        Destroy(_destroyMapEntities[id].gameObject);
        _destroyMapEntities.Remove(id);
    }

    public void DestroyMap()
    {
        foreach (var entity in _mapEntities)
        {
            Destroy(entity);
        }
        _mapEntities.Clear();
        foreach (var entity in _destroyMapEntities.Values)
        {
            Destroy(entity);
        }
        _destroyMapEntities.Clear();
    }
}
