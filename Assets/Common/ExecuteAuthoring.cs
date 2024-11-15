using Unity.Entities;
using UnityEngine;

public class ExecuteAuthoring : MonoBehaviour
{
    public bool MainThread;
    public bool IJobEntity;
    public bool IAspect;
    public bool Prefab;
    public bool Reparenting;
    public bool EnableComponent;
    public bool GameObjectSync;

    private class ExecuteAuthoringBaker : Baker<ExecuteAuthoring>
    {
        public override void Bake(ExecuteAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            if (authoring.MainThread) AddComponent<MainThreadExecute>(entity);
            if (authoring.IJobEntity) AddComponent<IJobEntityExecute>(entity);
            if (authoring.IAspect) AddComponent<IAspectExecute>(entity);
            if (authoring.Prefab) AddComponent<PrefabExecute>(entity);
            if (authoring.Reparenting) AddComponent<ReparentingExecute>(entity);
            if (authoring.EnableComponent) AddComponent<EnableComponentExecute>(entity);
            if (authoring.GameObjectSync) AddComponent<GameObjectSyncExecute>(entity);
        }
    }
}

public struct MainThreadExecute : IComponentData {}
public struct IJobEntityExecute : IComponentData {}
public struct IAspectExecute : IComponentData {}
public struct PrefabExecute : IComponentData {}
public struct ReparentingExecute : IComponentData {}
public struct EnableComponentExecute : IComponentData {}
public struct GameObjectSyncExecute : IComponentData {}