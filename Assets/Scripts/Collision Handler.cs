using Unity.VisualScripting;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] Transform vfxPoint;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Instantiate(destroyedVFX, vfxPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
