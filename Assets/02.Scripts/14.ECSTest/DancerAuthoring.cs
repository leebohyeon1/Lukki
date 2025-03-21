using System;
using Unity.Entities;
using UnityEngine;

public struct Dancer : IComponentData
{
    public float Speed;

    public static Dancer Random(uint seed)
        => new Dancer() { Speed = new Unity.Mathematics.Random(seed).NextFloat(1, 8) };
}

public class DancerAuthoring : MonoBehaviour
{
    public float Speed = 1.0f;

    class Baker : Baker<DancerAuthoring>
    {
        public override void Bake(DancerAuthoring src)
        {
            var data = new Dancer()
            {
                Speed = src.Speed

            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}