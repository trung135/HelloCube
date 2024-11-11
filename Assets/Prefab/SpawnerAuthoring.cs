using Unity.Entities;
using UnityEngine;

class SpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int count;
    class SpawnerAuthoringBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Spawner
            {
                PrefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                Count = authoring.count
            });
        }
    }
}