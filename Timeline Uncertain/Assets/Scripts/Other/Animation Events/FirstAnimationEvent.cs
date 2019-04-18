/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

public class FirstAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] canvasObjects;
    [SerializeField] private GameObject timeSwap3DText;

    public void EnableTimeSwap()
    {
        player.GetComponent<TimeSwapManager>().enabled = true;
        timeSwap3DText.SetActive(true);
    }

    public void EnableController()
    {
        player.GetComponent<PlayerController>().enabled = true;
        CameraFollowPlayer.cameraRot = false;

        foreach (GameObject g in canvasObjects)
        {
            g.SetActive(true);
        }

        player.GetComponent<Player>().SavePlayer();
    }

    public void Disable3DText()
    {
        timeSwap3DText.SetActive(false);
    }
}
