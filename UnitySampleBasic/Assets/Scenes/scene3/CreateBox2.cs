using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CreateBox2 : MonoBehaviour
{

    void Start(){
        test01();
    }


    void test01()
        {
            var mesh = new Mesh();

        // 頂点リストを作成
        mesh.SetVertices(new Vector3[] {


              new Vector3( 0.0f, 0.0f, 0.0f),
              new Vector3( 0.0f, 0.0f, 1.0f),
              new Vector3( 1.0f, 0.0f, 1.0f),
              new Vector3( 1.0f, 0.0f, 0.0f),

              new Vector3( 0.0f, 1.0f, 0.0f),
              new Vector3( 0.0f, 1.0f, 1.0f),
              new Vector3( 1.0f, 1.0f, 1.0f),
              new Vector3( 1.0f, 1.0f, 0.0f),
            });
        mesh.SetColors(new Color[] {
            Color.red,
            Color.red,
            Color.green,
            Color.green,
            Color.blue,
            Color.blue,
            Color.cyan,
            Color.cyan,
            });

     //   mesh.SetTriangles(new int[] { 0, 1, 3, 3, 1, 2 }, 0);
        mesh.SetTriangles(new int[] {
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
             2,7,6}, 0 );




        var mr = GetComponent<MeshRenderer>();

        //コードから触る時はMeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        // 変更箇所 : 参照するシェーダー名  
        renderer.material = new Material(Shader.Find("Unlit/VertexColorShader"));

    }


}
