/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    private Player player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player = GetComponent<Player>();

        if (File.Exists(Application.persistentDataPath + "/player.lol"))
        {
            continueButton.interactable = true;
            player.LoadPlayer();
            if (player.level == 2)
            {
                Camera.main.transform.position = new Vector3(-0.18f, -4.12f, -8.58f);
                Camera.main.transform.rotation = Quaternion.Euler(-9.81f, 14.71f, 0f);
            }
        }
    }

    public void ContinueGame()
    {
        player.LoadPlayer();
        GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().LoadLevel(player.level);
    }

    public void StartNewGame ()
    {
        File.Delete(Application.persistentDataPath + "/player.lol");
        GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>().LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
