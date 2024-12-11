using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    Vector3 pos;
    Vector3 forward;
    Vector3 left;
    Vector3 right;
    Vector3 up;
    Vector3 down;
    public float ForwardDistance = 0;
    public float LeftDistance = 0;
    public float RightDistance = 0;
    public float UpDistance = 0;
    public float DownDistance = 0;

    public float raycastRange = 30;

    private void Update()
    {
        forward = transform.TransformDirection(Vector3.forward) * raycastRange;
        left = transform.TransformDirection(new Vector3(.5f, 0, 1)) * raycastRange;
        right = transform.TransformDirection(new Vector3(-.5f, 0, 1)) * raycastRange;
        up = transform.TransformDirection(Vector3.up) * raycastRange;
        down = transform.TransformDirection(Vector3.down) * raycastRange;

        pos = transform.position;

        Debug.DrawRay(pos, forward, Color.red);
        Debug.DrawRay(pos, left, Color.blue);
        Debug.DrawRay(pos, right, Color.green);
        Debug.DrawRay(pos, up, Color.yellow);
        Debug.DrawRay(pos, down, Color.cyan);
    }

    private void FixedUpdate()
    {
        ForwardDistance = GetRaycastDistance(pos, forward);
        LeftDistance = GetRaycastDistance(pos, left);
        RightDistance = GetRaycastDistance(pos, right);
        UpDistance = GetRaycastDistance(pos, up);
        DownDistance = GetRaycastDistance(pos, down);
    }

    private float GetRaycastDistance(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, raycastRange))
        {
            return hit.distance / raycastRange; // Normaliza la distancia
        }
        return 1; // Sin obstáculos
    }
}
