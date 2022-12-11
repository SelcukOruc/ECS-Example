using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class DestroyTimerSystem : SystemBase
{
    protected override void OnUpdate()
    {


        float deltaTime = Time.DeltaTime;
        
        Entities.WithBurst().WithStructuralChanges().ForEach((Entity entity, ref DestoryTimerData destroyTimerData) => 
        {
            destroyTimerData.LifeTime -= deltaTime;

            if (destroyTimerData.LifeTime <= 0)
                EntityManager.DestroyEntity(entity);


        }).Run();
    }
}
