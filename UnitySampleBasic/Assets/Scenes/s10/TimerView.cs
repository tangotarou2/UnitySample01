using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


//https://qiita.com/toRisouP/items/2f1643e344c741dd94f8
public class TimerView : MonoBehaviour
{
    //���ꂼ��C���X�^���X�̓C���X�y�N�^�r���[����ݒ�

    [SerializeField] private TimeCounter timeCounter;
    [SerializeField] private Text counterText; //uGUI��Text
    /*
    void Start()
    {
        //�^�C�}�̃J�E���^���ω������C�x���g���󂯂�uGUI Text���X�V����
        timeCounter.OnTimeChanged += time => // =>�́u�����_���v�ƌĂ΂�铽���֐��̋L�@
        {
            //���݂̃^�C�}�l��UI�ɔ��f����
            counterText.text = time.ToString();
        };
    }
    */

    void Start()
    {
        //�^�C�}�̃J�E���^���ω������C�x���g���󂯂�uGUI Text���X�V����
        timeCounter.OnTimeChanged.Subscribe(time =>
        { 
            //���݂̃^�C�}�l��UI�ɔ��f����
            counterText.text = time.ToString();
        });
    }
}
