using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace S04
{

// Slerp‚ÌƒeƒXƒg
    public class TestSlerp : CommonBehaviour
    {
        [SerializeField] GameObject m_point;
        //[SerializeField] Text m_text;


        float rotY;

        void test_assert(){
            Debug.Assert(NearEqual(Mathf.Deg2Rad, Mathf.PI / 180f, 0.001f));
            Debug.Assert(NearEqual(Mathf.Rad2Deg, 57.29578f, 0.001f));


        }
        void test2(){

            float angleZ = 30.0f;
            float angleY = 90.0f;
            Vector3 t_pos1 = Quaternion.Euler(0, angleY, angleZ) * Vector3.right; // zxy
            m_point.transform.localPosition =t_pos1;


            Vector3 t_pos2 = Quaternion.Euler(0, angleY, 0) * Vector3.right; // zxy


        }

        // Start is called before the first frame update
        void Start()
        {
            test_assert();

            test2();

            start_pos = Vector3.right;//m_point.transform.localPosition;
        }


        Vector3 start_pos;
        void Update()
        {
            var tick = Time.frameCount / 120.0f;

            float angleZ = 30.0f;
            float angleY = 90.0f;
            var qt1 = Quaternion.Euler(0, angleY, angleZ);

            var  qt2= Quaternion.Euler(0, angleY, 0);

            var qt_slerp = Quaternion.Slerp(qt1, qt2, tick);
            var t_pos = qt_slerp * start_pos;
            m_point.transform.localPosition =t_pos;


        }
    }
}