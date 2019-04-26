using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

    bool charMovement;
    public GameObject gameMenu;

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
        DialogueSetScript.OnEventDialougeStart += StopMovement;
        DialogueSetScript.OnEventNoDialouge += StartMovement;
    }

    // When game object is disabled
    void OnDisable()
    {
        DialogueSetScript.OnEventDialougeStart -= StopMovement;
        DialogueSetScript.OnEventNoDialouge -= StartMovement;
    }

    // Use this for initialization
    void Start () {
        charMovement = true;
        MenuInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenu.activeInHierarchy == true)
        {
            StopMovement();
        }

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameMenu.activeInHierarchy == false)
            {
                MenuActive();
            }
            else
            {
                MenuInactive();
            }
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

    public void MenuActive()
    {
        gameMenu.SetActive(true);
    }

    public void MenuInactive()
    {
        gameMenu.SetActive(false);
    }
}
