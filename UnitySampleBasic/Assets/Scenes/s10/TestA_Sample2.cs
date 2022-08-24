using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestA_Sample2 : MonoBehaviour
{
    private ReactiveProperty<int> _valueReactiveProperty = new ReactiveProperty<int>(0);

    //ReactivePropertyのうち、IObservableだけを公開し、処理を登録できるように
    public IObservable<int> Observable
    {
        get { return _valueReactiveProperty; }
    }

    private void Start()
    {
        //てきとうに値を変更
        SetValue(1);
        SetValue(1);
        SetValue(2);
        SetValue(2);
        SetValue(1);
    }

    //値を設定する
    private void SetValue(int value)
    {
        _valueReactiveProperty.Value = value;
    }
}
