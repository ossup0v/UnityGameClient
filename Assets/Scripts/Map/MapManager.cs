using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject MapEntityPrefab;
    public Material material1;
    public Material material2;

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
        List<Transform> transforms = new List<Transform>();

        var objects = rawData.Split(';');
        try
        {
            foreach (var obj in objects)
            {
                try
                {
                    var indexOfPosition = obj.IndexOf("P:");
                    var indexOfRotation = obj.IndexOf("R:");
                    var indexOfScale = obj.IndexOf("S:");
                    var indexOfMatirealId = obj.IndexOf("M:");

                    var positionText = obj.Substring(indexOfPosition + 2, indexOfRotation - indexOfPosition - 2).Split('!');

                    var position = new Vector3(float.Parse(positionText[0]), float.Parse(positionText[1]), float.Parse(positionText[2]));

                    var rotationText = obj.Substring(indexOfRotation + 2, indexOfScale - indexOfRotation - 2).Split('!');

                    var rotation = new Quaternion(float.Parse(rotationText[0]), float.Parse(rotationText[1]), float.Parse(rotationText[2]), float.Parse(rotationText[3]));

                    var scaleText = obj.Substring(indexOfScale + 2, indexOfMatirealId - indexOfScale - 2).Split('!');

                    var scale = new Vector3(float.Parse(scaleText[0]), float.Parse(scaleText[1]), float.Parse(scaleText[2]));

                    var @object = Instantiate(MapEntityPrefab, position, rotation);
                    @object.transform.localScale = scale;

                    var materialText = obj.Substring(indexOfMatirealId + 2);

                    if (int.TryParse(materialText, out var materialId))
                    {

                        switch (materialId)
                        {
                            case 1337:
                                break;
                            case 1:
                                @object.GetComponent<MeshRenderer>().material = material1;
                                break;
                            case 2:
                                @object.GetComponent<MeshRenderer>().material = material2;
                                break;
                            default:
                                Debug.LogError($"Unckow material with id {materialId}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
