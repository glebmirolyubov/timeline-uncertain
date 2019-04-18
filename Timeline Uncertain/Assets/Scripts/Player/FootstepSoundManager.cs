/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

public class FootstepSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] metalFootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip[] grassFootstepSounds;
    private AudioSource audioSource;
    private RaycastHit footstepHit;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound()
    {
        // on every footstep, generate raycast to detect the surface the player is currently on
        if (Physics.Raycast(transform.position, Vector3.down, out footstepHit))
        {
            string hitSurfaceName = footstepHit.collider.gameObject.tag;

            if (hitSurfaceName == "Grass")
            {
                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                int n = Random.Range(1, grassFootstepSounds.Length);
                audioSource.clip = grassFootstepSounds[n];
                audioSource.PlayOneShot(audioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                grassFootstepSounds[n] = grassFootstepSounds[0];
                grassFootstepSounds[0] = audioSource.clip;
            }else
            {
                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                int n = Random.Range(1, metalFootstepSounds.Length);
                audioSource.clip = metalFootstepSounds[n];
                audioSource.PlayOneShot(audioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                metalFootstepSounds[n] = metalFootstepSounds[0];
                metalFootstepSounds[0] = audioSource.clip;
            }
        } 
    }
}
