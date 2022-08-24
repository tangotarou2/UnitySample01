using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestA_Sample2 : MonoBehaviour
{
    private ReactiveProperty<int> _valueReactiveProperty = new ReactiveProperty<int>(0);

    //ReactiveProperty�̂����AIObservable���������J���A������o�^�ł���悤��
    public IObservable<int> Observable
    {
        get { return _valueReactiveProperty; }
    }

    private void Start()
    {
        //�Ă��Ƃ��ɒl��ύX
        SetValue(1);
        SetValue(1);
        SetValue(2);
        SetValue(2);
        SetValue(1);
    }

    //�l��ݒ肷��
    private void SetValue(int value)
    {
        _valueReactiveProperty.Value = value;
    }
}
