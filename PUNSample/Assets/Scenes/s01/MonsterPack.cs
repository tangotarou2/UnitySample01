
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Utilities;


public class MonsterPack
{
    const int num_card = 6;

    public int m_num_card = 0;
    public Card[] m_list;


    public MonsterPack()
    {

    }

    public int CountOfCard()
    {
        return m_num_card;
    }
    public Card GetCard(int index)
    {
        if (0 <= index && index < CountOfCard()) {
            return m_list[index];
        } else {
            return null;
        }
    }


    public bool MakeDeck()
    {
  
        var mgr = GameManager.instance;
        if (mgr == null) return false;

        var db = mgr.m_cardDB;
        m_list = new Card[num_card];
        for (int i = 0; i< num_card; i++) {
            int id = Random.RandomRange(0, db.GetMax());

            var card = new Card();
            card.fixid = id;
            //card.name = db.GetFixData(id).name;

            //m_list.Add(card);
            m_list[i] = card;
        }

        return true;

    }


    //public static Deck Test(Deck d)
    //{

    //    var list = d.m_list;
    //    var json = JsonConvert.SerializeObject(list);// 2022.10 Unity公式( 昔はnewtonsoft)
    //    byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(json);


    //    string json2 = Encoding.GetEncoding("utf-8").GetString(bytes);
    //    var decode_list = JsonConvert.DeserializeObject<List<Card>>(json2);


    //    var deck = new Deck();
    //    deck.m_list = decode_list;
    //    return deck;
    //}

    public static byte[] Serialize(object customobject)
    {
        Deck card = (Deck)customobject;


        var bytes = new byte[3*sizeof(int)];
        //int index = 0;
        //for (int i = 0; i< num_card; i++) {
        //    var bytes = ExitGames.Client.Photon.Protocol.Serialize( card.m_list[i]);
        //}


        return bytes;
    }



    //public static object Deserialize(byte[] bytes)
    //{
    //    var deck = new Deck();

    //    string json2 = Encoding.GetEncoding("utf-8").GetString(bytes);
    //    var list = JsonConvert.DeserializeObject<List<Card>>(json2);


    //    deck.m_list =list;
    //    return deck;
    //}
}
