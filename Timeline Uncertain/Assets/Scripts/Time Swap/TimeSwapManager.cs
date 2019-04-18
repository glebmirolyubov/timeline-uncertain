/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeSwapManager : MonoBehaviour
{
    public Animator playerAnimator;

    public Transform leftArmBone;

    public GameObject timeline_1;
    public GameObject timeline_2;

    public GameObject pressQText;
    public GameObject rechargingText;

    public Image timeSwapImage;
    public Image timeSwapEffect;

    public AudioSource timeSwapSound;

    [SerializeField] private Material skyboxOne;
    [SerializeField] private Material skyboxTwo;

    float rechargeTime = 2.0f;
    float colorChangeTime = 0f;
    bool isRecharging = false;
    bool timeSwap = false;

    void Start()
    {
        timeline_1.SetActive(true);
        timeline_2.SetActive(false);

        pressQText.SetActive(true);
        rechargingText.SetActive(false);

        timeSwapEffect.color = new Color(255f, 255f, 255f, 0f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && isRecharging == false)
        {
            SwapTimeline();
            timeSwap = true;
        }

        if (isRecharging)
        {
            RechargeTimeSwap();
        }
    }

    void LateUpdate()
    {
        if (timeSwap)
        {
            float shoulderSpin = 0;
            shoulderSpin += Input.GetAxisRaw("Mouse X") * 1f;
            leftArmBone.eulerAngles = new Vector3(70f, shoulderSpin, 40f);
            leftArmBone.rotation = Camera.main.transform.rotation * leftArmBone.rotation;
        }
    }

    void SwapTimeline ()
    {
        if (timeline_1.activeInHierarchy)
        {
            timeline_1.SetActive(false);
            timeline_2.SetActive(true);
            timeSwapImage.fillAmount = 0f;
            timeSwapEffect.color = new Color(255f, 255f, 255f, 0.5f);
            colorChangeTime = 0f;
            timeSwapSound.Play();
            // only if at second level
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                RenderSettings.skybox = skyboxTwo;
            }
        } else if (timeline_2.activeInHierarchy)
        {
            timeline_2.SetActive(false);
            timeline_1.SetActive(true);
            timeSwapImage.fillAmount = 0f;
            timeSwapEffect.color = new Color(255f, 255f, 255f, 0.5f);
            colorChangeTime = 0f;
            timeSwapSound.Play();
            Physics.gravity = new Vector3(0, -18.81f, 0);
            Turret.shotReady = true;
            Turret.targetLocked = false;
            // only if at second level
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                RenderSettings.skybox = skyboxOne;
            }
        }

        isRecharging = true;

        playerAnimator.SetTrigger("TimeSwap");
        pressQText.SetActive(false);
        rechargingText.SetActive(true);
    }

    void RechargeTimeSwap ()
    {
        rechargeTime -= Time.deltaTime;
        timeSwapImage.fillAmount += 0.35f / rechargeTime * Time.deltaTime;
        timeSwapEffect.color = Color.Lerp(new Color(255f, 255f, 255f, 0.5f), new Color(255f, 255f, 255f, 0f), colorChangeTime);
        colorChangeTime += Time.deltaTime / rechargeTime;

        if (rechargeTime < 1.2f)
        {
            timeSwap = false;
        }

        if (rechargeTime < 0f)
        {
            isRecharging = false;
            rechargeTime = 2.0f;

            pressQText.SetActive(true);
            rechargingText.SetActive(false);

            timeSwapImage.fillAmount = 1.0f;
        }
    }
}
