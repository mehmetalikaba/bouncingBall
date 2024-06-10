using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target; 

    void Update()
    {
        if (target != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = target.position.x;
            transform.position = newPosition;
        }
    }
}
