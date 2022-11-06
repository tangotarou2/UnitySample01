using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 
 https://qiita.com/Idenon/items/8a1d29e0d372bfb73965
 
 */


public class Ticket
{
    public int punid;
    public int no;

    public void Clear()
    {
        punid = 0;
        no = 0;
    }
    public int GetNo()
    {
        return no;
    }
    public bool isBlank()
    {
        return punid == 0 ? true : false;
    }

    public static byte[] Serialize(object i_customobject)
    {
        Ticket tick = (Ticket)i_customobject;
        var bytes = new byte[2*sizeof(int)];
        int index = 0;
        ExitGames.Client.Photon.Protocol.Serialize(tick.punid, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(tick.no, bytes, ref index);
        return bytes;
    }

    public static object Deserialize(byte[] i_bytes)
    {
        Ticket tick = new Ticket();
        int index = 0;
        ExitGames.Client.Photon.Protocol.Deserialize(out tick.punid, i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out tick.no, i_bytes, ref index);
        return tick;
    }
}

public class EntryTicket : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    static public int ticket_max = 4;

    public Ticket m_ticket = new Ticket();


    public bool isBlank()
    {
        return m_ticket.isBlank();
    }
    public void Clear()
    {
        m_ticket.Clear();
    }

    public void SetPunID(int id){
        m_ticket.punid = id;

    }
    public int GetPunID()
    {
        return m_ticket.punid;

    }

    public void SetNo(int no)
    {
        m_ticket.no = no;
    }
    public int GetNo()
    {
        return m_ticket.no;
    }

    [PunRPC]
    private void RPCSetNo(int no, int punid)
    {
        m_ticket.no = no;
        m_ticket.punid = punid;
        
    }

    void Start()
    {

    }
    void OnEnable(){
        StartCoroutine("MovetoParent");
    }

    IEnumerator MovetoParent(){

        while( true){
            var ms = PlayerMasterClient.instance;
            if( ms){
                this.transform.SetParent(ms.transform);
                break;
            }
            yield return new WaitForEndOfFrame();

        }
        yield break;
    }

    void Update()
    {
        if (photonView.IsMine) {
            photonView.RPC("RPCSetNo", RpcTarget.Others, GetNo(), GetPunID());
        }
    }


    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
        // 自身が所有権を持つインスタンスで所有権のリクエストが行われたら、常に許可して所有権を移譲する
        if (targetView.IsMine) {
            bool acceptsRequest = true;
            if (acceptsRequest) {
                var newOwner = photonView.Owner;
                targetView.TransferOwnership(newOwner);
            } else {
                // リクエストを拒否する場合は、何もしない
            }
        }
    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {

        string id = targetView.ViewID.ToString();
        string p1 = previousOwner.NickName;
        string p2 = targetView.Owner.NickName;
        Debug.Log($"ViewID {id} の所有権が {p1} から {p2} に移譲されました");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Photon.Realtime.Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }

    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting) {
    //        // Transformの値をストリームに書き込んで送信する
    //        stream.SendNext(transform.localPosition);
    //        stream.SendNext(transform.localRotation);
    //        stream.SendNext(transform.localScale);
    //        stream.SendNext(this.m_no);
    //    } else {
    //        // 受信したストリームを読み込んでTransformの値を更新する
    //        transform.localPosition = (Vector3)stream.ReceiveNext();
    //        transform.localRotation = (Quaternion)stream.ReceiveNext();
    //        transform.localScale = (Vector3)stream.ReceiveNext();
    //        this.m_no = (int)stream.ReceiveNext();
    //    }
    //}




}
