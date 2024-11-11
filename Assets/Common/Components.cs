using Unity.Entities;

public struct RotateSpeed : IComponentData
{
    public float Value;
}

public struct Spawner : IComponentData
{
    public Entity PrefabEntity;
    public int Count;
}