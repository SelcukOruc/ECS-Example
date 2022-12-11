using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
public partial class BulletMovmentSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        float deltaTime = Time.DeltaTime;
        
        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref BulletData bulletData,ref PhysicsVelocity physics) => {

            translation.Value += math.forward(bulletData.playerRot) * deltaTime*bulletData.MoveSpeed;

        }).Schedule();

        

       

    }
}
