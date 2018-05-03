using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RandomSpawnCube : ISharedComponentData
{
    public GameObject Prefab;
    public int Count;
    public int MaxX;
    public int MaxY;
    public int MaxZ;
    public int MaxRotationSpeed;
}

public class RandomSpawnCubeComponent : SharedComponentDataWrapper<RandomSpawnCube> {}
