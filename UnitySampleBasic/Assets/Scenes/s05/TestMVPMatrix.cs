using Mathd;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace s05
{
    //https://light11.hatenadiary.com/entry/2020/02/19/224324

    [RequireComponent(typeof(Camera))]
    [ExecuteAlways]
    public class TestMVPMatrix : MonoBehaviour
    {
        public Material m_material;
        private static bool _didRegisterOnPreRender = false;

        static string matrixName = "_matView";
        [SerializeField] GameObject m_target;

        private void OnEnable()
        {
#if UNITY_EDITOR

            //　SceneViewからGmaeViewへ復帰するときもここを通過する
            SetMatrix(GetCamera());


            if (_didRegisterOnPreRender == false) {
                Camera.onPreRender += OnPreRender;// これは第一引数にcameraがあるほう
                _didRegisterOnPreRender = true;
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            Camera.onPreRender -= OnPreRender;
            _didRegisterOnPreRender = false;
#endif
        }

        //シーンビューの時は、こちらが呼ばれる
        private void OnPreRender(Camera camera)
        {
            if (camera == null) {
                return;
            }

            if (camera.name == "SceneCamera" || camera.name == "Preview Camera") {
                SetMatrix(camera);
            } else {
                SetMatrix(camera);

            }
        }

        private void OnPreRender()
        {
            SetMatrix(GetCamera());
        }

        Camera GetCamera(){
            return GetComponent<Camera>();
        }

        Vector3 t_pos_graybox = new Vector3(1, 0, 0);


        Matrix4x4d convMatrixF2D(Matrix4x4 src ){
            Matrix4x4d dst = Matrix4x4d.identity;
            for ( int i = 0; i< 16; i++ ){
                dst[i] = (double)src[i];
            }
            return dst;
        }
        Matrix4x4 convMatrixD2F(Matrix4x4d src)
        {
            var dst = Matrix4x4.identity;
            for (int i = 0; i< 16; i++) {
                dst[i] = (float)src[i];
            }
            return dst;
        }


        private void SetMatrixDouble(Camera camera)
        {
            Matrix4x4d modelMatrix = convMatrixF2D(m_target.transform.localToWorldMatrix);
            var viewMatrix = convMatrixF2D(camera.worldToCameraMatrix);
            var projectionMatrix = convMatrixF2D(GL.GetGPUProjectionMatrix(camera.projectionMatrix, true));

            Vector3d t_pos_graybox_double = t_pos_graybox;


            Matrix4x4d MVP = Matrix4x4d.identity;
            {
                var modelViewMatrix = viewMatrix*modelMatrix;

                var t_pos = modelViewMatrix * t_pos_graybox_double;
                modelViewMatrix[0, 3] += t_pos.x;
                modelViewMatrix[1, 3] += t_pos.y;
                modelViewMatrix[2, 3] += t_pos.z;

                MVP = projectionMatrix*modelViewMatrix;

            }

             m_material.SetMatrix(matrixName, convMatrixD2F(MVP));
        }
        private void SetMatrix(Camera camera)
        {
            SetMatrixDouble(camera);
        }

        private void SetMatrixFloat(Camera camera)
        {
            Matrix4x4 modelMatrix = m_target.transform.localToWorldMatrix;
            var viewMatrix = camera.worldToCameraMatrix;
            var projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
            var MVP = Matrix4x4.identity;
            {
                var modelViewMatrix = viewMatrix*modelMatrix;
                
                var t_pos = modelViewMatrix * t_pos_graybox;
                modelViewMatrix[0, 3] += t_pos.x;
                modelViewMatrix[1, 3] += t_pos.y;
                modelViewMatrix[2, 3] += t_pos.z;

                MVP = projectionMatrix*modelViewMatrix;
            }
            m_material.SetMatrix(matrixName, MVP);
        }
    }//class
}

#if false

namespace s05
{

    [RequireComponent(typeof(Camera))]
    public class TestMVPMatrix : MonoBehaviour
    {
        public Material material;
        public Material material_2;
        public Material material_3;
        private static bool _didRegisterOnPreRender = false;



        private void OnEnable()
        {
#if UNITY_EDITOR
            //　SceneViewからGmaeViewへ復帰するときもここを通過する
            var t_cam = GetComponent<Camera>();
            SetSceneMatrix(t_cam);

            if (!_didRegisterOnPreRender) {
                Camera.onPreRender += OnPreRender;
                _didRegisterOnPreRender = true;
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            Camera.onPreRender -= OnPreRender;
            _didRegisterOnPreRender = false;
#endif
        }

        private void OnPreRender(Camera camera)
        {
            if (camera == null) {
                return;
            }
            if (camera.name == "SceneCamera" || camera.name == "Preview Camera") {
                SetMatrix_2(camera, material_2);
            }
        }

        private void OnPreRender()
        {
            var t_cam = GetComponent<Camera>();
            SetSceneMatrix(t_cam);
        }

        static string matName = "_matView";
        private void SetSceneMatrix(Camera camera)
        {
            //SetMatrix(camera, material);
            SetMatrix_2(camera, material_2);
            //SetMatrix_3(camera, material_3);
        }

        Vector3 t_pos_graybox = new Vector3(1, 1, 0);

        private void SetMatrix(in Camera camera, Material mat)
        {
            Matrix4x4 viewMatrix = camera.worldToCameraMatrix;

            var tr = Vector3.one;
            camera.transform.localPosition = tr;

            //         Y,X  // ��D��
            try {
                //�J������pos�������Ă���
                var t_pos = t_pos_graybox;
                viewMatrix[0, 3] += t_pos.x;
                viewMatrix[1, 3] += t_pos.y;
                viewMatrix[2, 3] += t_pos.z; //���������ݒ肵�Ȃ��ƃj�A�N���b�v��J�����w�ʂƂ��Ď�����N���b�v�ɂ�����
                mat.SetMatrix(matName, viewMatrix);
            } catch (Exception e) {
                Debug.Log(e.ToString());
            }
        }

        private void SetMatrix_2(in Camera camera, Material mat)
        {
          //  Matrix4x4 modelMatrix = this.transform.localToWorldMatrix;
            Matrix4x4 viewMatrix = camera.worldToCameraMatrix;
           // Matrix4x4 modelViewMatrix = viewMatrix * modelMatrix;

  
            //         Y,X  // ��D��
            try {
                //var t_pos = modelViewMatrix * t_pos_graybox;
                ////�J������pos�������Ă���
                //modelViewMatrix[0, 3] += t_pos.x;
                //modelViewMatrix[1, 3] += t_pos.y;
                //modelViewMatrix[2, 3] += t_pos.z;

                mat.SetMatrix(matName, viewMatrix);
            } catch (Exception e) {
                Debug.Log(e.ToString());
            }
        }

        private void SetMatrix_3(in Camera camera, Material mat)
        {
            //Matrix4x4 modelMatrix = this.transform.localToWorldMatrix;
            Matrix4x4 viewMatrix = camera.worldToCameraMatrix;
            Matrix4x4 projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);


            try {
                var VPMatrix = projectionMatrix* viewMatrix;
                mat.SetMatrix(matName, VPMatrix);

            } catch (Exception e) {
                Debug.Log(e.ToString());
            }
        }

        /*
            //var projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
            //Shader.SetGlobalMatrix("_MatrixVP", projectionMatrix * viewMatrix);


                        //var sendata = new float[3] { 1.0f,1.0f, 0.0f };
                //Shader.SetGlobalFloatArray("myido", sendata);

                //Shader.SetGlobalFloat("_MoveX", 5.0f);
                //Shader.SetGlobalFloat("_MoveY", 3.0f);
                //Shader.SetGlobalFloat("_MoveZ", 0.0f);

            //         Y,X  ���ꂾ�ƍs�D��ɂȂ�
            //viewMatrix[3, 0] = 3.0f;
            //viewMatrix[3, 1] = 3.0f;
            //viewMatrix[3, 2] = 0.0f;

            if (false) {
                //         Y,X  // ��D��
                try {
                    viewMatrix[0, 3] = 3.0f;
                    viewMatrix[1, 3] = 1.0f;
                    //viewMatrix[2, 3] = 1.0f; //������L���������BOX���\������Ȃ�
                    Shader.SetGlobalMatrix(matName, viewMatrix);

                } catch (Exception e) {
                    Debug.Log(e.ToString());
                }
            }
         */

    }

}
#endif