using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    StreamWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        writer = new StreamWriter("Data.txt", false);

        Model myModel = new Model();

        List<Vector3> verts3 = myModel.vertices;
        List<Vector4> verts = convertToHomg(verts3);
        writeVectorsToFile(verts, " Vertices of my letter ", " ---------- ");



        Vector3 axis = (new Vector3(-2, 1, 1)).normalized;
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero,
                        Quaternion.AngleAxis(-25, axis),
                        Vector3.one);

        writeMatrixToFile(rotationMatrix, "Rotation Matrix", "  ---------  ");

        List<Vector4> imageAfterRotation =
            applyTransformation(verts, rotationMatrix);

        writeVectorsToFile(imageAfterRotation, " Verts after Rotation ", " -----------------");

        Matrix4x4 scaleMatrix =
            Matrix4x4.TRS(Vector3.zero,
            Quaternion.identity,
            new Vector3(2, 2, 1));

        writeMatrixToFile(scaleMatrix, " Scale Matrix ", " ----------------  ");

        List<Vector4> imageAfterScale =
            applyTransformation(imageAfterRotation, scaleMatrix);

        writeVectorsToFile(imageAfterScale, " After Scale (and Rotation)", " ---------------");
        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(new Vector3(), new Vector3(), new Vector3());
        Matrix4x4 projection = Matrix4x4.Perspective(90, 1, 1, 1000);
        writer.Close();



        Vector2 s = new Vector2(2,4);
        Vector2 e = new Vector2(3,-3);
        print(Intercept(s, e, 0));

        if (LineClip(ref s, ref e))
        {
            print(s);
            print(e);
        }
        else
        { print("Line rejected"); }



    }

    private void writeMatrixToFile(Matrix4x4 matrix, string before, string after)
    {
        writer.WriteLine(before);

        for (int i = 0; i < 4; i++)
        {
            Vector4 v = matrix.GetRow(i);
            writer.WriteLine(" ( " + v.x + " , " + v.y + " , " + v.z + " , " + v.w + " ) ");
        }
        writer.WriteLine(after);
    }

    private void writeVectorsToFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);

        foreach (Vector4 v in verts)
        {
            writer.WriteLine(" ( " + v.x + " , " + v.y + " , " + v.z + " , " + v.w + " ) ");
        }
        writer.WriteLine(after);
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
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;

    }

    private void displayMatrix(Matrix4x4 rotationMatrix)
    {
        for (int i = 0; i < 4; i++)
        { print(rotationMatrix.GetRow(i)); }
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool LineClip(ref Vector2 start, ref Vector2 end)
    { 
        /*
        OutCode startOC = new Outcode(start);


        if (startOC == inViewPort)
        {
            return LineClip(ref end, ref start);
        }
        if (startOC.up)
        {
           start =  Intercept(start, end, 0);
            return LineClip(ref start, ref end);
        }

        if (startOC.down)
        {
            start = Intercept(start, end, 1);
            return LineClip(ref start, ref end);
        }
        if (startOC.left)
        {
            start = Intercept(start, end, 2);
            return LineClip(ref start, ref end);

        }
        if (startOC.right)
        {
            start = Intercept(start, end, 3);
            return LineClip(ref start, ref end);
        } */
        return false;
    }
    Vector2 Intercept(Vector2 start, Vector2 end, int edgeIndex)
    {
        if (end.x != start.x)
        {
            float m = (end.y - start.y) / (end.x - start.x);

            switch (edgeIndex)
            {
                case 0:  // Top Edge  y = 1 whats x?
                    //  x = x1 + (1/m) ( y - y1)
                    if (m != 0)
                    {
                        return new Vector2(start.x + (1 / m) * (1 - start.y), 1);
                    }
                    else
                    {
                        // This should never happen if called on Outcode advice
                        if (start.y == 1)
                            return start;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }

                case 1:  // Bottom Edge y = -1 whats x?

                    if (m != 0)
                    {
                        return new Vector2(start.x + (1 / m) * (-1 - start.y), -1);
                    }
                    else
                    {
                        // This should never happen if called on Outcode advice
                        if (start.y == -1)
                            return start;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }
                case 2:  // Left Edge x = -1 what y?
                         //    y = y1 + m(x - x1)


                    return new Vector2(1, start.y + m * (-1 - start.x));

                default: // Right Egde x = 1 whats y?
                         //    y = y1 + m(x - x1)


                    return new Vector2(1, start.y + m * (1 - start.x));

            }


        }
        else
        {
            // m = infinity i.e. a vertical line

            switch (edgeIndex)
            {
                case 0:
                    // Top Edge 
                    return new Vector2(start.x, 1);

                case 1:
                    // Bottom Edge
                    return new Vector2(start.x, -1);

                case 2:
                    // Left Edge  (This cannot occur if called on Outcode advice)
                    if (start.x == -1)
                        return start;

                    return new Vector2(float.NaN, float.NaN);
                default:
                    // Right Edge  (This cannot occur if called on Outcode advice)
                    if (start.x == 1)
                        return start;

                    return new Vector2(float.NaN, float.NaN);


            }


        }

    }


}
