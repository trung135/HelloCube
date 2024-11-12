using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ReparentingExecute>();
            state.RequireForUpdate<RotateSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, speed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
            {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.Value * SystemAPI.Time.DeltaTime);
            }
        }
    }
}