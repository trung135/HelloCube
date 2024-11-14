using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace HelloCube
{
    public partial struct InitDirectorySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameObjectSyncExecute>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            GameObject go = GameObject.Find("Directory");
            Directory directory = go.GetComponent<Directory>();
            DirectoryManaged directoryManaged = new DirectoryManaged(directory.Prefab, directory.Toggle);

            Entity entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, directoryManaged);
        }
    }
}