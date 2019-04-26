using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject otherChar;
    Vector3 originalCamPos;
    public float shakeTime = 0f;
    public float shakeAmount = 0.5f;
    public float decreaseAmount = 0.4f;
    bool activeDia;

    // When game object is enabled
    void OnEnable()
    {
        PlayerMovement2D.OnEventShakeCamera += CameraShake;
        DialougeScript.OnEventDialougeStart += ZoomToDialouge;
        DialougeScript.OnEventNoDialouge += TrackPlayerMovement;
    }

    // When game object is disabled
    void OnDisable()
    {
        PlayerMovement2D.OnEventShakeCamera -= CameraShake;
        DialougeScript.OnEventDialougeStart -= ZoomToDialouge;
        DialougeScript.OnEventNoDialouge -= TrackPlayerMovement;
    }


    // Use this for initialization
    void Start()
    { 
        TrackPlayerMovement();
    }

    // Update is called once per frame
    void Update()
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

        if(activeDia)
        {
            GetComponent<Camera>().transform.position = new Vector3(otherChar.transform.position.x, otherChar.transform.position.y, this.transform.position.z);
        }

    }

    public void TrackPlayerMovement()
    {
        if (shakeTime == 0)
        {
            GetComponent<Camera>().transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -9f);
            if(GetComponent<Camera>().orthographicSize != 4f)
                GetComponent<Camera>().orthographicSize = 4f;
        }
        originalCamPos = GetComponent<Camera>().transform.position;
        activeDia = false;
    }

    public void ZoomToDialouge()
    {
        GetComponent<Camera>().transform.position = new Vector3(otherChar.transform.position.x, otherChar.transform.position.y, this.transform.position.z);
        GetComponent<Camera>().orthographicSize = 0.8f;
        activeDia = true;
    }

    public void CameraShake(float shakeDuration)
    {
        shakeTime = shakeDuration;
    }

}
