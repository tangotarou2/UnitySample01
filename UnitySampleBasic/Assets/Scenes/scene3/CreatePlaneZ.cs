using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CreatePlaneZ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        test01();
    }
    void test01()
    {
        var mesh = new Mesh();

        // 頂点リストを作成
        mesh.SetVertices(new Vector3[] {
            new Vector3( -1.0f, -1.0f, 0.0f),
            new Vector3( -1.0f, +1.0f, 0.0f),
            new Vector3(  1.0f, +1.0f, 0.0f),
            new Vector3(  1.0f, -1.0f,  0.0f),
        });

        mesh.SetColors(new Color[] {
            Color.red,
            Color.green,
            Color.blue,
            Color.cyan,
            });

        mesh.SetTriangles(new int[] {
              //上面
              3,2,0, 0,2,1,
              }, 0);

        var mr = GetComponent<MeshRenderer>();

        //コードから触る時はMeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        // 変更箇所 : 参照するシェーダー名  
        renderer.material = new Material(Shader.Find("Unlit/VertexColorShader"));

    }
}
