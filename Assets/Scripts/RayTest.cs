using UnityEngine;

public class RayTest : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        Debug.Log("SCRIPT IS RUNNING"); // Does this show up?
        
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        
        Debug.Log("Ray origin: " + ray.origin);
        Debug.Log("Ray direction: " + ray.direction);
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("!!!! HIT SOMETHING: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("!!!! HIT NOTHING");
        }
    }
}