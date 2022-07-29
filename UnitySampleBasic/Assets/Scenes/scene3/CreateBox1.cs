using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBox1
: MonoBehaviour
{
   // [SerializeField] Material m_mat;

    void Start()
    {
        test1();
    //    test2();

    }
    void test1()
    {
        Mesh mesh = new Mesh();

        var mr = GetComponent<MeshRenderer>();
        //var mat = mr.GetComponent<Material>();
        //mr.material = new Material(Shader.Find("Unlit/VertexColorShader"));


        // 頂点リストを作成
        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3( 0.0f, 0.0f, 0.0f),
            new Vector3( 0.0f, 0.0f, 1.0f),
            new Vector3( 1.0f, 0.0f, 1.0f),
            new Vector3( 1.0f, 0.0f, 0.0f),

            new Vector3( 0.0f, 1.0f, 0.0f),
            new Vector3( 0.0f, 1.0f, 1.0f),
            new Vector3( 1.0f, 1.0f, 1.0f),
            new Vector3( 1.0f, 1.0f, 0.0f),

        };

        List<int> indexes = new List<int> {
            
            1,0, 2,
            2,0,3,

            //上面
            4,5,7,
            7,5,6,

            // 手前：　XY平面と接する
           0,4,3,
           3,4,7,
           　
           // 奥　XY平面　ｚ＝１
           2,6,1,
           1,6,5,


           // YZ平面と接する
           1,5,0,
           0,5,4,

           // YZ平面 X = 1
           3,7,2,
           2,7,6,
        };
        //mesh.SetColors(colors.ToArray());
        mesh.SetVertices(vertices);
        mesh.SetIndices(indexes, MeshTopology.Triangles,0);

        //コードから触る時はMeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

    }
    /*
    void test2()
    {
        // メッシュを作成
        Mesh mesh = new Mesh();

        float XXX = 3.0f;
        // 頂点リストを作成
        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(XXX+1.0f, 1.0f, 1.0f),
            new Vector3(XXX+-1.0f, 1.0f, 1.0f),
            new Vector3(XXX+-1.0f, -1.0f, 1.0f),
            new Vector3(XXX+1.0f, -1.0f, 1.0f),
            new Vector3(XXX+1.0f, -1.0f, -1.0f),
            new Vector3(XXX+-1.0f, -1.0f, -1.0f),
            new Vector3(XXX+-1.0f, 1.0f, -1.0f),
            new Vector3(XXX+1.0f, 1.0f, -1.0f),
        };

        // 面を構成するインデックスリストを作成
        List<int> triangles = new List<int> {
            0, 1, 2,  // 奥面
            0, 2, 3,  // 奥面
            4, 5, 6,  // 前面
            4, 6, 7,  // 前面
            0, 4, 7,  // 右面
            0, 3, 4,  // 右面
            6, 2, 1,  // 左面
            6, 5, 2,  // 左面
            6, 1, 0,  // 上面
            7, 6, 0,  // 上面
            4, 3, 2,  // 下面
            5, 4, 2,  // 下面
        };


        // メッシュに頂点を登録する
        mesh.SetVertices(vertices);

        // メッシュに面を構成するインデックスリストを登録する
        mesh.SetTriangles(triangles, 0);


        // 作成したメッシュをメッシュフィルターに設定する
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        var mr = GetComponent<MeshRenderer>();
      //  mr.material = m_mat;

    }
    */
    private void Update()
    {

    }
}

