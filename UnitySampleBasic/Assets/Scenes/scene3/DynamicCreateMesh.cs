using UnityEngine;
using System.Collections;

/// <summary>  
/// 頂点カラーで三角形を描画します  
/// </summary>  
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class DynamicCreateMesh : MonoBehaviour
{
    private void Start()
    {
        var mesh = new Mesh();
        mesh.SetVertices(new Vector3[] {
      new Vector3 (0, 1f),
      new Vector3 (1f, -1f),
      new Vector3 (-1f, -1f),
    });
        mesh.SetTriangles(new int[] { 0, 1, 2 }, 0);

        // 変更箇所 : 各頂点に色情報を設定  
        mesh.SetColors(new Color[] {
      Color.red, Color.green, Color.blue
    });

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        // 変更箇所 : 参照するシェーダー名  
        renderer.material = new Material(Shader.Find("Unlit/VertexColorShader"));
    }
}