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

        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Deck), 0x41, Deck.Serialize, Deck.Deserialize);
        ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(Card), 0x42, Card.Serialize, Card.Deserialize);

    }

    public override void OnConnectedToMaster()
    {
        var option = new RoomOptions();
        option.PlayerTtl = ttl;

        PhotonNetwork.JoinOrCreateRoom("Room", option, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        var prefabname = (PhotonNetwork.IsMasterClient)? "Player" : "Player";

        var gobj = PhotonNetwork.Instantiate(prefabname, position, Quaternion.identity);
        if (gobj) {
            gobj.SetActive(true);
            if ((PhotonNetwork.IsMasterClient)) {
                gobj.transform.localPosition = new Vector3(0,-3f,0);
                gobj.name = "Player_M";
            } 
        }

        //if (PhotonNetwork.IsMasterClient){
        //    PhotonNetwork.Instantiate("TestCode", position, Quaternion.identity);
        //}

    }
}