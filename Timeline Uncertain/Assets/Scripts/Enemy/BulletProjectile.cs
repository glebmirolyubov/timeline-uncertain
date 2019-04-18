/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using System.Collections;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float movementSpeed;
    public float damage;
    private GameObject target;

    void Start()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    IEnumerator DestroyAfterSeconds ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            target.GetComponent<PlayerHealth>().health -= damage;
            target.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }

        if (other.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
}
