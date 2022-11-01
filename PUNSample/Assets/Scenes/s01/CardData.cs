using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FixData
{
    public FixData(int id, string name, string filename, int FixData)
    {
        this.id = id;
        this.name = name;
        this.filename = filename;
        this.data1 = FixData;
        
    }
    public string filename;
    public string name;
    public int id;
    public int data1;
    public Material mat;

};

public class CardDataDB
{
    public List<FixData> m_list = new List<FixData>();

    public int GetMax( ){
        return m_list.Count;

    }
    public FixData GetFixData( int index)
    {
        return m_list[index];

    }

    public CardDataDB()
    {

    }
    public void InitDB(Material mat)
    {
        //https://www.irasutoya.com/2018/10/blog-post_526.html
        FixData[] datatbl = new FixData[]{
            new FixData(1, "かぼちゃ","halloween_chara1_pumpkin", 120),
            new FixData(2, "死神",    "halloween_chara2_shinigami", 150),
            new FixData(3, "ミイラ",  "halloween_chara3_miira", 120),
            new FixData(4, "狼男","halloween_chara4_ookamiotoko", 150),
            new FixData(5, "ドラキュラ", "halloween_chara5_dracula", 140),
            new FixData(6, "魔女",       "halloween_chara6_majo", 140),
            new FixData(7, "おばけ",  "halloween_chara7_obake", 130),
            new FixData(8, "フランケン","halloween_chara8_frankenstein", 150),
            new FixData(9, "ゾンビ",  "halloween_chara9_zombie", 150),
        };

        m_list.AddRange(datatbl);

        foreach (var dat in datatbl) {
            var texture = Resources.Load(dat.filename) as Texture;
            if(texture!=null){
                dat.mat = new Material(mat);
                dat.mat.mainTexture = texture;
            }else{
                Debug.Assert(false);
            }
        }
    }
}

