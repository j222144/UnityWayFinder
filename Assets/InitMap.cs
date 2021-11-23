using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AbstractMap _map;
    ILocationProvider _locationProvider;

    private void Awake()
    {
        // Prevent double initialization of the map. 
        _map.InitializeOnStart = true;
    }
}
