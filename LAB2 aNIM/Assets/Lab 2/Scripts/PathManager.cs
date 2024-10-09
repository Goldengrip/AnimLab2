using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    public List<WayPoint> path;

    public GameObject prefab;
    int currentPointIndex = 0;

    public List<GameObject> prefabPoints;

    private void Start()
    {
        prefabPoints = new List<GameObject>();
        //create prefab colliders for the path locations 
        foreach(WayPoint p in path)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = p.pos;
            prefabPoints.Add(go);
        }
    }
    public void Update()
    {
       //update all the prefabs to the waypoint location
       for(int i = 0; i< path.Count; i++)
        {
            WayPoint p = path[i];
            GameObject g = prefabPoints[i];
            g.transform.position = p.pos;
        }
    }

    public List<WayPoint> GetPath()
    {
        if(path == null) 
            path = new List<WayPoint>();
        return path;
    }

    public void CreateAddPoint()
    {
        WayPoint go = new WayPoint();
        path.Add(go);
    }
    
    public WayPoint GetNextTarget()
    {
        int nextPointIndex = (currentPointIndex + 1) % path.Count;
        currentPointIndex = nextPointIndex;
        return path[nextPointIndex];
    }

}
