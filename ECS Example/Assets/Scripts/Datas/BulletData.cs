using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[GenerateAuthoringComponent]
public struct BulletData : IComponentData
{
    public float MoveSpeed;
    public quaternion playerRot;
    
}
    
