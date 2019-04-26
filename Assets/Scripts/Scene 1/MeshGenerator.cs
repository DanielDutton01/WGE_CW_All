using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]

public class MeshGenerator : MonoBehaviour {

    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;
    int numQuads = 0;

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start () {

        // RequireComponent declaration earlier should guarantee a MeshFilter component
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();
        for(int i = 0; i<11;i++)
        {
            CreateQuad(i, 3, new Vector2(0, 0.5f));
            CreateQuad(i, 4, new Vector2(0, 0.5f));
            CreateQuad(i, 0, new Vector2(0.5f, 0));
            CreateQuad(i, 1, new Vector2(0.5f, 0));
            CreateQuad(i, 2, new Vector2(0.5f, 0));

            CreateGroundQuad(0, -i - 1,new Vector2(0, 0));
            CreateGroundQuad(1, -i - 1, new Vector2(0.5f, 0.5f));
            CreateGroundQuad(2, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(3, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(4, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(5, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(6, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(7, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(8, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(9, -i - 1, new Vector2(0, 0));
            CreateGroundQuad(10, -i - 1, new Vector2(0, 0));
        }



        // Convert lists to arrays and store in mesh
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triIndexList.ToArray();
        // Convert UV list to array and store in mesh
        mesh.uv = UVList.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("HI");
            }
        }
    }



    void CreateQuad(int x, int y, Vector2 uvCoords)
    {
        // Add vertices to the mesh vertex list
        vertexList.Add(new Vector3(x, y + 1, 0));
        vertexList.Add(new Vector3(x + 1, y + 1, 0));
        vertexList.Add(new Vector3(x + 1, y, 0));
        vertexList.Add(new Vector3(x, y, 0));
        // Add indices to the mesh tri index list
        triIndexList.Add(0);
        triIndexList.Add(1);
        triIndexList.Add(3);
        triIndexList.Add(1);
        triIndexList.Add(2);
        triIndexList.Add(3);

        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));

        // Add vertex indices to the mesh tri index list
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;
    }


    void CreateGroundQuad(int x, int z, Vector2 uvCoords)
    {
        // Add vertices to the mesh vertex list
        vertexList.Add(new Vector3(x, 0, z+1));
        vertexList.Add(new Vector3(x + 1, 0, z+1));
        vertexList.Add(new Vector3(x + 1, 0, z));
        vertexList.Add(new Vector3(x, 0, z));
        // Add indices to the mesh tri index list
        triIndexList.Add(0);
        triIndexList.Add(1);
        triIndexList.Add(3);
        triIndexList.Add(1);
        triIndexList.Add(2);
        triIndexList.Add(3);

        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));

        // Add vertex indices to the mesh tri index list
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;




    }


}
