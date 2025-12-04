To find the colour of the appropriate pixel in a texture given a pixel in the model

        Vector3Int face = myModel.faces[i];
        Vector3Int texture = myModel.texture_index_list[i];

        a_t = myModel.texture_coordinates[texture.x];
        b_t = myModel.texture_coordinates[texture.y];
        c_t = myModel.texture_coordinates[texture.z];

        Vector3 a = verts[face.x]; 
        Vector3 b = verts[face.y];  
        Vector3 c = verts[face.z]; 

        a2 = pixelize(Project(a));
        b2 = pixelize(Project(b));
        c2 = pixelize(Project(c));


        A = b2 - a2;
        B = c2 - a2;

        A_t = b_t - a_t;
        B_t = c_t - a_t;
       
        float x = x_p - a2.x;
        float y = y_p - a2.y;

        float r = (x * B.y - y * B.x) / (A.x * B.y - A.y * B.x);
        float s = (A.x * y - x * A.y) / (A.x * B.y - A.y * B.x);

        Vector2 texture_point = a_t+r*A_t+s*B_t;
        texture_point *= 512;

        Color color = texture_file.GetPixel((int)texture_point.x, (int)texture_point.y);

       

