using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube
{
    public partial struct ReparentingSystem : ISystem
    {
        const float Interval = 1f;
        private float _timer;
        private bool _attached;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ReparentingExecute>();
            state.RequireForUpdate<RotateSpeed>();
            
            _timer = Interval;
            _attached = true;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _timer -= SystemAPI.Time.DeltaTime;
            if (_timer > 0) return;
            _timer = Interval;
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var entityParent = SystemAPI.GetSingletonEntity<RotateSpeed>();
            if (_attached)
            {
                DynamicBuffer<Child> children = SystemAPI.GetBuffer<Child>(entityParent);
                for (int i = 0; i < children.Length; i++)
                {
                    ecb.RemoveComponent<Parent>(children[i].Value);
                }
            }
            else
            {
                foreach (var (transform, entity) in
                         SystemAPI.Query<RefRW<LocalTransform>>()
                             .WithNone<RotateSpeed>()
                             .WithEntityAccess())
                {
                    ecb.AddComponent(entity, new Parent { Value = entityParent });
                }
                // var query = SystemAPI.QueryBuilder().WithAll<LocalTransform>().WithNone<RotateSpeed>().Build();
                // ecb.AddComponent(query, new Parent {Value = entityParent});
            }
            ecb.Playback(state.EntityManager);

            _attached = !_attached;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}