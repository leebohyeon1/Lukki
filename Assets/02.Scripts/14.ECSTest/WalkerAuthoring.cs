using System;
using Unity.Entities;
using UnityEngine;

public struct Walker : IComponentData
{
    public float ForwardSpeed;
    public float AngularSpeed;

    public static Walker Random(uint seed)
    {
        var random = new Unity.Mathematics.Random(seed);
        return new Walker()
        {
            ForwardSpeed = (float)random.NextFloat(0.1f,0.8f),
            AngularSpeed = (float)random.NextFloat(0.5f, 4)
        };
    }
}

public class WalkerAuthoring : MonoBehaviour
{
    public float ForwardSpeed = 1.0f;
    public float AngularSpeed = 1.0f;

    class Baker : Baker<WalkerAuthoring>
    {
        public override void Bake(WalkerAuthoring src)
        {
            var data = new Walker()
            {
                ForwardSpeed = src.ForwardSpeed,
                AngularSpeed = src.AngularSpeed
            };

            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }

    }

}
