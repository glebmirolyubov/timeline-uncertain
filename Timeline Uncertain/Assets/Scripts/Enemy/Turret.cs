/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 * This is in prototype state, additional comments will be added during later development
 * 
 */

using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // this variables is static and public so I could control it from outside of this script
    public static bool shotReady;
    public static bool targetLocked;

    public AudioClip turretShotSound;
    public GameObject turret;
    public GameObject bullet;
    public GameObject bulletStartPosition;
    public float fireTimer;

    private GameObject target;
    private AudioSource turretAudioSource;

    void Start()
    {
        turretAudioSource = GetComponent<AudioSource>();
        shotReady = true;
        targetLocked = false;
    }

    void Update()
    {
        // shooting and detecting player
        if (targetLocked && target != null)
        {
            turret.transform.LookAt(target.transform);
            turret.transform.Rotate(0, 180, 0);

            if (shotReady)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Transform bulletProjectile = Instantiate(bullet.transform, bulletStartPosition.transform.position, Quaternion.identity);
        bulletProjectile.transform.parent = gameObject.transform;
        bulletProjectile.transform.rotation = bulletStartPosition.transform.rotation;
        shotReady = false;
        turretAudioSource.clip = turretShotSound;
        turretAudioSource.Play();
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate ()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            targetLocked = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target = null;
            targetLocked = false;
        }
    }
}
