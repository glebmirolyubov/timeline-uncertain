using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    private LevelLoader levelLoader;

    void Start()
    {
        levelLoader = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        Physics.gravity = new Vector3(0, -18.81f, 0);
        yield return new WaitForSeconds(3f);
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
