using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct SpawnSystem : ISystem
    {
        private uint _index;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PrefabExecute>();
            state.RequireForUpdate<Spawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery rotateCubeQuery = SystemAPI.QueryBuilder().WithAll<RotateSpeed>().Build();
            if (rotateCubeQuery.IsEmpty)
            {
                Entity prefab = SystemAPI.GetSingleton<Spawner>().PrefabEntity;
                int count = SystemAPI.GetSingleton<Spawner>().Count;
                Random random = Random.CreateFromIndex(_index++);

                for (int i = 0; i < count; i++)
                {
                    Entity entity = state.EntityManager.Instantiate(prefab);
                    RefRW<LocalTransform> transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                    transform.ValueRW.Position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;
                }
            }
        }
    }
}