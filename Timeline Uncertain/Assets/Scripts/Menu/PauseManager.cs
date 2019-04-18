/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private LevelLoader levelLoader;

    bool pauseEnabled;

    void Start()
    {
        pauseEnabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
            pauseMenu.SetActive(true);
            pauseEnabled = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && pauseEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
            pauseMenu.SetActive(false);
            pauseEnabled = false;
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
        pauseMenu.SetActive(false);
        pauseEnabled = false;
        Debug.Log("Resume");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        levelLoader.LoadLevel(0);
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
