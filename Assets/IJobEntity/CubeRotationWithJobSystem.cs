using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct CubeRotationWithJobSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<IJobEntityExecute>();
            state.RequireForUpdate<RotateSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            CubeRotationJob job = new CubeRotationJob
            {
                DeltaTime = deltaTime
            };
            job.Schedule();
        }
    }

    [BurstCompile]
    public partial struct CubeRotationJob : IJobEntity
    {
        public float DeltaTime;
        
        public void Execute(ref LocalTransform transform, in RotateSpeed speed)
        {
            transform = transform.RotateY(speed.Value * DeltaTime);
        }
    }
}