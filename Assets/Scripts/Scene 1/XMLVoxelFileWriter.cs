using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XMLVoxelFileWriter
{    
    // Read a voxel chunk from XML file
    public static int[,,] LoadChunkFromXMLFile(int size, string fileName)
    {
        int[,,] voxelArray = new int[size, size, size];

        // Create an XML reader with the file supplied
        XmlReader xmlReader;
        if (!File.Exists(fileName + ".xml"))
        {
             xmlReader = XmlReader.Create("AssessmentChunk1.xml");
            Debug.Log("File Unavailable, setting default Assesment Chunk");
        }
        else
        {
             xmlReader = XmlReader.Create(fileName + ".xml");
            Debug.Log("File Available, setting file");
        }
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
