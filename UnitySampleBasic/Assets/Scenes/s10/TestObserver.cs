using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

class SampleA
{
    //引数にstringが渡せるSubject
    private Subject<string> _subject = new Subject<string>();

    //Subjectのうち、IObservableだけを公開し、処理を登録できるように
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
        //引数にstringが渡せるSubjectを作成(intやboolなど他の型でもOK)
        var sub = new Subject<string>();

        //Subscribeを使って処理を登録する
        sub.Subscribe(text => Debug.Log(text));

        //"テキスト"というstringを渡して、処理を実行(ログが表示される)
        sub.OnNext("テキスト");

    }

    void test2()
    {

        _sampleA.Observable.Subscribe(text => Debug.Log(text));

        //OnNextを使って処理を実行する事は出来ない。
        //_sampleA.Observable.OnNext("テキスト");
    }


    void test3(){
        var sub = new Subject<string>();
        //onNextで通常時の処理、onErrorでエラー時の処理、onCompletedで終了時の処理を登録
        sub.Subscribe(
          onNext: text => Debug.Log("テキスト！ : " + text),
          onError: error => Debug.Log("エラー！ : " + error),
          onCompleted: () => Debug.Log("完了!")
        );

        sub.OnNext("テキスト1");
        sub.OnCompleted();
        sub.OnNext("テキスト2");//実行されない

    }

    //オペレータ
    void test4(){
        var sub = new Subject<string>();

        //Subscribeでログの処理を追加、その前にWhereとSelectの処理も設定
        sub.Where(text => text.Length < 10) //10文字未満の時だけ、これ以降の処理をする
          .Select(text => text + text[0])   //1文字目を最後尾に追加
          .Subscribe(text => Debug.Log(text));

        //処理の実行
        sub.OnNext("テキストテキストテキストテキスト"); //10文字以上なので、ログは表示されない(Selectも実行されない)
        sub.OnNext("テキスト");                  //10文字未満なので、1文字目(テ)を最後尾に追加したログ(テキストテ)が表示される

    }

    void test5(){
        //引数にstringが渡せるSubjectを作成
        var sub = new Subject<string>();

        //Subscribeでログの処理を追加、その前にWhereとSelectの処理も設定
        sub.Where(text => text.Length < 10) //10文字未満の時だけ、これ以降の処理をする
          .Select(text => text + text[0])   //1文字目を最後尾に追加
          .Subscribe(
            onNext: text => Debug.Log(text),
            onError: error => Debug.Log("エラー！ : " + error)
        );

        //処理の実行
        sub.OnNext("");       //1文字目がないので、Selectのtext[0]でエラーが出る
        sub.OnNext("テキスト"); //実行されない
    }


    //遅延処理
    void test6(){
 
        
        //2秒後にログを表示実行する
        StartCoroutine(DelayMethod(2f, () => {
            Debug.Log("2秒遅れて実行");
        }));
        

      //  Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => Debug.Log("2秒遅れて実行_ unirx"));

        //2秒後に処理が実行されるIObservableを作成
        IObservable<long> observable = Observable.Timer(TimeSpan.FromSeconds(2));

        //処理を登録
        observable.Subscribe(_ => Debug.Log("2秒遅れて実行unirx"));

    }
    //渡された処理を指定時間後に実行する
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        //指定時間待つ
        yield return new WaitForSeconds(waitTime);

        //処理を実行
        action();
    }

    void test7()
    {
        //F
        //  .OnClickAsObservable()
        //  .Buffer(2) //2回分の処理をまとめて行う
        //  .Subscribe(_ => Debug.Log("偶数！"));


        //IObservable化したクリックのイベントを生成
        IObservable<Unit> observable = m_button.OnClickAsObservable();
        //IObservable<Unit> observable = GetComponent<Button>().onClick.AsObservable();//この書き方でも同じ

        //ボタンを押したときの処理を追加
        observable.Buffer(2).Subscribe(_ => Debug.Log("偶数！"));
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

        //3回Subscribe
       // subject.Subscribe(msg => Debug.Log("Subscribe1:" + msg));
        //subject.Subscribe(msg => Debug.Log("Subscribe2:" + msg));
        //subject.Subscribe(msg => Debug.Log("Subscribe3:" + msg));


        //OnNext & OnCompleted
        subject.Subscribe(
            msg => Debug.Log("Subscribe1:" + msg),
            () => Debug.Log("Completed"));

        //イベントメッセージ発行
        subject.OnNext("こんにちは");
        subject.OnNext("おはよう");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
