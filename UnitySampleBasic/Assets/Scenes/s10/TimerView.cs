using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


//https://qiita.com/toRisouP/items/2f1643e344c741dd94f8
public class TimerView : MonoBehaviour
{
    //それぞれインスタンスはインスペクタビューから設定

    [SerializeField] private TimeCounter timeCounter;
    [SerializeField] private Text counterText; //uGUIのText
    /*
    void Start()
    {
        //タイマのカウンタが変化したイベントを受けてuGUI Textを更新する
        timeCounter.OnTimeChanged += time => // =>は「ラムダ式」と呼ばれる匿名関数の記法
        {
            //現在のタイマ値をUIに反映する
            counterText.text = time.ToString();
        };
    }
    */

    void Start()
    {
        //タイマのカウンタが変化したイベントを受けてuGUI Textを更新する
        timeCounter.OnTimeChanged.Subscribe(time =>
        { 
            //現在のタイマ値をUIに反映する
            counterText.text = time.ToString();
        });
    }
}
