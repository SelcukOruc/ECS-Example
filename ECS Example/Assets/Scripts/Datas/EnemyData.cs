using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct EnemyData : IComponentData
{
    public Entity Target;
    public float MoveSpeed;
    
    
}
