using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace s02
{

    public class LookAt : CommonBehaviour
    {
        public GameObject m_target;

        // Start is called before the first frame update
        void Start()
        {
            Matrix4x4 mat = Matrix4x4.identity;
            Quaternion qt = Quaternion.identity;

            //北緯36度、東経140度
            //qt = Quaternion.Euler(36, 140, 0); //ZXY

            var pt = this.transform.localPosition;
            var dir = pt;

            qt = Quaternion.LookRotation(dir);
            this.transform.localRotation = qt;
            // var thePosition = transform.TransformPoint(Vector3.right * 2);

        }

        // Update is called once per frame
        void UpdateKey()
        {
            var cam = Camera.main;
            KeyCode code = KeyCode.None;
            if (Input.GetKeyDown(KeyCode.Space)) {
                code = KeyCode.Space;
            }

            if (Input.GetKeyDown(KeyCode.Return)) {
                code = KeyCode.Return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (m_target == null) return;
                {
                    var pt = m_target.transform.localPosition;
                    var localpos = string.Format(" {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));
                    pt = m_target.transform.position;
                    var pos = string.Format(" {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));

                    Debug.Log(string.Format("localpos = {0}, pos = {1}", localpos, pos));

                }

                {
                    var localTarget = m_target.transform.localPosition;
                    var worldPoint = transform.TransformPoint(localTarget); // local->world



                    var pt = worldPoint;
                    var t_pos = string.Format(" {0},{1},{2}", pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));

                    Debug.Log(string.Format("worldp = {0}", t_pos));

                }
            }


            if (code != KeyCode.None) {

                float org_mag = this.transform.localPosition.magnitude;
                var forwd = this.transform.forward;// 単位ベクトル
                Debug.Assert(NearEqual(forwd.magnitude, 1.0f, 0.001f));

                {
                    var pt = this.transform.localPosition;
                    if (code == KeyCode.Space)
                        pt += forwd*0.5f;
                    else
                        pt -= forwd*0.5f;

                    this.transform.localPosition = pt;

                    var scaleF = this.transform.localPosition.magnitude/org_mag;
                    var sclaeV = transform.localScale;


                    transform.localScale = sclaeV * scaleF;
                }


            }


        }

        void Update()
        {
            UpdateKey();
        }
    }

}