using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heightSet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject map;
    public GameObject rooms;
    void Start()
    {
        map = GameObject.Find("Map");


        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < map.transform.childCount; i++)
        {
            list.Add(map.transform.GetChild(i).gameObject);
        }
        

        System.Console.WriteLine(map);
    }

    // Update is called once per frame
    void Update()
    {
        rooms = GameObject.Find("room1");
        System.Console.WriteLine(rooms);
    }
}
