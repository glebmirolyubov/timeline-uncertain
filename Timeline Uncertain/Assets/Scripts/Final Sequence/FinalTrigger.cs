using UnityEngine;
using System.Collections;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] private Animation doorAnimation;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject deathSteam;
    [SerializeField] private GameObject player;
    [SerializeField] private Animation finalPanel;

    bool trigger = false;


    void Start()
    {
        trigger = false;
        deathSteam.SetActive(false);
    }

    IEnumerator FinalSequence()
    {
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerController>().enabled = false;    
        player.GetComponentInChildren<Animator>().SetTrigger("Death");
        finalPanel.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!trigger)
            {
                player.GetComponent<TimeSwapManager>().enabled = false;
                deathSteam.SetActive(true);
                doorAnimation.Play();
                robot.SetActive(false);
                StartCoroutine(FinalSequence());
                trigger = true;
            }
        }
    }
}
