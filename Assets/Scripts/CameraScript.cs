﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject otherChar;
    public Transform camTransform;
    Vector3 originalCamPos;
    public float shakeTime = 0f;
    public float shakeAmount = 0.5f;
    public float decreaseAmount = 0.4f;
    bool dialouge;
    int i = 1;


    // When game object is enabled
    void OnEnable()
    {
        DialougeScript.OnEventDialougeSpeaker += TrackOtherDialouge;
        DialougeScript.OnEventNoDialouge += TrackPlayerMovement;
        DialougeScript.OnEventDialougeResponse += TrackPlayerDialouge;
    }

    // When game object is disabled
    void OnDisable()
    {

        DialougeScript.OnEventDialougeSpeaker -= TrackOtherDialouge;
        DialougeScript.OnEventNoDialouge -= TrackPlayerMovement;
        DialougeScript.OnEventDialougeResponse += TrackPlayerMovement;
    }


    // Use this for initialization
    void Start()
    { 
        TrackPlayerMovement();
        originalCamPos = GetComponent<Camera>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraShake();
    }

    public void TrackPlayerMovement()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
    }

    public void TrackPlayerDialouge()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        Camera.current.orthographicSize = 1.3f;

    }

    public void TrackOtherDialouge()
    {
        transform.position = new Vector3(otherChar.transform.position.x, otherChar.transform.position.y, this.transform.position.z);
        Camera.current.orthographicSize = 1.3f;
    }

    public void CameraShake()
    {
        if (shakeTime > 0)
        { 
            GetComponent<Camera>().transform.position = originalCamPos + Random.insideUnitSphere * shakeAmount;
            shakeTime -= Time.deltaTime * decreaseAmount;
        }
        else
        {
            shakeTime = 0f;
            GetComponent<Camera>().transform.position = originalCamPos;
        }
    }

}
