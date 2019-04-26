using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.UI;
using DialogueTree;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DialogueEdit
{
    public class DialogueEditorWindow : EditorWindow
    {
        Vector2 scroller;

        static DialogueEditorWindow window;
        public int setAmount = 0;
        bool dialougeEdit0, dialougeEdit1, dialougeEdit2, dialougeEdit3, dialougeEdit4, dialougeEdit5;
        public string npcDialougeSet0, npcDialougeSet1, npcDialougeSet2, npcDialougeSet3, npcDialougeSet4, npcDialougeSet5;
        public string[] responsesSet0, responsesSet1, responsesSet2, responsesSet3, responsesSet4, responsesSet5;
        public int[] id0, id1, id2, id3, id4, id5;
        public int[] currentID = { 0, 1, 2, 3, 4, 5 };
        string dialogueFile = "Dialogue_File.xml";

        [MenuItem("DialogueEdit/Editor")]
        static void Init()
        {
            window = (DialogueEditorWindow)EditorWindow.GetWindow(typeof(DialogueEditorWindow));
            window.Show();
        }

        private void OnGUI()
        {
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);

            GUILayout.BeginVertical("Box");
            scroller = GUILayout.BeginScrollView(scroller, false, true, GUILayout.Width(600), GUILayout.MinHeight(200), GUILayout.MaxHeight(1000), GUILayout.ExpandHeight(true));

            GUILayout.BeginHorizontal("Box");
            setAmount = EditorGUILayout.IntField("How many sets (0-5):", setAmount);
            setAmount = Mathf.Clamp(setAmount, 0, 5);
            GUILayout.EndHorizontal();

                if (setAmount >= 0)
                {
                    dialougeEdit0 = EditorGUILayout.Foldout(dialougeEdit0, "Dialogue Editor");
                    if (dialougeEdit0)
                    {
                        EditorGUILayout.LabelField("ID 0");
                        
                        npcDialougeSet0 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet0);
                        EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet0");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id0");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }       
                }
                if (setAmount >= 1)
                {
                    dialougeEdit1 = EditorGUILayout.Foldout(dialougeEdit1, "Dialogue Editor");
                    if (dialougeEdit1)
                    {
                        EditorGUILayout.LabelField("ID 1");

                        npcDialougeSet1 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet1);
                        EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet1");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id1");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }
                }
                if (setAmount >= 2)
                {
                    dialougeEdit2 = EditorGUILayout.Foldout(dialougeEdit2, "Dialogue Editor");
                    if (dialougeEdit2)
                    {
                        EditorGUILayout.LabelField("ID 2");

                        npcDialougeSet2 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet2);
                        EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet2");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id2");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }
                }
                if (setAmount >= 3)
                {
                    dialougeEdit3 = EditorGUILayout.Foldout(dialougeEdit3, "Dialogue Editor");
                    if (dialougeEdit3)
                    {
                        EditorGUILayout.LabelField("ID 3");

                        npcDialougeSet3 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet3);
                        EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet3");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id3");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }
                }
                if (setAmount >= 4)
                {
                    dialougeEdit4 = EditorGUILayout.Foldout(dialougeEdit4, "Dialogue Editor");
                    if (dialougeEdit4)
                    {
                        EditorGUILayout.LabelField("ID 4");

                    npcDialougeSet4 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet4);
                    EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet4");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id4");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }
                }
                if (setAmount >= 5)
                {
                    dialougeEdit5 = EditorGUILayout.Foldout(dialougeEdit5, "Dialogue Editor");
                    if (dialougeEdit5)
                    {
                        EditorGUILayout.LabelField("ID 5");

                    npcDialougeSet5 = EditorGUILayout.TextField("Type NPC Dialogue", npcDialougeSet5);
                    EditorGUILayout.LabelField("Set a number of responses");
                        SerializedProperty integerProperty = so.FindProperty("responsesSet5");
                        EditorGUILayout.PropertyField(integerProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties

                        EditorGUILayout.LabelField("Set ID for next NPC line, set element to -1 to end");
                        SerializedProperty idProperty = so.FindProperty("id5");
                        EditorGUILayout.PropertyField(idProperty, true); // True means show children
                        so.ApplyModifiedProperties(); // Remember to apply modified properties
                    }
                }
            
            GUILayout.BeginHorizontal("Box");
            dialogueFile = EditorGUILayout.TextField("File Name", dialogueFile);
            if (GUILayout.Button("Save"))
            {
                CreateDialogue(setAmount, dialogueFile, npcDialougeSet0, npcDialougeSet1, npcDialougeSet2, npcDialougeSet3, npcDialougeSet4, npcDialougeSet5,
                    responsesSet0, responsesSet1, responsesSet2, responsesSet3, responsesSet4, responsesSet5, id0, id1, id2, id3, id4, id5);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Load"))
            {
                LoadXML(dialogueFile);
            }
            GUILayout.EndHorizontal();

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

        }

        public static void CreateDialogue(int amount, string filename, string npc0, string npc1, string npc2, string npc3, string npc4, string npc5,
            string[] response0, string[] response1, string[] response2, string[] response3, string[] response4, string[] response5,
            int[] id0, int[] id1, int[] id2, int[] id3, int[] id4, int[] id5)
        {
            Debug.Log("Saved");

            DialogueScript dia = new DialogueScript();

            DialogueNodeScript node0 = new DialogueNodeScript(npc0);
            DialogueNodeScript node1 = new DialogueNodeScript(npc1);
            DialogueNodeScript node2 = new DialogueNodeScript(npc2);
            DialogueNodeScript node3 = new DialogueNodeScript(npc3);
            DialogueNodeScript node4 = new DialogueNodeScript(npc4);
            DialogueNodeScript node5 = new DialogueNodeScript(npc5);

            if (amount >= 0)
            {
                Debug.Log("Saved 0 " + npc0);
                dia.AddNode(node0);
                for (int i = 0; i < response0.Length; i++)
                {
                    Debug.Log(i + " " + id0[i] + " " + response0[i]);

                    if (id0[i] == 0)
                    {
                        dia.AddOption(response0[i], node0, node0);
                    }
                    else if (id0[i] == 1)
                    {
                        dia.AddOption(response0[i], node0, node1);
                    }
                    else if (id0[i] == 2)
                    {
                        dia.AddOption(response0[i], node0, node2);
                    }
                    else if (id0[i] == 3)
                    {
                        dia.AddOption(response0[i], node0, node3);
                    }
                    else if (id0[i] == 4)
                    {
                        dia.AddOption(response0[i], node0, node4);
                    }
                    else if (id0[i] == 5)
                    {
                        dia.AddOption(response0[i], node0, node5);
                    }
                    else if (id0[i] == -1)
                    {
                        dia.AddOption(response0[i], node0, null);
                    }
                }
            }
            if (amount >= 1)
            {
                Debug.Log("Saved 1 " + npc1);
                dia.AddNode(node1);
                for (int i = 0; i < response1.Length; i++)
                {
                    Debug.Log(i + " " + id1[i] + " " + response1[i]);
                    if (id1[i] == 0)
                    {
                        dia.AddOption(response1[i], node1, node0);
                    }
                    else if (id1[i] == 1)
                    {
                        dia.AddOption(response1[i], node1, node1);
                    }
                    else if (id1[i] == 2)
                    {
                        dia.AddOption(response1[i], node1, node2);
                    }
                    else if (id1[i] == 3)
                    {
                        dia.AddOption(response1[i], node1, node3);
                    }
                    else if (id1[i] == 4)
                    {
                        dia.AddOption(response1[i], node1, node4);
                    }
                    else if (id1[i] == 5)
                    {
                        dia.AddOption(response1[i], node1, node5);
                    }
                    else if (id1[i] == -1)
                    {
                        dia.AddOption(response1[i], node1, null);
                    }
                }
            }
            if (amount >= 2)
            {
                Debug.Log("Saved 2");
                dia.AddNode(node2);
                for (int i = 0; i < response2.Length; i++)
                {
                    Debug.Log(i + " " + id2[i] + " " + response2[i]);
                    if (id2[i] == 0)
                    {
                        dia.AddOption(response2[i], node2, node0);
                    }
                    else if (id2[i] == 1)
                    {
                        dia.AddOption(response2[i], node2, node1);
                    }
                    else if (id2[i] == 2)
                    {
                        dia.AddOption(response2[i], node2, node2);
                    }
                    else if (id2[i] == 3)
                    {
                        dia.AddOption(response2[i], node2, node3);
                    }
                    else if (id2[i] == 4)
                    {
                        dia.AddOption(response2[i], node2, node4);
                    }
                    else if (id2[i] == 5)
                    {
                        dia.AddOption(response2[i], node2, node5);
                    }
                    else if (id2[i] == -1)
                    {
                        dia.AddOption(response2[i], node2, null);
                    }
                }
            }
            if (amount >= 3)
            {
                Debug.Log("Saved 3");
                dia.AddNode(node3);
                for (int i = 0; i < response3.Length; i++)
                {
                    Debug.Log(i + " " + id3[i]);
                    if (id3[i] == 0)
                    {
                        dia.AddOption(response3[i], node3, node0);
                    }
                    else if (id3[i] == 1)
                    {
                        dia.AddOption(response3[i], node3, node1);
                    }
                    else if (id3[i] == 2)
                    {
                        dia.AddOption(response3[i], node3, node2);
                    }
                    else if (id3[i] == 3)
                    {
                        dia.AddOption(response3[i], node3, node3);
                    }
                    else if (id3[i] == 4)
                    {
                        dia.AddOption(response3[i], node3, node4);
                    }
                    else if (id3[i] == 5)
                    {
                        dia.AddOption(response3[i], node3, node5);
                    }
                    else if (id3[i] == -1)
                    {
                        dia.AddOption(response3[i], node3, null);
                    }
                }
            }
            if (amount >= 4)
            {
                Debug.Log("Saved 4");
                
                dia.AddNode(node4);
                node4.NodeID = 4;
                for (int i = 0; i < response4.Length; i++)
                {
                    Debug.Log(i + " " + id4[i] + " " + node4.NodeID);
                    if (id4[i] == 0)
                    {
                        dia.AddOption(response4[i], node4, node0);
                    }
                    else if (id4[i] == 1)
                    {
                        dia.AddOption(response4[i], node4, node1);
                    }
                    else if (id4[i] == 2)
                    {
                        dia.AddOption(response4[i], node4, node2);
                    }
                    else if (id4[i] == 3)
                    {
                        dia.AddOption(response4[i], node4, node3);
                    }
                    else if (id4[i] == 4)
                    {
                        dia.AddOption(response4[i], node4, node4);
                    }
                    else if (id4[i] == 5)
                    {
                        dia.AddOption(response4[i], node4, node5);
                    }
                    else if (id4[i] == -1)
                    {
                        dia.AddOption(response4[i], node4, null);
                    }
                }
            }
            if (amount >= 5)
            {
                Debug.Log("Saved 5");
                
                dia.AddNode(node5);
                node5.NodeID = 5;
                for (int i = 0; i < response5.Length; i++)
                {
                    Debug.Log(i + " " + id5[i] + " " + response5[i]);
                    if (id5[i] == 0)
                    {
                        dia.AddOption(response5[i], node5, node0);
                    }
                    else if (id5[i] == 1)
                    {
                        dia.AddOption(response5[i], node5, node1);
                    }
                    else if (id5[i] == 2)
                    {
                        dia.AddOption(response5[i], node5, node2);
                    }
                    else if (id5[i] == 3)
                    {
                        dia.AddOption(response5[i], node5, node3);
                    }
                    else if (id5[i] == 4)
                    {
                        dia.AddOption(response5[i], node5, node4);
                    }
                    else if (id5[i] == 5)
                    {
                        dia.AddOption(response5[i], node5, node5);
                    }
                    else if (id5[i] == -1)
                    {
                        dia.AddOption(response5[i], node5, null);
                    }
                }
            }

            XmlSerializer serz = new XmlSerializer(typeof(DialogueScript));
            StreamWriter writer = new StreamWriter(filename);

            serz.Serialize(writer, dia);
        }

        public void LoadXML(string filename)
        {
            Debug.Log("Loaded");
            DialogueScript dia = LoadDialogue(filename);

            if (dia.nodes[0].Text != null && dia.nodes[0].options.Count != 0)
            {
                Debug.Log("A");
                npcDialougeSet0 = dia.nodes[0].Text;
                responsesSet0 = new string[dia.nodes[0].options.Count];
                id0 = new int[dia.nodes[0].options.Count];
                setAmount = 0;
                Debug.Log("B");
                for (int i = 0; i < dia.nodes[0].options.Count; i++)
                {
                    responsesSet0[i] = dia.nodes[0].options[i].Text;
                    id0[i] = dia.nodes[0].options[i].DestinationNodeID;
                }
                Debug.Log("C");
            }
            if (dia.nodes[1].Text != null && dia.nodes[1].options.Count != 0)
            {
                npcDialougeSet1 = dia.nodes[1].Text;
                responsesSet1 = new string[dia.nodes[1].options.Count];
                id1 = new int[dia.nodes[1].options.Count];
                setAmount = 1;
                for (int i = 0; i < dia.nodes[1].options.Count; i++)
                {
                    responsesSet1[i] = dia.nodes[1].options[i].Text;
                    id1[i] = dia.nodes[1].options[i].DestinationNodeID;
                }
            }
            if (dia.nodes[2].Text != null && dia.nodes[2].options.Count != 0)
            {
                npcDialougeSet2 = dia.nodes[2].Text;
                responsesSet2 = new string[dia.nodes[2].options.Count];
                id2 = new int[dia.nodes[2].options.Count];
                setAmount = 2;
                for (int i = 0; i < dia.nodes[2].options.Count; i++)
                {
                    responsesSet2[i] = dia.nodes[2].options[i].Text;
                    id2[i] = dia.nodes[2].options[i].DestinationNodeID;
                }
            }
            if (dia.nodes[3].Text != null && dia.nodes[3].options.Count != 0)
            {
                npcDialougeSet3 = dia.nodes[3].Text;
                responsesSet3 = new string[dia.nodes[3].options.Count];
                id3 = new int[dia.nodes[3].options.Count];
                setAmount = 3;
                for (int i = 0; i < dia.nodes[3].options.Count; i++)
                {
                    responsesSet3[i] = dia.nodes[3].options[i].Text;
                    id3[i] = dia.nodes[3].options[i].DestinationNodeID;
                }
            }
            if (dia.nodes[4].Text != null && dia.nodes[4].options.Count != 0)
            {
                npcDialougeSet4 = dia.nodes[4].Text;
                responsesSet4 = new string[dia.nodes[4].options.Count];
                id4 = new int[dia.nodes[4].options.Count];
                setAmount = 4;
                for (int i = 0; i < dia.nodes[4].options.Count; i++)
                {
                    responsesSet4[i] = dia.nodes[4].options[i].Text;
                    id4[i] = dia.nodes[4].options[i].DestinationNodeID;
                }
            }
            if (dia.nodes[5].Text != null && dia.nodes[5].options.Count != 0)
            {
                npcDialougeSet5 = dia.nodes[5].Text;
                responsesSet5 = new string[dia.nodes[5].options.Count];
                id5 = new int[dia.nodes[5].options.Count];
                setAmount = 5;
                for (int i = 0; i < dia.nodes[5].options.Count; i++)
                {
                    responsesSet5[i] = dia.nodes[5].options[i].Text;
                    id5[i] = dia.nodes[5].options[i].DestinationNodeID;
                }
            }

        }

        public static DialogueScript LoadDialogue(string path)
        {
            XmlSerializer serz = new XmlSerializer(typeof(DialogueScript));
            StreamReader reader = new StreamReader(path);

            DialogueScript dia = (DialogueScript)serz.Deserialize(reader);

            return dia;
        }
    }
}