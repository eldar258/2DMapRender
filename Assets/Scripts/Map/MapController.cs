using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private Map _map;
    Vector2 leftUpAngleOfMap;
    Vector2 rightDownAngleOfMap;

    [SerializeField] string jsonFilePath = "ResourcesTestingTaskJunior/testing_views_settings_normal_level";
    void Awake()
    {
        ReadJsonInMap(jsonFilePath);
        CameraController.ControlCameraTransform += ClampCameraPosition;
    }
   
    void Start()
    {
        GenerateMap();
    }

    private void ReadJsonInMap(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        _map = JsonUtility.FromJson<Map>(textAsset.text);
    }
    private void GenerateMap()
    {
        for (int i = 0; i < _map.Length; i++)
        {
            Sprite sprite = CreateSpriteWithTextureResources(_map[i]);
            var go = CreateSpriteObject(_map[i], sprite);
        }
        CalculateLimit(_map[0], _map[_map.Length - 1]);
    }

    private void CalculateLimit(MapTile firstMapTile, MapTile lastMapTile)
    {
        leftUpAngleOfMap = new Vector3(0, firstMapTile.Height);
        rightDownAngleOfMap = new Vector3(lastMapTile.X + lastMapTile.Width, lastMapTile.Y);
    }

    private GameObject CreateSpriteObject(MapTile mapTile, Sprite sprite)
    {
        GameObject result = new GameObject(mapTile.Id);
        result.AddComponent<SpriteRenderer>().sprite = sprite;
        result.transform.parent = transform;
        result.transform.localPosition = new Vector3(mapTile.X, mapTile.Y, 0);
        result.AddComponent<BoxCollider2D>();
        return result;
    }

    private static Sprite CreateSpriteWithTextureResources(MapTile mapTile)
    {
        Texture2D texture = Resources.Load<Texture2D>($"ResourcesTestingTaskJunior/{mapTile.Id}");
        Rect rect = new Rect(0, 0, mapTile.Width * 100, mapTile.Height * 100);
        return Sprite.Create(texture, rect, Vector2.zero);
    }

    private void ClampCameraPosition(Camera camera)
    {
        Vector3 leftUpAngleOfCamera = camera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector3 rightDownAngleOfCamera = camera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        camera.transform.position += ShiftRightDown(leftUpAngleOfCamera, leftUpAngleOfMap) 
            + ShiftLeftUp(rightDownAngleOfCamera, rightDownAngleOfMap);
    }

    private static Vector3 ShiftRightDown(Vector2 a, Vector2 m)
    {
        float x = a.x < m.x ? m.x - a.x : 0;
        float y = a.y > m.y ? m.y - a.y : 0;
        return new Vector3(x, y, 0);
    }
    private static Vector3 ShiftLeftUp(Vector2 a, Vector2 m)
    {
        float x = a.x > m.x ? m.x - a.x : 0;
        float y = a.y < m.y ? m.y - a.y : 0;
        return new Vector3(x, y, 0);
    }

    void OnDestroy()
    {
        CameraController.ControlCameraTransform -= ClampCameraPosition;
    }
}
