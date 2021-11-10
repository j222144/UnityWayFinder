
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class SpawnOnMap : MonoBehaviour
{

    [SerializeField]
    AbstractMap _map;

    List<Vector3> pos = new List<Vector3>();
    PathFinder pathFinder = new PathFinder();


    public string To { get; set; }
    public string From { get; set; }
    // Json
    TextAsset targetFile;
    string myJson;
    Root pathFinding;
    void Start()
    {
        // Assign JSON
        targetFile = Resources.Load<TextAsset>("NavPoints");
        myJson = targetFile.text;
        pathFinding = JsonConvert.DeserializeObject<Root>(myJson);
        // Create LineRenderer
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
    }

    private void Update()
    {
        DrawLine(From, To);
    }


    // Update line every frame incase map moves
    private void DrawLine(string from, string to)
    {
        LineRenderer line = GetComponent<LineRenderer>();
        pos.Clear();
        //line = Instantiate (line) as LineRenderer;
        //if (gameObject.GetComponent<LineRenderer>())
        //{
        //	//l = gameObject.GetComponent<LineRenderer>();
        //	Destroy(GetComponent<LineRenderer>()); // if already drawn, destroy and redraw
        //}

        //l = gameObject.AddComponent<LineRenderer>();
        List<Feature> shortestPath = pathFinder.ShortestPathFunction(pathFinding.features, pathFinding.features.Where(point => point.properties.roomNumber == from).Single(), pathFinding.features.Where(point => point.properties.roomNumber == to).Single());
        foreach (var feature in shortestPath)
        {
            Vector2d conversion = Conversions.StringToLatLon($"{feature.geometry.coordinates[1]}, {feature.geometry.coordinates[0]}");
            Vector3 localPos = _map.GeoToWorldPosition(conversion, true);
            pos.Add(new Vector3(localPos.x, 2f, localPos.z));
        }
        //foreach (var location in _locations)
        //{
        //	Vector3 localPos = _map.GeoToWorldPosition(location, true);
        //	//spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
        //	//transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        //	//
        //	pos.Add(new Vector3(localPos.x, 2f, localPos.z));
        //}
        line.positionCount = pos.Count;
        line.SetPositions(pos.ToArray());
    }
}