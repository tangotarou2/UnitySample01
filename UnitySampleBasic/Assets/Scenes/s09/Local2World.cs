using common;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace s09
{
    public class Local2World : CommonBehaviour
    {
        public GameObject m_target;

        // Start is called before the first frame update
        void Start()
        {
            //    Kidou();
            Dump(this.gameObject);

        }

        // 起動時処理
        void Kidou()
        {
            //Debug.Log("Time : " + Time.time + ", Main ThreadID:" + System.Threading.Thread.CurrentThread.ManagedThreadId);

            // 10秒ごとに実行（Timerかつ第一引数に 0 指定の場合、Subscribe後に即座に1回目の処理が実行される）
            Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(8))
                .Subscribe(x => {
                    //Debug.Log("Timer Time : " + Time.time + ", No : " + x.ToString() +", ThreadID : " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                    Dump(m_target);
                }
                ).AddTo(this);

            //// 10秒ごとに実行（Intervalの場合、Subscribe後に待機時間を待機してから1回目の処理が実行される）
            //Observable.Interval(System.TimeSpan.FromSeconds(10))
            //    .Subscribe(x => {
            //        Debug.Log("Interval Time : " + Time.time + ", No : " + x.ToString() +
            //            ", ThreadID : " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //    }
            //    ).AddTo(this);
        }

        new protected string FM = "F1";

        void Dump(GameObject target)
        {
            Func<Vector3,string,string, string> vector_print = (pt,header, FM) => {
                return string.Format(header + " {0}, {1}, {2}", 
                    pt.x.ToString(FM), pt.y.ToString(FM), pt.z.ToString(FM));
            };

            var global_pt = target.transform.position;
            var text1 = vector_print(global_pt, "global_pt", FM);

            var local_pt = target.transform.localPosition;
            var text2 = vector_print(local_pt, "local_pt", FM);

            var oya_pt = target.transform.TransformPoint(
                // UnityEditorで設定したlocalPositionが原点なので、座標系を上にあがるだけなら、
                // zeroでよい。
                Vector3.zero  
            );
            var text3 = vector_print(oya_pt, "oya_pt", FM);
            
            var text4 = "";
            var oya = target.transform.parent;
            if (oya) {
                var world_pt = target.transform.TransformPoint(Vector3.zero);

                //var world_pt = oya.transform.localToWorldMatrix * oya_pt;
                //    text4 = vector_print(world_pt, "oya_world", FM);
            }

            var sz =text1 +"\n" +text2 + "\n"+text3 + "\n" + text4;
            Debug.Log(sz);

        }

    }

}