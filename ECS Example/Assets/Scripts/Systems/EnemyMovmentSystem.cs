using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics.Systems;
using Unity.Physics;
using UnityEngine;
public partial class EnemyMovmentSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;


        Entities.WithoutBurst().WithStructuralChanges().ForEach((ref Translation translation, ref Rotation rotation, ref EnemyData enemyData, ref PhysicsVelocity physics) =>
        {
            float3 enemyPos = EntityManager.GetComponentData<Translation>(enemyData.Target).Value;
            
            if (math.distance(translation.Value,enemyPos) > 5)
            {
                float3 heading = enemyPos - translation.Value;
                rotation.Value = quaternion.LookRotation(heading, math.up());

                physics.Linear += math.forward(rotation.Value) * deltaTime * enemyData.MoveSpeed;

            }

        }).Run();

           
    }
           

}
        

