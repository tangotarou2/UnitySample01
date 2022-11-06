using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks
{
    public string name;
    public int punid;
    public int hp;
    public int mp;
    public int count;
    public int ticket_no;
    public Deck m_deck = new Deck();

    void Start()
    {
        GameManager.instance.m_isMinePlayer  = this;

        name = photonView.Owner.NickName;
        punid = photonView.Owner.ActorNumber;

        //自分のときだけ作成する
        if (this.photonView.IsMine) {
            hp = UnityEngine.Random.Range(1, 100);
            mp = UnityEngine.Random.Range(1, 100);

            m_deck.MakeDeck();

        }
        this.name = string.Format("{0}({1})", this.name, photonView.Owner.ActorNumber);
        StartCoroutine("SyncData");

        //Coroutine _co = (Coroutine)EnterByTicket();
        StartCoroutine("EnterByTicket");
    }

    int GetPhotonID(){
        int id = photonView.Owner.ActorNumber;
        return id;

    }

    IEnumerator EnterByTicket(){

        while( true){
            var result = PlayerMasterClient.instance.UseTicketFromID(GetPhotonID());
            if (result)
                break;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    string GetFieldName( int index ) {
        string[] tbl_field = new string[]{
            "Field_A",
            "Field_B",
            "Field_C",
            "Field_D",
        };
        if (0 <= index && index < tbl_field.Length) {
            return tbl_field[index];
        }
        return "";
    }

    void Bind(Deck deck)
    {

        if (deck == null) return;
        if (deck.CountOfCard() >0) {

            var ticket = PlayerMasterClient.instance.GetTicketFromID(GetPhotonID());
            if (ticket == null) return;

            var field = GameObject.Find(GetFieldName(ticket_no-1));
            if (field!=null) {
                var monsters = field.GetComponentsInChildren<DrawMonster>();
                for (int i = 0; i < monsters.Length; i++) {
                    Card card = deck.GetCard(i);
                    monsters[i].SetData(card);
                }
            }
        }
    }

    #region sync
    public float span = 1f;

    IEnumerator SyncData()
    {
        while (true) {
            yield return new WaitForSeconds(span);
            //Debug.LogFormat("{0}秒経過", span);
            DoSync();
        }
    }

    public void DoSync()
    {
        if (this.photonView.IsMine) {
            SyncData(m_deck);
        }
        Bind(m_deck);
    }

    //DeckをRPCで同期するためには、登録とSerialize関数が必要。
    void SyncData(Deck deck)
    {
        photonView.RPC("RPCSyncData", RpcTarget.All, deck);
    }

    [PunRPC]
    private void RPCSyncData(Deck deck)
    {
        count = deck.CountOfCard();
        m_deck = deck;
    }

    [PunRPC]
    private void RPCSyncCard(Card val, int mp)
    {
        this.mp = mp;
    }
    #endregion

    void SetName(string name){
        this.name = name;
    }

    void Update()
    {

        if (photonView.IsMine) {
            UpdatePlayerPos();
            DoSync();
        } else {

        }

        string infomsg = "";
        infomsg += string.Format("{0},hp:{1} mp:{2}\n", this.name, this.hp, this.mp);

        var ticket = PlayerMasterClient.instance.GetTicketFromID(GetPhotonID());
        if (ticket != null) {
            this.ticket_no = ticket.GetNo();
            infomsg += string.Format("seat_no : {0}\n", ticket_no);
        }
        


        int numofcard = m_deck.CountOfCard();
        infomsg += string.Format("deck : {0},{1}\n", numofcard, this.count);

        var t_text = transform.GetComponentInChildren<TMP_Text>();
        if (t_text!=null) {
            t_text.text = infomsg;
        }

    }
    void UpdatePlayerPos()
    {
        float ax = 0.02f;
        float ay = 0.02f;

        var tpos = transform.localPosition;
        if( Input.GetKey(KeyCode.RightArrow)){
            tpos.x += ax;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            tpos.x -= ax;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            tpos.y += ax;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            tpos.y -= ax;
        }
        transform.localPosition= tpos;
    }


    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting) {
    //        // Transformの値をストリームに書き込んで送信する
    //        stream.SendNext(transform.localPosition);
    //        stream.SendNext(transform.localRotation);
    //        stream.SendNext(transform.localScale);
    //        stream.SendNext(this.hp);
    //      //  stream.SendNext(this.mp);
    //        stream.SendNext(this.count);
    //    } else {
    //        // 受信したストリームを読み込んでTransformの値を更新する
    //        transform.localPosition = (Vector3)stream.ReceiveNext();
    //        transform.localRotation = (Quaternion)stream.ReceiveNext();
    //        transform.localScale = (Vector3)stream.ReceiveNext();
    //        this.hp = (int)stream.ReceiveNext();
    //     //   this.mp = (int)stream.ReceiveNext();
    //        this.count = (int)stream.ReceiveNext();
    //    }
    //}
}
