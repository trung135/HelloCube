using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube
{
    public partial struct InitCubeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameObjectSyncExecute>();
            state.RequireForUpdate<RotateSpeed>();
            state.RequireForUpdate<DirectoryManaged>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            DirectoryManaged directoryManaged = SystemAPI.ManagedAPI.GetSingleton<DirectoryManaged>();
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (temp, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<RotateSpeed>()
                         .WithNone<CubeGameObject>()
                         .WithEntityAccess())
            {
                GameObject go = GameObject.Instantiate(directoryManaged.Prefab);
                ecb.AddComponent(entity, new CubeGameObject(go));
            }
            
            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}