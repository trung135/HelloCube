using UnityEngine;
using UnityEngine.UI;

public class Directory : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Toggle toggle;
    
    public GameObject Prefab => prefab;
    public Toggle Toggle => toggle;
}