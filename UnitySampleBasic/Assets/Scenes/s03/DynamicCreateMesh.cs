using UnityEngine;
using System.Collections;

/// <summary>  
/// ���_�J���[�ŎO�p�`��`�悵�܂�  
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

        // �ύX�ӏ� : �e���_�ɐF����ݒ�  
        mesh.SetColors(new Color[] {
      Color.red, Color.green, Color.blue
    });

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        // �ύX�ӏ� : �Q�Ƃ���V�F�[�_�[��  
        renderer.material = new Material(Shader.Find("Unlit/VertexColorShader"));
    }
}