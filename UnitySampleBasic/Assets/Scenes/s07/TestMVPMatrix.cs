using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace s07
{
    //https://light11.hatenadiary.com/entry/2020/02/19/224324

    [RequireComponent(typeof(Camera))]
    [ExecuteAlways]
    public class TestMVPMatrix : MonoBehaviour
    {
        public Material m_material;
        private static bool _didRegisterOnPreRender = false;

        private void OnEnable()
        {
#if UNITY_EDITOR

            //�@SceneView����GmaeView�֕��A����Ƃ���������ʉ߂���
            var camera = GetComponent<Camera>();
            SetMatrix(camera); 
            

            if (_didRegisterOnPreRender == false) {
                Camera.onPreRender += OnPreRender;// ����͑�������camera������ق�
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

        //�V�[���r���[�̎��́A�����炪�Ă΂��
        private void OnPreRender(Camera camera)
        {
            if (camera == null) {
                return;
            }
            if (camera.name == "SceneCamera" || camera.name == "Preview Camera") {
                SetMatrix(camera);
            }else{

            }
        }

        private void OnPreRender()
        {
            SetMatrix(GetComponent<Camera>());
        }

        //static private void SetGlobalMatrix(Camera camera)
        //{
        //    var viewMatrix = camera.worldToCameraMatrix;
        //    var projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
        //    Shader.SetGlobalMatrix("_MatrixVP", projectionMatrix * viewMatrix);
        //    //m_material.SetMatrix("_MatrixVP", projectionMatrix * viewMatrix);
        //}

        private void SetMatrix(Camera camera)
        {
            var viewMatrix = camera.worldToCameraMatrix;
            var projectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
            m_material.SetMatrix("_MatrixVP", projectionMatrix * viewMatrix);

        }
    }//class
}
