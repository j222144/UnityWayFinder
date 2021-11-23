
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnOnMap : MonoBehaviour
{

    [SerializeField]
    AbstractMap _map;

    List<Vector3> pos = new List<Vector3>();
    PathFinder pathFinder = new PathFinder();

    public GameObject startCircle;
    public GameObject endCircle;

    public string To { get; set; }
    public string From { get; set; }
    // Json
    TextAsset targetFile;
    string myJson;
    public Root pathFinding { get; set; }
    void Start()
    {
        startCircle.SetActive(false);
        endCircle.SetActive(false);
        // Assign JSON
        targetFile = Resources.Load<TextAsset>("NavPoints");
        myJson = targetFile.text;
        pathFinding = JsonConvert.DeserializeObject<Root>(myJson);
        // Create LineRenderer
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.7f;
        line.endWidth = 0.7f;
    }

    private void Update()
    {
        DrawLine(From, To);
    }


    // Update line every frame incase map moves
    private void DrawLine(string from, string to)
    {
        LineRenderer line = GetComponent<LineRenderer>();
        if (From == null || To == null)
        {
            startCircle.SetActive(false);
            endCircle.SetActive(false);
            pos.Clear();
            line.positionCount = pos.Count;
        }
        else
        {
            pos.Clear();
            List<Feature> shortestPath = pathFinder.ShortestPathFunction(pathFinding.features, pathFinding.features.Where(point => point.properties.roomNumber == from).Single(), pathFinding.features.Where(point => point.properties.roomNumber == to).Single());
            foreach (var feature in shortestPath)
            {
                Vector2d conversion = Conversions.StringToLatLon($"{feature.geometry.coordinates[1]}, {feature.geometry.coordinates[0]}");
                Vector3 localPos = _map.GeoToWorldPosition(conversion, true);
                pos.Add(new Vector3(localPos.x, 2f, localPos.z));
            }
            line.positionCount = pos.Count;
            line.SetPositions(pos.ToArray());
            DrawCircles(shortestPath[0], shortestPath[shortestPath.Count - 1]);
        }
    }
    private void DrawCircles(Feature end, Feature start)
    {
        startCircle.SetActive(true);
        endCircle.SetActive(true);
        Vector2d conversionStart = Conversions.StringToLatLon($"{start.geometry.coordinates[1]}, {start.geometry.coordinates[0]}");
        Vector2d conversionEnd = Conversions.StringToLatLon($"{end.geometry.coordinates[1]}, {end.geometry.coordinates[0]}");
        Vector3 localPosStart = _map.GeoToWorldPosition(conversionStart, true);
        Vector3 localPosEnd = _map.GeoToWorldPosition(conversionEnd, true);
        startCircle.transform.position = new Vector3(localPosStart.x, 3f, localPosStart.z);
        endCircle.transform.position = new Vector3(localPosEnd.x, 3f, localPosEnd.z);
    }
}