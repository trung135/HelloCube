using Unity.Entities;
using UnityEngine;

public class ExecuteAuthoring : MonoBehaviour
{
    public bool mainthread;
    public bool ijobentity;
    public bool iaspect;
    public bool prefab;
    private class ExecuteAuthoringBaker : Baker<ExecuteAuthoring>
    {
        public override void Bake(ExecuteAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            if (authoring.mainthread) AddComponent<MainThreadExecute>(entity);
            if (authoring.ijobentity) AddComponent<IJobEntityExecute>(entity);
            if (authoring.iaspect) AddComponent<IAspectExecute>(entity);
            if (authoring.prefab) AddComponent<PrefabExecute>(entity);
        }
    }
}

public struct MainThreadExecute : IComponentData {}
public struct IJobEntityExecute : IComponentData {}
public struct IAspectExecute : IComponentData {}
public struct PrefabExecute : IComponentData {}