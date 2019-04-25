using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

namespace DialogueTree
{
    public class DialogueNodeScript
    {
        public int NodeID = -1;
        public string Text;
        public List<DialogueOptionScript> options;

        public DialogueNodeScript()
        {
            options = new List<DialogueOptionScript>();
        }

        public DialogueNodeScript(string text)
        {
            Text = text;
            options = new List<DialogueOptionScript>();
        }

    }
}
