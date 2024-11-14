using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct SyncRotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameObjectSyncExecute>();
            state.RequireForUpdate<RotateSpeed>();
            state.RequireForUpdate<CubeGameObject>();
            state.RequireForUpdate<DirectoryManaged>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            DirectoryManaged directoryManaged = SystemAPI.ManagedAPI.GetSingleton<DirectoryManaged>();
            if (!directoryManaged.Toggle.isOn) return;
            
            foreach (var (transform, speed, cubeGO) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, CubeGameObject>())
            {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.Value * SystemAPI.Time.DeltaTime);
                cubeGO.Value.transform.rotation = transform.ValueRO.Rotation;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}