using Unity.Entities;
using UnityEngine;

class CubeEnableAuthoring : MonoBehaviour
{
    public float rotationSpeed;
    public bool enableRotation;

    class CubeEnableAuthoringBaker : Baker<CubeEnableAuthoring>
    {
        public override void Bake(CubeEnableAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                Value = authoring.rotationSpeed
            });
            SetComponentEnabled<RotateSpeed>(entity, authoring.enableRotation);
        }
    }
}