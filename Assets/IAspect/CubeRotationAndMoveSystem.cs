using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct CubeRotationAndMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<IAspectExecute>();
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
    
            foreach (var movement in 
                     SystemAPI.Query<VerticalMovement>())
            {
                movement.Move(SystemAPI.Time.ElapsedTime);
            }
        }
    }
    
    public readonly partial struct VerticalMovement : IAspect
    {
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<RotateSpeed> _speed;
    
        public void Move(double elapseTime)
        {
            _transform.ValueRW.Position.y = (float)math.sin(elapseTime * _speed.ValueRO.Value);
        }
    }
}