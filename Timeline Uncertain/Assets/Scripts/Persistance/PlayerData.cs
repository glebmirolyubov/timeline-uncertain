/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;

    public PlayerData (Player player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.position[0];
        position[1] = player.position[1];
        position[2] = player.position[2];

    }
}
