using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [Header("Moving Ship - Settings")]

    [Tooltip("How fast you can move your ship")] 
    [SerializeField] float controlSpeed = 10f;

    [Tooltip("How far you can move yout ship Left and Right")]
    [SerializeField] float xRange = 22f;

    [Tooltip("How far you can move yout ship Up")]
    [SerializeField] float yMaxUP = 18f;

    [Tooltip("How far you can move yout ship Down")]
    [SerializeField] float yMaxDown = -8f;


    [Header("Rotating Ship - Settings")]

    [Tooltip("Screen position based tuning")]
    [SerializeField] float posisionPichFactor = -1.5f;
    [SerializeField] float posisionYawFactor = 1.5f;

    [Tooltip("Player input based tuning")]
    [SerializeField] float movePichFactor = -17f;
    [SerializeField] float moveRollFactor = -40f;


    [Header("Laser gun array")]

    [Tooltip("Add player lasers here")]
    [SerializeField] GameObject[] lasers;


    float xMove, yMove;

    void Update()
    {
        MovingPlayer();
        RotatingPlayer();
        FireProcess();
        BackToMenu();
    }

    void MovingPlayer()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        float xOffset = xMove * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = yMove * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, yMaxDown, yMaxUP);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void RotatingPlayer()
    {
        float pitchDueToPosistion = transform.localPosition.y * posisionPichFactor;
        float pitchDueToMove = yMove * movePichFactor;

        float yawDueToPosision = transform.localPosition.x * posisionYawFactor;

        float rollDueToMove = xMove * moveRollFactor;

        float pitch = pitchDueToPosistion + pitchDueToMove;
        float yaw = yawDueToPosision;
        float roll = rollDueToMove;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void FireProcess()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
        
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    void BackToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
