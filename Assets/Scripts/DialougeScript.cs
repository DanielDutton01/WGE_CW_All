using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeScript : MonoBehaviour {

    bool dialougeActive;
    GUIStyle guiStyle = new GUIStyle();
    string[] textList = { "The", "Test", "Is", "Done" };
    string[] responseList = { "O1", "O2", "O3"};
    int i = 0;

    // delegate signature
    public delegate void EventDialouge();

    // event instances for EventStartDialouge
    public static event EventDialouge OnEventDialougeSpeaker;
    public static event EventDialouge OnEventDialougeResponse;
    public static event EventDialouge OnEventNoDialouge;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(dialougeActive == false)
        {
            OnEventNoDialouge();
        }
	}

    private void OnGUI()
    {
        guiStyle.fontSize = 20;

        if (dialougeActive == true)
        {
            GUI.Label(new Rect(10, 200, 900, 75), textList[i], guiStyle);
            Debug.Log("Clicked the button with text");
            if (GUI.Button(new Rect(10, 400, 900, 30), responseList[0]))
            {
                Debug.Log("Clicked the button with text");
                i++;
            }
            if (GUI.Button(new Rect(10, 430, 900, 30), responseList[1]))
            {
                Debug.Log("Clicked the button with text");
                i++;
            }
            if (GUI.Button(new Rect(10, 460, 900, 30), responseList[2]))
            {
                Debug.Log("Clicked the button with text");
                i++;
            }
            if (i >= textList.Length)
            {
                dialougeActive = false;
                i = 0;
                OnEventNoDialouge();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Start");
            dialougeActive = true;
            OnEventDialougeSpeaker();
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Start");
            dialougeActive = false;
            OnEventNoDialouge();
        }
    }
    */
}
