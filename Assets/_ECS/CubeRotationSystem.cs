using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

#if false

public class CubeRotationSystem : ComponentSystem
{
    struct Group
    {
        [ReadOnly]
        public ComponentDataArray<RotationSpeed> RotationSpeeds;
        public ComponentDataArray<Rotation>      Rotations;
        public EntityArray                       Entities;

        public int Length;
    }

    [Inject] private Group _group;

    protected override void OnUpdate()
    {
        if (_group.Length <= 0)
            return;

        for (var i = 0; i < _group.Length; i++)
        {
            var rotationSpeed = _group.RotationSpeeds[i];
            var rotation = _group.Rotations[i];
            var entity = _group.Entities[i];

            rotation.Value = math.mul(
                math.normalize(rotation.Value), 
                math.axisAngle(math.up(), rotationSpeed.Value * Time.deltaTime)
            );

            EntityManager.SetComponentData(entity, rotation);
        }
    }
}

#else

public class CubeRotationSystem : JobComponentSystem
{
    [ComputeJobOptimization]
    struct CubeRotationJob : IJobProcessComponentData<Rotation, RotationSpeed>
    {
        public float deltaTime;

        public void Execute(ref Rotation rotation, [ReadOnly]ref RotationSpeed rotationSpeed)
        {
            rotation.Value = math.mul(
                math.normalize(rotation.Value), 
                math.axisAngle(math.up(), rotationSpeed.Value * deltaTime)
            );
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new CubeRotationJob { deltaTime = Time.deltaTime };
        return job.Schedule(this, 64, inputDeps);
    }
}

#endif