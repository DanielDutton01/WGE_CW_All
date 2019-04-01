using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

    bool charMovement;

    public delegate void JumpInput();
    public delegate void JumpReleaseInput();
    public delegate void JumpPressedInput();
    public delegate void HorizontalMoveInput(float x);
    public delegate void DashPressedInput(Vector2 direction);

    public event JumpInput _jumpInput;
    public event JumpReleaseInput _jumpReleaseInput;
    public event HorizontalMoveInput _hMoveInput;
    public event JumpPressedInput _jumpPressedInput;
    public event DashPressedInput _dashPressedInput;

    // When game object is enabled
    void OnEnable()
    {
        DialougeScript.OnEventDialougeSpeaker += StopMovement;
        DialougeScript.OnEventNoDialouge += StartMovement;
    }

    // When game object is disabled
    void OnDisable()
    {
        DialougeScript.OnEventDialougeSpeaker -= StopMovement;
        DialougeScript.OnEventNoDialouge -= StartMovement;
    }

    // Use this for initialization
    void Start () {
        charMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");
        if (charMovement == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _jumpPressedInput();
            }
            if (Input.GetButton("Jump"))
            {
                _jumpInput();
            }
            if (Input.GetButtonUp("Jump"))
            {
                _jumpReleaseInput();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _dashPressedInput(new Vector2(hMove, vMove));
            }

            _hMoveInput(hMove);
        }
    }

    void StopMovement()
    {
        charMovement = false;
    }

    void StartMovement()
    {
        charMovement = true;
    }
}
