using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Xml;
using DialogueTree;

public class DialogueSetScript : MonoBehaviour {

    Vector2 scroller;

    bool dialougeActive;
    GUIStyle guiStyle = new GUIStyle();
    string npcText;
    string[] responseList;
    int[] responseNode;
    int responseCount = 0;

    DialogueScript dia;
    int selectOpt = -2;
    public string DiaFile;

    // delegate signature
    public delegate void EventDialouge();

    // event instances for EventStartDialouge
    public static event EventDialouge OnEventDialougeStart;
    public static event EventDialouge OnEventNoDialouge;


    // Use this for initialization
    void Start () {
        dia = DialogueScript.LoadDialogue(DiaFile);
        RunDialogue();
    }
	
    public void RunDialogue()
    {
        StartCoroutine(runDialogue());
    }

    public void SetSelOpt(int i)
    {
        selectOpt = i;
        Debug.Log(selectOpt);
    }

    public IEnumerator runDialogue()
    {
        int nodeId = 0; 

        while (selectOpt != -1)
        {
            DisplayNode(dia.nodes[nodeId]);
            selectOpt = -2;
            while(selectOpt==-2)
            {
                yield return new WaitForSeconds(0.1f);
            }
            nodeId = selectOpt;
        }
    }

    void DisplayNode(DialogueNodeScript node)
    {
        npcText = node.Text;
        responseList = new string[node.options.Count];
        responseNode = new int[node.options.Count];
        responseCount = node.options.Count;

        for (int i = 0; i < node.options.Count; i++)
        {
            responseList[i] = node.options[i].Text;
            responseNode[i] = node.options[i].DestinationNodeID;
        }
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
        guiStyle.alignment = TextAnchor.MiddleCenter;
        int labelSize = Screen.width / 2;

        if (dialougeActive == true)
        {
            GUILayout.BeginArea(new Rect((Screen.width/2) - labelSize/2, (Screen.width/2) - labelSize/2, labelSize, labelSize));
            GUILayout.BeginVertical("Box");
            GUILayout.Label(npcText, guiStyle);
            GUILayout.EndVertical();
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(0, Screen.height/2, Screen.width, Screen.height/2));
            GUILayout.BeginVertical("Box", GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 2));
            scroller = GUILayout.BeginScrollView(scroller, false, true, GUILayout.Width(Screen.width), GUILayout.MinHeight(Screen.height / 2), GUILayout.MaxHeight(Screen.height), GUILayout.ExpandHeight(true));
            for (int i = 0; i < responseCount; i++)
            {
            if (GUILayout.Button(responseList[i], GUILayout.Width(Screen.width), GUILayout.Height((Screen.height / 4)/3)))
            { 
                    SetSelOpt(responseNode[i]);
                    if(selectOpt == -1)
                    {
                        dialougeActive = false;
                    }
            }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Start");
            dialougeActive = true;
            SetSelOpt(0);
            StartCoroutine(runDialogue());
            OnEventDialougeStart();
        }
    }

  
}
