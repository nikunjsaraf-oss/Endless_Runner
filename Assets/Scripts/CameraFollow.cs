using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}