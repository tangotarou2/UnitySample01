using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class TimeCounter : MonoBehaviour
{
#if false
    /// <summary>
    /// �C�x���g�n���h���i�C�x���g���b�Z�[�W�̌^��`�j
    /// </summary>
    public delegate void TimerEventHandler(int time);

    /// <summary>
    /// �C�x���g
    /// </summary>
    public event TimerEventHandler OnTimeChanged;


    void Start()
    {
        //�^�C�}�N��
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        //100����J�E���g�_�E��
        var time = 100;
        while (time > 0) {
            time--;
            //�C�x���g�ʒm
            OnTimeChanged(time);

            //1�b�҂�
            yield return new WaitForSeconds(1);
        }
    }
#else
    //�C�x���g�𔭍s����j�ƂȂ�C���X�^���X
    private Subject<int> timerSubject = new Subject<int>();

    //�C�x���g�̍w�Ǒ����������J
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
        //100����J�E���g�_�E��
        var time = 100;
        while (time > 0) {
            time--;

            //�C�x���g�𔭍s
            timerSubject.OnNext(time);

            //1�b�҂�
            yield return new WaitForSeconds(1);
        }
    }
#endif
}