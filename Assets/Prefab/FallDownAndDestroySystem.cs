using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace HelloCube
{
    public partial struct FallDownAndDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RotateSpeed>();
            state.RequireForUpdate<PrefabExecute>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, speed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
            {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.Value * SystemAPI.Time.DeltaTime);
            }

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            float3 movement = new float3(0, -SystemAPI.Time.DeltaTime * 5f, 0);
            foreach (var (transform, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<RotateSpeed>()
                         .WithEntityAccess())
            {
                transform.ValueRW.Position += movement;
                if (transform.ValueRW.Position.y < 0)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }

    }
}