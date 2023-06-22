using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    public Vector3 zoneSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }

}