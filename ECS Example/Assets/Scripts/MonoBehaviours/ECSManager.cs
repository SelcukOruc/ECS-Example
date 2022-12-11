using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    EntityManager manager;
    World world;
    BlobAssetStore store;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject enemyPrefab;


    private const int enemyNumber = 100;

    [SerializeField] private CharacterTracker characterTracker;


    [Header("VARIABLES")]
    [Header("Player")]
    [SerializeField] private float playerMoveSpeed = 30;
    [SerializeField] private float playerRotationSpeed = 180;
    [Header("Enemy")]
    [SerializeField] private float enemyMoveSpeed = 15;
    [SerializeField] private float enemyHealth = 10;



    private void Start()
    {
        store = new BlobAssetStore();
        world = World.DefaultGameObjectInjectionWorld;
        manager = world.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(world, store);

        Entity playerentity = GameObjectConversionUtility.ConvertGameObjectHierarchy(playerPrefab,settings);
        Entity bulletentity = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, settings);

        Entity player = manager.Instantiate(playerentity);
        manager.SetComponentData(player, new Translation { Value = new float3(0, 2, 0) });
        manager.SetComponentData(player, new PlayerData { MoveSpeed = this.playerMoveSpeed, RotationSpeed = this.playerRotationSpeed, Bullet = bulletentity });

        characterTracker.SetTargetEntity(player);

        
        Entity enemyentity = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyPrefab, settings);

        for (int i = 0; i < enemyNumber; i++)
        {
            Entity enemy = manager.Instantiate(enemyentity);
            float3 randomPos = new float3(UnityEngine.Random.Range(-20, 20), 2, UnityEngine.Random.Range(-20, 20));
            manager.SetComponentData(enemy, new Translation { Value = randomPos });
            manager.SetComponentData(enemy, new EnemyData { Target = player, MoveSpeed = this.enemyMoveSpeed });
            manager.SetComponentData(enemy, new DestroyNowData { shouldDestroy = false, Health = enemyHealth});
        }


    }

    private void OnDestroy()
    {
        store.Dispose();
    }

}
