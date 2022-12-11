using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class CharacterTracker : MonoBehaviour
{
    Entity characterEntity;
    public void SetTargetEntity(Entity _entity)
    {
        characterEntity = _entity;
    }

    private void LateUpdate()
    {
        if (characterEntity != null)
        {
            transform.position = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Translation>(characterEntity).Value;
        }
            
    }

      

}
