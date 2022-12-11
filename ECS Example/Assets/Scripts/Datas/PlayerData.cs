using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PlayerData : IComponentData
{
    public float MoveSpeed;
    public float RotationSpeed;
    public Entity Bullet;
    
    
}
