using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonScene : MonoBehaviourPunCallbacks
{
    public int ttl;

    private void Start()
    {
        // プレイヤー自身の名前を""に設定する
        PhotonNetwork.NickName = "MyPlayer";
        PhotonNetwork.ConnectUsingSettings();

        byte index = 0x41;
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Deck), index++, Deck.Serialize, Deck.Deserialize);
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Card), index++, Card.Serialize, Card.Deserialize);
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Ticket), index++, Ticket.Serialize, Ticket.Deserialize);


        
    }

    public override void OnConnectedToMaster()
    {
        var option = new RoomOptions();
        option.PlayerTtl = ttl;

        var resutl = PhotonNetwork.JoinOrCreateRoom("Room", option, TypedLobby.Default);


    }

    public override void OnJoinedRoom()
    {
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));

        GameObject gobj = null;
        var prefabname = (PhotonNetwork.IsMasterClient)? "PlayerMasterClient" : "Player";
        if (PhotonNetwork.IsMasterClient) {

            gobj = PhotonNetwork.Instantiate(prefabname, position, Quaternion.identity);
            gobj.transform.position = new Vector3(10, 0, 0);

            for (int i = 0; i< EntryTicket.ticket_max; i++) {
                var ticketobj = PhotonNetwork.Instantiate("EntryTicket", Vector3.zero, Quaternion.identity);
                var ticket = ticketobj.GetComponent<EntryTicket>();
                ticket.SetNo(i+1);

            }
        } else {
            gobj = PhotonNetwork.Instantiate(prefabname, position, Quaternion.identity);

        }

        if (gobj) {
            gobj.name = prefabname;
        }

    }
}