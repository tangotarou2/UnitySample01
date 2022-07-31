using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace common
{ 
    public class CommonBehaviour : MonoBehaviour
    {
        protected string FM = "F3";

        protected bool NearEqual(float target, float kijun, float gosa = 0.001f)
        {

            if ((kijun-gosa) <  target && target < (kijun + gosa)) {
                return true;
            }
            return false;
        }

        protected bool NearEqual(Vector3 target, Vector3 kijun, Vector3 gosa)
        {
            var xx = NearEqual(target.x, kijun.x, gosa.x);
            var yy = NearEqual(target.y, kijun.y, gosa.y);
            var zz = NearEqual(target.z, kijun.z, gosa.z);
            if (xx && yy &&zz)
                return true;
            return false;
        }
    }
}
