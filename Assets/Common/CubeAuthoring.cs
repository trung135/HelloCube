using Unity.Entities;
using UnityEngine;

class CubeAuthoring : MonoBehaviour
{
    public float rotationSpeed;
    public bool isEnableRotate;
    class CubeAuthoringBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                Value = authoring.rotationSpeed,
                IsRotate = authoring.isEnableRotate
            });
        }
    }
}