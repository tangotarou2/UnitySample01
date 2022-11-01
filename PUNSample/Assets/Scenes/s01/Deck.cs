using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class Deck 
{
    public List<Card> m_list = new List<Card>();
 
    public int CountOfCard()
    {
        if (m_list == null) return 0;
        return m_list.Count;
    }
    public Card GetCard(int index)
    {
        if (0 <= index && index < CountOfCard()) {
            return m_list[index];
        }else{
            return null;
        }
    }

    public bool MakeDeck()
    {
        int num_card = 6;
        var mgr = GameManager.instance;
        if (mgr == null) return false;
        var db = mgr.m_cardDB;

        for (int i = 0; i< num_card; i++) {
            int id = UnityEngine.Random.RandomRange(0, db.GetMax());

            var card = new Card();
            card.fixid = id;
            //card.name = db.GetFixData(id).name;

            m_list.Add(card);
        }
        return true;
    }

    //unsafe public static byte[] Serialize(object customobject)
    //{
    //    var deck = customobject as Deck;

    //    var buffList = new List<byte[]>();
    //    int length = 0;
    //    foreach( var cd  in deck.m_list){
    //        var bytes = ExitGames.Client.Photon.Protocol.Serialize(cd);
    //        buffList.Add(bytes);
    //        length += bytes.Length;
    //    }
    //    byte[] sumbuff = new byte[length];
    //    int offset = 0;

    //    Array array =sumbuff;

    //    IntPtr array_itr = new IntPtr(array);
    //    foreach (var buff in buffList) {
    //        //array.
    //        //Array.Copy(buff, sumbuff[offset], buff.Length);

    //    }

    //    return null;

    //    //IntPtr sumbuff_top = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * length);
    //    //int offset = 0;
    //    //foreach (var buff in buffList) {
    //    //    var sumbuff = IntPtr.Add(sumbuff_top, offset);
    //    //    Marshal.Copy(buff, 0, sumbuff, buff.Length);
    //    //}

    //    //return (byte[])sumbuff_top.ToPointer();
    //}

    //public static object Deserialize(byte[] bytes)
    //{
    //   // var obj = JsonConvert.DeserializeObject<Deck>(bytes);
    //    var obj = ExitGames.Client.Photon.Protocol.Serialize(bytes);

    //    return obj;
    //}


    //var json = System.Text.Encoding.UTF8.GetString(bytes);
    //string json = System.Text.Encoding.UTF8.GetString(bytes);



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

    public static Deck Test(Deck deck)
    {
        //Deck deck = (Deck)customobject;
        var json = JsonConvert.SerializeObject(deck);// 2022.10 Unity公式( 昔はnewtonsoft)
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);


        var json_d = System.Text.Encoding.UTF8.GetString(bytes);
        var result_deck = JsonConvert.DeserializeObject<Deck>(json_d);

        return result_deck;

    }


    public static byte[] Serialize(object customobject)
    {
        Deck deck = (Deck)customobject;
        var json = JsonConvert.SerializeObject(deck);// 2022.10 Unity公式( 昔はnewtonsoft)
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    public static object Deserialize(byte[] bytes)
    {
        var json = System.Text.Encoding.UTF8.GetString(bytes);
        var deck = JsonConvert.DeserializeObject<Deck>(json);
        return deck;
    }



}
