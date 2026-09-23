using Unity.VisualScripting;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Instantiate(destroyedVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
