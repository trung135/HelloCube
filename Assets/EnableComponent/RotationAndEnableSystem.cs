using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct RotationAndEnableSystem : ISystem
    {
        const float Interval = 1f;
        private float _timer;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnableComponentExecute>();
            state.RequireForUpdate<EnableRotateSpeed>();

            _timer = Interval;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _timer -= SystemAPI.Time.DeltaTime;
            if (_timer < 0)
            {
                _timer = Interval;
                foreach (var rotateEnable in
                         SystemAPI.Query<EnabledRefRW<EnableRotateSpeed>>()
                             .WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
                {
                    rotateEnable.ValueRW = !rotateEnable.ValueRO;
                }
            }

            foreach (var (transform, speed) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnableRotateSpeed>>())
            {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.Value * SystemAPI.Time.DeltaTime);
            }
        }
    }
}