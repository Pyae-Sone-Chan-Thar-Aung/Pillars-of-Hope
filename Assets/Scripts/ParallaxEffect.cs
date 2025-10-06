using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Start position for the parallax object
    Vector2 startingPosition;
    float startingZ;

    // How far the camera has moved since the start
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // Distance from the camera to the target
    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    // Clipping plane distance based on z distance
    float clippingPlane => (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);

    // Parallax factor
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
