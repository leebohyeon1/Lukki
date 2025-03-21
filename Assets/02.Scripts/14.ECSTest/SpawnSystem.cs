using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

// Update 타이밍을 명시적으로 지정
[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct SpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    => state.RequireForUpdate<Config>(); // 월드 내에 Config 컴포넌트가 존재하는 경우에만 압데이트 실행

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

        // 스폰 후 다시 업데이트가 돌기 때문에 비활성화
        state.Enabled = false;
    }
}
