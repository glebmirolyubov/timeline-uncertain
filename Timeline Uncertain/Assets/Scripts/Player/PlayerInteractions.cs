/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameObject ambientMusic;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vent")
        {
            Physics.gravity = new Vector3(0, 18.81f, 0);
        }

        if (other.tag == "Death")
        {
            GetComponent<PlayerHealth>().health = 0;
        }

        if (other.tag == "SavePoint")
        {
            GetComponent<Player>().SavePlayer();
        }

        if (other.tag == "MusicTrigger")
        {
            ambientMusic.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vent")
        {
            Physics.gravity = new Vector3(0, -18.81f, 0);
        }
    }
}
