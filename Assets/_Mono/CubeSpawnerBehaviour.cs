using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerBehaviour : MonoBehaviour 
{
    public GameObject Prefab;
    public int Count;
    public int MaxX;
    public int MaxY;
    public int MaxZ;

    void Start () 
    {
        for (var i = 0; i < Count; i++)
        {
            Instantiate(Prefab, 
                        new Vector3(Random.Range(-MaxX / 2, MaxX / 2),
                                    Random.Range(-MaxY / 2, MaxY / 2),
                                    Random.Range(-MaxZ / 2, MaxZ / 2)),
                        Quaternion.identity);
        }
    }
}
