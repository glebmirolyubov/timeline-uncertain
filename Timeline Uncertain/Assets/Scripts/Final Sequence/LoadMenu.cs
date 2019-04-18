using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    public void LoadMainMenu()
    {
        levelLoader.LoadLevel(0);
    }
}
