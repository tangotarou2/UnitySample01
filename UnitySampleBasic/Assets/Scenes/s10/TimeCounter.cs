using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class TimeCounter : MonoBehaviour
{
#if false
    /// <summary>
    /// イベントハンドラ（イベントメッセージの型定義）
    /// </summary>
    public delegate void TimerEventHandler(int time);

    /// <summary>
    /// イベント
    /// </summary>
    public event TimerEventHandler OnTimeChanged;


    void Start()
    {
        //タイマ起動
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        //100からカウントダウン
        var time = 100;
        while (time > 0) {
            time--;
            //イベント通知
            OnTimeChanged(time);

            //1秒待つ
            yield return new WaitForSeconds(1);
        }
    }
#else
    //イベントを発行する核となるインスタンス
    private Subject<int> timerSubject = new Subject<int>();

    //イベントの購読側だけを公開
    public IObservable<int> OnTimeChanged
    {
        get { return timerSubject; }
    }

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        //100からカウントダウン
        var time = 100;
        while (time > 0) {
            time--;

            //イベントを発行
            timerSubject.OnNext(time);

            //1秒待つ
            yield return new WaitForSeconds(1);
        }
    }
#endif
}