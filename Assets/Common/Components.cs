using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public struct RotateSpeed : IComponentData
{
    public float Value;
}

public struct EnableRotateSpeed : IComponentData, IEnableableComponent
{
    public float Value;
}

public struct Spawner : IComponentData
{
    public Entity PrefabEntity;
    public int Count;
}

public class DirectoryManaged : IComponentData
{
    public GameObject Prefab;
    public Toggle Toggle;
    
    public DirectoryManaged() {}

    public DirectoryManaged(GameObject prefab, Toggle toggle)
    {
        Prefab = prefab;
        Toggle = toggle;
    }
}

public class CubeGameObject : IComponentData
{
    public GameObject Value;
    
    public CubeGameObject() {}

    public CubeGameObject(GameObject value)
    {
        Value = value;
    }
}