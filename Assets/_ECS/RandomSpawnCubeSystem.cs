using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class RandomSpawnCubeSystem : ComponentSystem
{
    struct Group
    {
        [ReadOnly]
        public SharedComponentDataArray<RandomSpawnCube> SpawnCube;
        public EntityArray Entity;
        public int Length;
    }

    [Inject] private Group _group;

    protected override void OnUpdate()
    {
        while (_group.Length > 0)
        {
            var spawnCube = _group.SpawnCube[0];
            var entity = _group.Entity[0];

            var newEntities = new NativeArray<Entity>(spawnCube.Count, Allocator.Temp);
            EntityManager.Instantiate(spawnCube.Prefab, newEntities);

            for (var i = 0; i < spawnCube.Count; i++)
            {
                var pos = new Position
                {
                    Value = new float3(Random.Range(-spawnCube.MaxX / 2, spawnCube.MaxX / 2),
                                       Random.Range(-spawnCube.MaxY / 2, spawnCube.MaxY / 2),
                                       Random.Range(-spawnCube.MaxZ / 2, spawnCube.MaxZ / 2))
                };
                EntityManager.SetComponentData(newEntities[i], pos);

                var rotationSpeed = new RotationSpeed
                {
                    Value = Random.Range(0, spawnCube.MaxRotationSpeed)
                };
                EntityManager.SetComponentData(newEntities[i], rotationSpeed);
            }

            newEntities.Dispose();

            EntityManager.RemoveComponent<RandomSpawnCube>(entity);
            UpdateInjectedComponentGroups();
        }
    }
}