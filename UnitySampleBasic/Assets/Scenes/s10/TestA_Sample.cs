using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestA_Sample : MonoBehaviour
{
    private int _value = 0;

    //値が変更された時に実行されるイベント
    public event System.Action<int> ChangedValue = null;

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
        //同じ値が来た場合は設定しないし、イベントも実行しない
        if (_value == value) {
            return;
        }

        _value = value;
        if(ChangedValue != null)
            ChangedValue(_value);
    }
}

