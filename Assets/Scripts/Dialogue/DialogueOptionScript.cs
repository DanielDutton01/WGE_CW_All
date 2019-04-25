using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

namespace DialogueTree
{
    public class DialogueOptionScript
    {
        public string Text;
        public int DestinationNodeID;

        public DialogueOptionScript()
        {

        }

        public DialogueOptionScript(string text, int dest)
        {
            this.Text = text;
            this.DestinationNodeID = dest;
        }
    }
}