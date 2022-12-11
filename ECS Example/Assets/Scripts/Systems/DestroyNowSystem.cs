using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;

public partial class DestroyNowSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithoutBurst().WithStructuralChanges()
           .WithName("DestroyNowSystem")
           .ForEach((Entity entity, ref DestroyNowData destroyNowData) =>
           {
               if (destroyNowData.shouldDestroy)
               {
                   UIManager.OnScored?.Invoke();
                   EntityManager.DestroyEntity(entity);
               }
                   
           })
           .Run();

    }
    
}

