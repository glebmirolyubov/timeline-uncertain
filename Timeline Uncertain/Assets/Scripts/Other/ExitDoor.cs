/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
