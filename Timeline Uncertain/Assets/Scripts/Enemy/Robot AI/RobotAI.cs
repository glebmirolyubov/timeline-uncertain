/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RobotAI : MonoBehaviour
{
    [SerializeField] private AudioClip[] robotFootstepSounds;
    [SerializeField] private Light robotLight;

    private NavMeshAgent robotAgent;
    private GameObject player;
    private Animator robotAnimator;
    private AudioSource audioSource;
    private RaycastHit playerHit;

    bool startFollowingPlayer;
    bool followingPlayer;
    bool hitPlayer;
    float distanceToPlayer;

    void Start()
    {
        robotAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        robotAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startFollowingPlayer = false;
        followingPlayer = false;
        hitPlayer = true;
    }

    void Update()
    {
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer >= 2f && startFollowingPlayer)
            {
                robotAgent.isStopped = false;
                robotAgent.destination = player.transform.position;
                robotAnimator.SetBool("Move", true);
                followingPlayer = true;
                startFollowingPlayer = false;
            }
            else if (distanceToPlayer < 2f && followingPlayer)
            {
                robotAgent.isStopped = true;
                robotAnimator.SetBool("Move", false);
                AttackPlayer();
                followingPlayer = false;
            }

            if (followingPlayer)
            {
                robotAgent.destination = player.transform.position;
                if (!audioSource.isPlaying)
                {
                    PlayFootsteps();
                }
            }
        }
    }

    void AttackPlayer()
    {
        robotAnimator.SetTrigger("Attack");
        StartCoroutine("OnCompleteAttackAnimation");
    }

    IEnumerator OnCompleteAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 playerLookingDir = player.transform.forward;
        player.GetComponent<Rigidbody>().AddForce(playerLookingDir * -500, ForceMode.Impulse);
        player.GetComponent<PlayerHealth>().TakeDamage();
        player.GetComponent<PlayerHealth>().health -= 20f;
        startFollowingPlayer = true;
    }

    void PlayFootsteps()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, robotFootstepSounds.Length);
        audioSource.clip = robotFootstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        robotFootstepSounds[n] = robotFootstepSounds[0];
        robotFootstepSounds[0] = audioSource.clip;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            startFollowingPlayer = true;
            robotLight.color = Color.red;
        }
    }
}
