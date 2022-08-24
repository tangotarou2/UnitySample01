using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestA_Sample : MonoBehaviour
{
    private int _value = 0;

    //�l���ύX���ꂽ���Ɏ��s�����C�x���g
    public event System.Action<int> ChangedValue = null;

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
        //�����l�������ꍇ�͐ݒ肵�Ȃ����A�C�x���g�����s���Ȃ�
        if (_value == value) {
            return;
        }

        _value = value;
        if(ChangedValue != null)
            ChangedValue(_value);
    }
}

