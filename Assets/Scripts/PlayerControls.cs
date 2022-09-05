using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float controlSpeed = 30f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 7f;

    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -15f;

    [SerializeField] private float positionYawFactor = 2.25f;
    [SerializeField] private float controlYawFactor = 0f;

    [SerializeField] private float positionRollFactor = 0f;
    [SerializeField] private float controlRollFactor = -25f;

    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessShooting();
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(rawYPos, -yRange, yRange);        

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);
    }
    
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch =  pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;
        float yaw =  yawDueToPosition + yawDueToControlThrow;

        float rollDueToPosition = transform.localPosition.x * positionRollFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;
        float roll =  rollDueToPosition + rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessShooting()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
