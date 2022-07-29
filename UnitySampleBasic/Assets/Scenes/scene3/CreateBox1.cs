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


        // ���_���X�g���쐬
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

            //���
            4,5,7,
            7,5,6,

            // ��O�F�@XY���ʂƐڂ���
           0,4,3,
           3,4,7,
           �@
           // ���@XY���ʁ@�����P
           2,6,1,
           1,6,5,


           // YZ���ʂƐڂ���
           1,5,0,
           0,5,4,

           // YZ���� X = 1
           3,7,2,
           2,7,6,
        };
        //mesh.SetColors(colors.ToArray());
        mesh.SetVertices(vertices);
        mesh.SetIndices(indexes, MeshTopology.Triangles,0);

        //�R�[�h����G�鎞��MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

    }
    /*
    void test2()
    {
        // ���b�V�����쐬
        Mesh mesh = new Mesh();

        float XXX = 3.0f;
        // ���_���X�g���쐬
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

        // �ʂ��\������C���f�b�N�X���X�g���쐬
        List<int> triangles = new List<int> {
            0, 1, 2,  // ����
            0, 2, 3,  // ����
            4, 5, 6,  // �O��
            4, 6, 7,  // �O��
            0, 4, 7,  // �E��
            0, 3, 4,  // �E��
            6, 2, 1,  // ����
            6, 5, 2,  // ����
            6, 1, 0,  // ���
            7, 6, 0,  // ���
            4, 3, 2,  // ����
            5, 4, 2,  // ����
        };


        // ���b�V���ɒ��_��o�^����
        mesh.SetVertices(vertices);

        // ���b�V���ɖʂ��\������C���f�b�N�X���X�g��o�^����
        mesh.SetTriangles(triangles, 0);


        // �쐬�������b�V�������b�V���t�B���^�[�ɐݒ肷��
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

