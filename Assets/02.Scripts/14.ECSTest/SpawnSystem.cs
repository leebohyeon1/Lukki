using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

// Update Ÿ�̹��� ��������� ����
[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct SpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    => state.RequireForUpdate<Config>(); // ���� ���� Config ������Ʈ�� �����ϴ� ��쿡�� �е���Ʈ ����

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<Config>();

        var instances = state.EntityManager.Instantiate
            (config.Prefab, config.SpawnCount, Allocator.Temp);

        var rand = new Unity.Mathematics.Random(config.RandomSeed);
        foreach(var entity in instances)
        {
            var xform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            var dancer = SystemAPI.GetComponentRW<Dancer>(entity);
            var walker = SystemAPI.GetComponentRW<Walker>(entity);

            xform.ValueRW = LocalTransform.FromPositionRotation
                (rand.NextFloat3Direction() * config.SpawnRadius, rand.NextQuaternionRotation());

            dancer.ValueRW = Dancer.Random(rand.NextUInt());
            walker.ValueRW = Walker.Random(rand.NextUInt());
        }

        // ���� �� �ٽ� ������Ʈ�� ���� ������ ��Ȱ��ȭ
        state.Enabled = false;
    }
}
