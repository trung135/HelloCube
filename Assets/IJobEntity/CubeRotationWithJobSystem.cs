using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    [DisableAutoCreation]
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
            CubeRotationJob job = new CubeRotationJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            job.Schedule();
        }
    }

    [BurstCompile]
    public partial struct CubeRotationJob : IJobEntity
    {
        public float DeltaTime;
        
        public void Execute(LocalTransform transform, RotateSpeed speed)
        {
            transform = transform.RotateY(speed.Value * DeltaTime);
        }
    }
}