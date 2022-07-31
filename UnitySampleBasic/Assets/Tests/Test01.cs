using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test01
{

    //加法定理・極座標とQT
    [Test]
    public void Test01_01()
    {
        float alpha = 30.0f * Mathf.Deg2Rad;
        float beta = 30.0f * Mathf.Deg2Rad;
        var P = new Vector3( Mathf.Cos(alpha) , 0, Mathf.Sin(alpha));
        var P2 = new Vector3( Mathf.Cos(alpha+beta) , 0, Mathf.Sin(alpha+beta));


        var rot = Quaternion.Euler(0, -60.0f,0 );
        var P3 = rot *Vector3.right;
        var result  = CommonUtil.NearEqual(P3, P2, Vector3.one/100.0f);


        Assert.That(result);
    }


    //https://osinko.hatenablog.jp/entry/2017/04/05/184123
    //球面座標とQT
    [Test]
    public void Test01_02()
    {

        float R = 1.0f;
        float theta = 60;
        float theta_rad = theta * Mathf.Deg2Rad;
        float phi = 45.0f;
        float phi_rad = phi * Mathf.Deg2Rad;

        //var P = new Vector3(
        //     R*Mathf.Sin(theta_rad)*Mathf.Cos(phi_rad),
        //     R*Mathf.Cos(theta_rad),
        //     R*Mathf.Sin(theta_rad)*Mathf.Sin(phi_rad));

        var P = new Vector3(
             R*Mathf.Cos(phi_rad)*Mathf.Sin(theta_rad),
             R*Mathf.Sin(phi_rad),
             R*Mathf.Cos(phi_rad)*Mathf.Cos(theta_rad));

        var rot1 = Quaternion.Euler(-phi, 0,0);
        var rot2 = Quaternion.Euler(0, theta, 0);
        
        var P_ROT = rot1 * new Vector3(0,0,1);
            P_ROT = rot2 * P_ROT;

        var result = CommonUtil.NearEqual(P, P_ROT, Vector3.one/100.0f);
        Debug.Log("P = " + P.ToString());
        Debug.Log("P_ROT = " + P_ROT.ToString());

        Assert.That(result);
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Test01WithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
