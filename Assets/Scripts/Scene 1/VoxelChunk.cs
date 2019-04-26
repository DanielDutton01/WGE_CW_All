using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoxelChunk : MonoBehaviour {

    VoxelGenerator voxelGenerator;
    public int[,,] terrainArray;
    public GameObject player;
    public GameObject playerCamera;
    Vector3 playerPos;
    Quaternion playerRot;
    public GameObject[] droppableBlock;


    public int chunkSize = 16;

    public InputField fileNameInput;
    string saveFileText;

    // delegate signature
    public delegate void EventBlockChangedWithType(int blockType);

    // event instances for EventBlockChanged
    public static event EventBlockChangedWithType OnEventBlockChanged;

    // When game object is enabled
    void OnEnable()
    {
        PlayerScript.OnEventBlockUse += SetBlock;

    }

    // When game object is disabled
    void OnDisable()
    {
        PlayerScript.OnEventBlockUse -= SetBlock;
    }

    public void clearObject()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.Clear();
    }

    public void Start()
    {
        InitialiseChunk();
    }

    public void LoadChunk(string filename)
    {
        voxelGenerator = GetComponent<VoxelGenerator>();
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, filename);
        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();
        voxelGenerator.UpdateMesh();
    }

    public void InitialiseChunk()
    {
        voxelGenerator = GetComponent<VoxelGenerator>();
        // Instantiate the array with size based on chunksize
        terrainArray = new int[chunkSize, chunkSize, chunkSize];

        voxelGenerator.Initialise();
        InitialiseTerrain();
        //CreatePathway();
        CreateTerrain();
        voxelGenerator.UpdateMesh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            saveFileText = fileNameInput.text;
            Saving();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            saveFileText = fileNameInput.text;
            Loading();
        }
    }

    public void Saving()
    {
        playerPos = player.transform.position;
        playerRot = playerCamera.transform.rotation;

        XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, saveFileText);
        XMLVoxelFileWriter.SavePlayerToXMLFile(playerPos, playerRot, "PlayerPosition");
    }

    public void LoadingMain()
    {
        // Get terrainArray from XML file
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, saveFileText);
        playerPos = XMLVoxelFileWriter.LoadPlayerPosFromXMLFile("PlayerPosition");
        playerRot = XMLVoxelFileWriter.LoadPlayerRotFromXMLFile("PlayerPosition");
        Loading();
    }

    public void LoadingA1()
    {
        // Get terrainArray from XML file
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, "AssessmentChunk1");
        playerPos = XMLVoxelFileWriter.LoadPlayerPosFromXMLFile("PlayerPosition");
        playerRot = XMLVoxelFileWriter.LoadPlayerRotFromXMLFile("PlayerPosition");
        Loading();
    }

    public void LoadingA2()
    {
        // Get terrainArray from XML file
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, "AssessmentChunk2");
        playerPos = XMLVoxelFileWriter.LoadPlayerPosFromXMLFile("PlayerPosition");
        playerRot = XMLVoxelFileWriter.LoadPlayerRotFromXMLFile("PlayerPosition");
        Loading();
    }

    public void Loading()
    {
        // Draw the correct faces
        CreateTerrain();
        // Update mesh info
        voxelGenerator.UpdateMesh();

        player.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        playerCamera.transform.rotation = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);
        player.transform.rotation = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);

        player.SetActive(false);
        player.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        playerCamera.transform.rotation = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);
        player.transform.rotation = Quaternion.Euler(playerRot.x, playerRot.y, playerRot.z);
        player.SetActive(true);
    }



    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x > 0 && index.x < terrainArray.GetLength(0)) &&
        (index.y > 0 && index.y < terrainArray.GetLength(1)) &&
        (index.z > 0 && index.z < terrainArray.GetLength(2)))
        {
            //gets the block type that is changed
            int prevBlockType = terrainArray[(int)index.x, (int)index.y, (int)index.z];
            // Change the block to the required type
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;
            // Create the new mesh
            CreateTerrain();
            // Update the mesh data
            GetComponent<VoxelGenerator>().UpdateMesh();

            OnEventBlockChanged(blockType);

            //if the block is destroyed use the location and type it WAS to gen a new block
            if (blockType == 0)
                CreateDropBlock(index, prevBlockType);
        }

    }

    public void CreateDropBlock(Vector3 index, int blockType)
    {
        if (blockType == 1)
            Instantiate(droppableBlock[0], new Vector3(index.x + 0.5f, index.y + 0.5f, index.z + 0.5f), Quaternion.identity);
        if (blockType == 2)
            Instantiate(droppableBlock[1], new Vector3(index.x + 0.5f, index.y + 0.5f, index.z + 0.5f), Quaternion.identity);
        if (blockType == 3)
            Instantiate(droppableBlock[2], new Vector3(index.x + 0.5f, index.y + 0.5f, index.z + 0.5f), Quaternion.identity);
        if (blockType == 4)
            Instantiate(droppableBlock[3], new Vector3(index.x + 0.5f, index.y + 0.5f, index.z + 0.5f), Quaternion.identity);
    }

    public void InitialiseTerrain()
    {
        // iterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // iterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // iterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2);
                z++)
                {
                    // if we are operating on 4th layer
                    if (y == 8)
                    {
                        terrainArray[x, y, z] = 1;
                    }
                    //else if the the layer is below the fourth
                    else if (y < 8 && y >= 4)
                    {
                        terrainArray[x, y, z] = 2;
                    }
                    else if (y < 4)
                    {
                        terrainArray[x, y, z] = 4;
                    }
                }
            }
        }
    }


    public void CreateTerrain()
    {
        // iterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // iterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // iterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2);
                z++)
                {
                    // if this voxel is not empty
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;
                        // set texture name by value
                        switch (terrainArray[x, y, z])
                        {
                            case 1:
                                tex = "Grass";
                                break;
                            case 2:
                                tex = "Dirt";
                                break;
                            case 3:
                                tex = "Sand";
                                break;
                            case 4:
                                tex = "Stone";
                                break;
                            default:
                                tex = "Grass";
                                break;
                        }
                        // check if we need to draw the negative x face
                        if (x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive x face
                        if (x == terrainArray.GetLength(0) - 1 ||
                        terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }
                        // check if we need to draw the negative y face
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive y face
                        if (y == terrainArray.GetLength(1) - 1 ||
                        terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);

                        }
                        // check if we need to draw the negative z face
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive z face
                        if (z == terrainArray.GetLength(2) - 1 ||
                        terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                    }
                }
            }
        }
    }


}
