using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public partial class BulletCollisionEventSystem : SystemBase
{
    
    StepPhysicsWorld stepWorld;

    protected override void OnCreate() => stepWorld = World.GetOrCreateSystem<StepPhysicsWorld>();


    struct CollisionEventJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<BulletData> BulletGroup;
        public ComponentDataFromEntity<DestroyNowData> DestroyNowGroup;



        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.EntityA;
            Entity entityB = collisionEvent.EntityB;

            bool isTargetA = DestroyNowGroup.HasComponent(entityA);
            bool isTargetB = DestroyNowGroup.HasComponent(entityB);

            bool isBulletA = BulletGroup.HasComponent(entityA);
            bool isBulletB = BulletGroup.HasComponent(entityB);

            
            if (isBulletA && isTargetB)
            {
                var destroyComponent = DestroyNowGroup[entityB];
                
                destroyComponent.Health -= 1;
                if (destroyComponent.Health <= 0)
                    destroyComponent.shouldDestroy = true;

                DestroyNowGroup[entityB] = destroyComponent;
            }

            if (isBulletB && isTargetA)
            {
                var destroyComponent = DestroyNowGroup[entityA];
                
                destroyComponent.Health -= 1;
                if (destroyComponent.Health <= 0)
                    destroyComponent.shouldDestroy = true;

                DestroyNowGroup[entityA] = destroyComponent;
            }

        }
    
    
    }

   

    protected override void OnUpdate()
    {
        JobHandle jobHandle = new CollisionEventJob
        {
            BulletGroup = GetComponentDataFromEntity<BulletData>(),
            DestroyNowGroup = GetComponentDataFromEntity<DestroyNowData>()
       
        }.Schedule(stepWorld.Simulation,Dependency);

        jobHandle.Complete();
    }
}
