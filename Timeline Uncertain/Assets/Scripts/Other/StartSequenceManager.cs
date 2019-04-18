/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSequenceManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject[] canvasObjects;

    void Start()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            ConfigureFirstLevel();
        } else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            ConfigureSecondLevel();
        }
    }

    void ConfigureFirstLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        try
        {
            player.GetComponent<Player>().LoadPlayer();
            playerAnimator.Play("Idle", 0);
        }
        catch
        {
            CameraFollowPlayer.cameraRot = true;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<TimeSwapManager>().enabled = false;

            foreach (GameObject g in canvasObjects)
            {
                g.SetActive(false);
            }
        }
    }

    void ConfigureSecondLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        try
        {
            player.GetComponent<Player>().LoadPlayer();
        }
        catch
        {
            
        }
        
        playerAnimator.Play("Idle", 0);
    }
}
