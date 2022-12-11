using Unity.Entities;

[GenerateAuthoringComponent]
public struct DestroyNowData : IComponentData
{
    public float Health;
    public bool shouldDestroy;
}
