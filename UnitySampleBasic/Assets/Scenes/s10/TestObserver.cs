using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

class SampleA
{
    //������string���n����Subject
    private Subject<string> _subject = new Subject<string>();

    //Subject�̂����AIObservable���������J���A������o�^�ł���悤��
    public IObservable<string> Observable
    {
        get { return _subject; }
    }
}



public class TestObserver : MonoBehaviour
{

    //    https://kan-kikuchi.hatenablog.com/entry/What_is_UniRx

    public Button m_button;

    SampleA _sampleA = new SampleA();

    void test1(){
        //������string���n����Subject���쐬(int��bool�ȂǑ��̌^�ł�OK)
        var sub = new Subject<string>();

        //Subscribe���g���ď�����o�^����
        sub.Subscribe(text => Debug.Log(text));

        //"�e�L�X�g"�Ƃ���string��n���āA���������s(���O���\�������)
        sub.OnNext("�e�L�X�g");

    }

    void test2()
    {

        _sampleA.Observable.Subscribe(text => Debug.Log(text));

        //OnNext���g���ď��������s���鎖�͏o���Ȃ��B
        //_sampleA.Observable.OnNext("�e�L�X�g");
    }


    void test3(){
        var sub = new Subject<string>();
        //onNext�Œʏ펞�̏����AonError�ŃG���[���̏����AonCompleted�ŏI�����̏�����o�^
        sub.Subscribe(
          onNext: text => Debug.Log("�e�L�X�g�I : " + text),
          onError: error => Debug.Log("�G���[�I : " + error),
          onCompleted: () => Debug.Log("����!")
        );

        sub.OnNext("�e�L�X�g1");
        sub.OnCompleted();
        sub.OnNext("�e�L�X�g2");//���s����Ȃ�

    }

    //�I�y���[�^
    void test4(){
        var sub = new Subject<string>();

        //Subscribe�Ń��O�̏�����ǉ��A���̑O��Where��Select�̏������ݒ�
        sub.Where(text => text.Length < 10) //10���������̎������A����ȍ~�̏���������
          .Select(text => text + text[0])   //1�����ڂ��Ō���ɒǉ�
          .Subscribe(text => Debug.Log(text));

        //�����̎��s
        sub.OnNext("�e�L�X�g�e�L�X�g�e�L�X�g�e�L�X�g"); //10�����ȏ�Ȃ̂ŁA���O�͕\������Ȃ�(Select�����s����Ȃ�)
        sub.OnNext("�e�L�X�g");                  //10���������Ȃ̂ŁA1������(�e)���Ō���ɒǉ��������O(�e�L�X�g�e)���\�������

    }

    void test5(){
        //������string���n����Subject���쐬
        var sub = new Subject<string>();

        //Subscribe�Ń��O�̏�����ǉ��A���̑O��Where��Select�̏������ݒ�
        sub.Where(text => text.Length < 10) //10���������̎������A����ȍ~�̏���������
          .Select(text => text + text[0])   //1�����ڂ��Ō���ɒǉ�
          .Subscribe(
            onNext: text => Debug.Log(text),
            onError: error => Debug.Log("�G���[�I : " + error)
        );

        //�����̎��s
        sub.OnNext("");       //1�����ڂ��Ȃ��̂ŁASelect��text[0]�ŃG���[���o��
        sub.OnNext("�e�L�X�g"); //���s����Ȃ�
    }


    //�x������
    void test6(){
 
        
        //2�b��Ƀ��O��\�����s����
        StartCoroutine(DelayMethod(2f, () => {
            Debug.Log("2�b�x��Ď��s");
        }));
        

      //  Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => Debug.Log("2�b�x��Ď��s_ unirx"));

        //2�b��ɏ��������s�����IObservable���쐬
        IObservable<long> observable = Observable.Timer(TimeSpan.FromSeconds(2));

        //������o�^
        observable.Subscribe(_ => Debug.Log("2�b�x��Ď��sunirx"));

    }
    //�n���ꂽ�������w�莞�Ԍ�Ɏ��s����
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        //�w�莞�ԑ҂�
        yield return new WaitForSeconds(waitTime);

        //���������s
        action();
    }

    void test7()
    {
        //F
        //  .OnClickAsObservable()
        //  .Buffer(2) //2�񕪂̏������܂Ƃ߂čs��
        //  .Subscribe(_ => Debug.Log("�����I"));


        //IObservable�������N���b�N�̃C�x���g�𐶐�
        IObservable<Unit> observable = m_button.OnClickAsObservable();
        //IObservable<Unit> observable = GetComponent<Button>().onClick.AsObservable();//���̏������ł�����

        //�{�^�����������Ƃ��̏�����ǉ�
        observable.Buffer(2).Subscribe(_ => Debug.Log("�����I"));
    }

    
    [SerializeField] private TestA_Sample test_sampleA = null;
    [SerializeField] private TestA_Sample2 test_sampleB = null;

    void test8(){
        //    test_sampleA.ChangedValue += (value) => debug_log(value.ToString());
        test_sampleB.Observable.Subscribe(count => Debug.Log(count));


    }
    void debug_log(string value){
        Debug.Log(value);

    }

    void Awake()
    {
        test8();

    }

    void Start(){
    }



    void test99(){ 
        //    public sealed class Subject<T> : ISubject<T>, IDisposable, IOptimizedObservable<T>
        //public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)


        Subject<string> subject = new Subject<string>();

        //3��Subscribe
       // subject.Subscribe(msg => Debug.Log("Subscribe1:" + msg));
        //subject.Subscribe(msg => Debug.Log("Subscribe2:" + msg));
        //subject.Subscribe(msg => Debug.Log("Subscribe3:" + msg));


        //OnNext & OnCompleted
        subject.Subscribe(
            msg => Debug.Log("Subscribe1:" + msg),
            () => Debug.Log("Completed"));

        //�C�x���g���b�Z�[�W���s
        subject.OnNext("����ɂ���");
        subject.OnNext("���͂悤");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
