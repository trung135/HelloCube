using Unity.Entities;
using UnityEngine;

class CubeAuthoring : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    class CubeAuthoringBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                Value = authoring.rotationSpeed
            });
        }
    }
}