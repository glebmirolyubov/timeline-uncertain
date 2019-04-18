/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject damagePanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject deathRagdoll;

    private bool died = false;

    void Update()
    {
        if (health <= 0 && !died)
        {
            died = true;
            DieAndSpawnRagdoll();
        }
    }

    void DieAndSpawnRagdoll()
    {
        Instantiate(deathRagdoll, transform.position, transform.rotation);
        deathPanel.SetActive(true);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        healthBar.fillAmount -= 0.2f;
        damagePanel.SetActive(true);
    }
}
