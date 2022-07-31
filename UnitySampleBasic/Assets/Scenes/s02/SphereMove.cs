using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace s02
{
    public class SphereMove : CommonBehaviour
    {
        [SerializeField] GameObject m_point;
        [SerializeField] Text m_text;

        // Start is called before the first frame update
        void Start()
        {
            var pt = m_point.transform.localPosition;
            var ret1 = test01(pt);
            var ret2 = test02(pt);

            var ret3 = test03_ZXY(pt);
            Debug.Assert(NearEqual(ret2, ret3, Vector3.one/1000.0f));

            pt = ret2;
            m_text.text = string.Format("pt = {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));
            m_point.transform.localPosition = pt;
        }

        Vector3 test03_ZXY(Vector3 pt)
        {
            // Z
            {
                var rot_qt = Quaternion.Euler(0, 0, 30);
                Matrix4x4 mat = Matrix4x4.Rotate(rot_qt);
                pt = mat * pt;
            }

            // X
            {
                var rot_qt = Quaternion.Euler(30, 0, 0);
                Matrix4x4 mat = Matrix4x4.Rotate(rot_qt);
                pt = mat * pt;
            }

            // Y
            {
                var rot_qt = Quaternion.Euler(0, 45, 0);
                Matrix4x4 mat = Matrix4x4.Rotate(rot_qt);
                pt = mat * pt;
            }
            return pt;
        }

        // qtの順序　ZXY
        Vector3 test02(Vector3 pt)
        {
            var rot_qt = Quaternion.Euler(30, 45, 30);
            Matrix4x4 mat = Matrix4x4.Rotate(rot_qt);
            pt = mat * pt;
            m_text.text = string.Format("pt = {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));
            return pt;
        }

        // Y -> Z の順序
        Vector3 test01(Vector3 pt)
        {
            //左手系なので、軸先端からみて時計回りが＋方向
            //Vector3 pt = Vector3.zero;
            {
                var rot_qt = Quaternion.Euler(0, 45, 0);
                Matrix4x4 mat_y = Matrix4x4.Rotate(rot_qt);
                pt = mat_y * m_point.transform.localPosition;

                Debug.Log(string.Format("rotY45: {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM)));

                var ans1 = new Vector3(0.707f, 0.000f, -0.707f);
                Debug.Assert(NearEqual(ans1, pt, Vector3.one/1000.0f));
            }

            {
                var qt = Quaternion.Euler(0, 0, 30);
                Matrix4x4 mat = Matrix4x4.Rotate(qt);
                pt = mat *pt;
                Debug.Log(string.Format("rotZ30: {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM)));

                m_text.text = string.Format("pt = {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));

                var ans2 = new Vector3(0.612f, 0.354f, -0.707f);
                Debug.Assert(NearEqual(ans2, pt, Vector3.one/1000.0f));
            }
            return pt;
        }

    }

}