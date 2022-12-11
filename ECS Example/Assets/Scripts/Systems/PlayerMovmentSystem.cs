using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
public partial class PlayerMovmentSystem : SystemBase
{
    protected override void OnUpdate()
    {

        float deltaTime = Time.DeltaTime;
        float3 direction = new float3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
        

        // Move And Rotate
        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref PlayerData playerData,ref PhysicsVelocity physics,ref PhysicsMass mass) => {

            physics.Linear += math.forward(rotation.Value) * deltaTime * playerData.MoveSpeed * direction.z;

            quaternion rot = quaternion.RotateY(Mathf.Atan2(direction.x, direction.z) * Mathf.Deg2Rad * deltaTime * playerData.RotationSpeed);
            rotation.Value = math.mul(rotation.Value, rot);


            mass.InverseInertia[0] = 0;
            mass.InverseInertia[1] = 0;
            mass.InverseInertia[2] = 0;

        }).Schedule();

        Dependency.Complete();


        bool isShooting = Input.GetMouseButtonDown(0) ? isShooting = true : isShooting = false;

        

        Entities.WithBurst().WithStructuralChanges().ForEach((ref PlayerData playerData, ref Translation translation,ref Rotation rotation) =>
        {
            if (isShooting)
            {
                Entity bullet = World.DefaultGameObjectInjectionWorld.EntityManager.Instantiate(playerData.Bullet);
               
                
                EntityManager.SetComponentData(bullet, new Translation { Value = translation.Value });
                EntityManager.SetComponentData(bullet, new BulletData { playerRot = rotation.Value, MoveSpeed = 50 });
                EntityManager.SetComponentData(bullet, new DestoryTimerData { LifeTime = 2 });

                EffectManager.Instance.PlayShootingSFX();

            }
                
        }).Run();


        

        
    }
            
           




}
