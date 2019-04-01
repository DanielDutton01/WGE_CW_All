using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLVoxelFileWriter
{

    // Write a voxel chunk to XML file
    public static void SaveChunkToXMLFile(int[,,] voxelArray, string fileName)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        // Create a write instance
        XmlWriter xmlWriter =
        XmlWriter.Create(fileName + ".xml", writerSettings);
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("VoxelChunk");


        // iterate through all array elements
        for (int x = 0; x < voxelArray.GetLength(0); x++)
        {
            for (int y = 0; y < voxelArray.GetLength(1); y++)
            {
                for (int z = 0; z < voxelArray.GetLength(1); z++)
                {
                    if (voxelArray[x, y, z] != 0)
                    {
                        // Create a single voxel element
                        xmlWriter.WriteStartElement("Voxel");
                        // Write an attribute to store the x index
                        xmlWriter.WriteAttributeString("x", x.ToString());
                        // Write an attribute to store the x index
                        xmlWriter.WriteAttributeString("y", y.ToString());
                        // Write an attribute to store the x index
                        xmlWriter.WriteAttributeString("z", z.ToString());
                        // Store the voxel type
                        xmlWriter.WriteString(voxelArray[x, y, z].ToString());
                        // End the voxel element
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        // End the root element
        xmlWriter.WriteEndElement();

        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();

    }


    public static void SavePlayerToXMLFile(Vector3 playerPos, Quaternion playerRot, string fileName)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
        // Create a write instance
        XmlWriter xmlWriter =
        XmlWriter.Create(fileName + ".xml", writerSettings);
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        // Create the root element
        xmlWriter.WriteStartElement("Player");

        
            // Create a single player element
            xmlWriter.WriteStartElement("Position");
            // Write an attribute to store the x index
            xmlWriter.WriteAttributeString("x", playerPos.x.ToString());
            // Write an attribute to store the y index
            xmlWriter.WriteAttributeString("y", playerPos.y.ToString());
            // Write an attribute to store the z index
            xmlWriter.WriteAttributeString("z", playerPos.z.ToString());
        // End the position element
        xmlWriter.WriteEndElement();
        
        // Create a single player element
        xmlWriter.WriteStartElement("Rotation");
        // Write an attribute to store the x rotation index
        xmlWriter.WriteAttributeString("x", playerRot.x.ToString());
            // Write an attribute to store the y rotation index
            xmlWriter.WriteAttributeString("y", playerRot.y.ToString());
            // Write an attribute to store the z rotation index
            xmlWriter.WriteAttributeString("z", playerRot.z.ToString());

            // End the rotation element
            xmlWriter.WriteEndElement();
        

        // End the root element
        xmlWriter.WriteEndElement();

        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();
    }

    public static Vector3 LoadPlayerPosFromXMLFile(string fileName)
    {
        Vector3 playerPos = new Vector3(5, 5, 5);
        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            // Check if this node is a player element
            if (xmlReader.IsStartElement("Position"))
            {
                // Retrieve x attribute and store as int
                float x = float.Parse(xmlReader["x"]);
                // Retrieve x attribute and store as int
                float y = float.Parse(xmlReader["y"]);
                // Retrieve x attribute and store as int
                float z = float.Parse(xmlReader["z"]);

                xmlReader.Read();
                playerPos = new Vector3(x, y, z);
            }
        }
        return playerPos;
    }
    
    public static Quaternion LoadPlayerRotFromXMLFile(string fileName)
    {
        Quaternion playerRot = new Quaternion(0, 0, 0, 0);
        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            // Check if this node is a player element
            if (xmlReader.IsStartElement("Rotation"))
            {
                // Retrieve x attribute and store as int
                float xR = float.Parse(xmlReader["x"]);
                // Retrieve x attribute and store as int
                float yR = float.Parse(xmlReader["y"]);
                // Retrieve x attribute and store as int
                float zR = float.Parse(xmlReader["z"]);

                xmlReader.Read();

                playerRot = new Quaternion(xR, yR, zR, 0);
            }
        }
        return playerRot;
    }
    
    // Read a voxel chunk from XML file
    public static int[,,] LoadChunkFromXMLFile(int size, string fileName)
    {
        int[,,] voxelArray = new int[size, size, size];

        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileName + ".xml");
        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            // Check if this node is a Voxel element
            if (xmlReader.IsStartElement("Voxel"))
            {
                // Retrieve x attribute and store as int
                int x = int.Parse(xmlReader["x"]);
                // Retrieve x attribute and store as int
                int y = int.Parse(xmlReader["y"]);
                // Retrieve x attribute and store as int
                int z = int.Parse(xmlReader["z"]);
                xmlReader.Read();
                int value = int.Parse(xmlReader.Value);
                voxelArray[x, y, z] = value;

            }
        }

        return voxelArray;
    }

}
