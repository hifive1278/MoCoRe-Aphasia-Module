using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDrag : MonoBehaviour
{
    //https://forum.unity.com/threads/solved-moving-gameobject-along-x-and-z-axis-by-drag-and-drop-using-x-and-y-from-screenspace.488476/
    // Update is called once per frame
    void Update()
    {
        float planeY = 0;
        Transform draggingObject = transform;

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if (plane.Raycast(ray, out distance))
            {
                draggingObject.position = ray.GetPoint(distance); // distance along the ray
            }
        }
    }
}
