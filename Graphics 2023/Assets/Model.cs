using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{

    internal List<Vector3Int> faces;
    List<Vector3Int> texture_index_list;
    internal List<Vector3> vertices;
    List<Vector2> texture_coordinates;
    List<Vector3> normals; 
    


    public Model()
    {
        vertices = new List<Vector3>();
        addvertices();
        faces = new List<Vector3Int>();
        addfaces();

    }

    private void addfaces()
    {
        faces.Add(new Vector3Int(0,2,1));
        faces.Add(new Vector3Int(0, 3, 2));
        faces.Add(new Vector3Int(0, 4, 3));
        faces.Add(new Vector3Int(0, 1, 4));

        faces.Add(new Vector3Int(1, 2, 3));
        faces.Add(new Vector3Int(1,3,4));
        

    }

    private void addvertices()
    {
        vertices.Add(new Vector3(0,1,0)); // 0

        vertices.Add(new Vector3(-1, 0, -1)); // 1

        vertices.Add(new Vector3(-1, 0, 1)); // 2

        vertices.Add(new Vector3(1, 0, 1)); // 3

        vertices.Add(new Vector3(1, 0, -1)); // 4





    }

    public GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/

        for (int i = 0; i < faces.Count; i++)
        {
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }


}
