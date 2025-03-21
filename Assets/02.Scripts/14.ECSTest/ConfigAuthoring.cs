using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Config : IComponentData
{
    public Entity Prefab;
    public float SpawnRadius;
    public int SpawnCount;
    public uint RandomSeed;
}

public class ConfigAuthoring : MonoBehaviour
{
    public GameObject Prefab = null;
    public float SpawnRadius = 1f;
    public int SpawnCount = 10;
    public uint RandomSeed = 100;

    class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring src)
        {
            var data = new Config
            {
                // GetEntity는 기존의 게임오브젝트기반의 프리팹을 ECS로 베이크하는 기능이다 
                Prefab = GetEntity(src.Prefab, TransformUsageFlags.Dynamic),
                SpawnRadius = src.SpawnRadius,
                SpawnCount = src.SpawnCount,
                RandomSeed = src.RandomSeed
            };

            AddComponent(GetEntity(TransformUsageFlags.None), data);
        }
    }
}
