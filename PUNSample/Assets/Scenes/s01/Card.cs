using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Share
//{
//    public int fixid;
//    public int atk_plus;  //デバフ
//    public int visibleIcon;
//}

public class Card 
{


    //public Share m_share = new Share();
    //public string name;
    public int fixid;
    public int atk_plus;  //デバフ
    public int visibleIcon;

    public int GetFixID(){
        return fixid;
    }
    public static byte[] Serialize(object i_customobject)
    {
        Card card = (Card)i_customobject;
        //card.m_share = new Share();
        var bytes = new byte[ 3*sizeof(int)];
        int index = 0;
        ExitGames.Client.Photon.Protocol.Serialize(card.fixid, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(card.atk_plus, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize(card.visibleIcon, bytes, ref index);
        return bytes;
    }

    public static object Deserialize(byte[] i_bytes)
    {
        Card card = new Card();
        int index = 0;
        ExitGames.Client.Photon.Protocol.Deserialize(out card.fixid, i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out card.atk_plus, i_bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out card.visibleIcon, i_bytes, ref index);
        return card;
    }
}
