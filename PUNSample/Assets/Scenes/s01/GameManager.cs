using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CardDataDB m_cardDB = new CardDataDB();

    public Material m_matClose;
    public Material m_matPrefab;

    public Player m_isMinePlayer;
    private void Awake()
    {
        instance = this;
        m_cardDB.InitDB(m_matPrefab);
    }
    public FixData GetFixData(int id){
        return m_cardDB.GetFixData(id);
    }


}
