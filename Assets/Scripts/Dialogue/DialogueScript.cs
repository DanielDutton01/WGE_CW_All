using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DialogueTree
{
    public class DialogueScript
    {
        public List<DialogueNodeScript> nodes;

        public void AddNode(DialogueNodeScript node)
        {
            if (node == null)
                return;

            nodes.Add(node);
            node.NodeID = nodes.IndexOf(node);
        }

        public void AddOption(string text, DialogueNodeScript node, DialogueNodeScript dest)
        {
            if (!nodes.Contains(dest))
                AddNode(dest);

            if (!nodes.Contains(node))
                AddNode(node);

            DialogueOptionScript opt;

            if(dest == null)
            {
                opt = new DialogueOptionScript(text, -1);
            }
            else
            {
                opt = new DialogueOptionScript(text, dest.NodeID);
            }

            node.options.Add(opt);
        }

        public DialogueScript()
        {
            nodes = new List<DialogueNodeScript>();
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