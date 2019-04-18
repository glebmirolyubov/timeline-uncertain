/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public static bool cameraRot = false;

    public Transform playerCameraPosition;
    public Transform player;

    void Update()
    {
        transform.position = playerCameraPosition.position;
        if (cameraRot)
        {
            transform.rotation = playerCameraPosition.rotation;
        }
    }
}
