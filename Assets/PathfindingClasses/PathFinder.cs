using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
        //ShortestPathFunction(pathFinding.features, pathFinding.features.Where(point => point.properties.roomNumber == "Main").Single(), pathFinding.features.Where(point => point.properties.roomNumber == "B214").Single());

    public List<Feature> ShortestPathFunction(List<Feature> features, Feature start, Feature end)
    {
        var previous = new Dictionary<Feature, Feature>();

        var queue = new Queue<Feature>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            if (vertex == end)
            {
                break;
            }
            foreach (var neighbor in vertex.properties.connectingPoints)
            {
                foreach (var feature in features)
                {
                    if (previous.ContainsKey(feature))
                        continue;
                    if (neighbor == feature.properties.id)
                    {
                        previous[feature] = vertex;

                        queue.Enqueue(feature);
                        break;
                    }
                }
            }
        }

        List<Feature> shortestPath = new List<Feature>();
        Feature loopFeature = end;

        while (loopFeature != start)
        {
            shortestPath.Add(loopFeature);
            loopFeature = previous[loopFeature];
        }
        shortestPath.Add(loopFeature);

        return shortestPath;
    }
}


public class Properties
{
    public int id { get; set; }
    public int isRoom { get; set; }
    public string roomNumber { get; set; }
    public List<int> connectingPoints { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public List<double> coordinates { get; set; }
}

public class Feature
{
    public string type { get; set; }
    public Properties properties { get; set; }
    public Geometry geometry { get; set; }

    public override string ToString()
    {
        return $"{this.properties.id} {this.properties.roomNumber}";
    }
}

public class Root
{
    public string type { get; set; }
    public string name { get; set; }
    public List<Feature> features { get; set; }
}
