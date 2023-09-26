using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Model myModel = new Model();
        List<Vector4> verts = convertToHomg(myModel.vertices);

       // myModel.CreateUnityGameObject(); 
        Vector3 axis = (new Vector3(-2,1,1)).normalized;
        Matrix4x4 rotationMatrix = 
            Matrix4x4.TRS(Vector3.zero,
                        Quaternion.AngleAxis(-25,axis),
                        Vector3.one);

        displayMatrix(rotationMatrix);

        List<Vector4> imageAfterRotation = 
            applyTransformation(verts, rotationMatrix);

        Matrix4x4 translationMatrix = 
            Matrix4x4.TRS( new Vector3(0,-3,2),
            Quaternion.identity,
            Vector3.one );

        displayMatrix(translationMatrix);

        List<Vector4> imageAfterTranslation = 
            applyTransformation(imageAfterRotation, translationMatrix);


    }

    private List<Vector4> convertToHomg(List<Vector3> vertices)
    {
        List<Vector4> output = new List<Vector4>();

        foreach (Vector3 v in vertices)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));

        }
        return output;

    }

    private List<Vector4> applyTransformation
        (List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output    = new List<Vector4>();
        foreach (Vector4 v in verts) 
        { output.Add(tranformMatrix * v); }

        return output;

    }

    private void displayMatrix(Matrix4x4 rotationMatrix)
    {
        for (int i = 0; i < 4; i++)
            { print(rotationMatrix.GetRow( i)); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
