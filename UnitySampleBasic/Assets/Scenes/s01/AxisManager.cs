using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisManager : MonoBehaviour
{

    public GameObject m_PlaneRoot;
    public GameObject m_Ball;
    public Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        Matrix4x4 mat = Matrix4x4.identity;
        Quaternion qt = Quaternion.identity;

        //�k��36�x�A���o140�x
        qt = Quaternion.Euler( 36,140,0); //ZXY

        m_PlaneRoot.transform.localRotation = qt;


        var thePosition = transform.TransformPoint(Vector3.right * 2);

    }

    // Update is called once per frame
    void Update()
    {

        var work_text = "";
        work_text += "ball = ";

        work_text  += m_Ball?.transform.localPosition.ToString() + "\n";
        work_text  += m_Ball?.transform.position.ToString();
        m_text.text = work_text;

    }
}
