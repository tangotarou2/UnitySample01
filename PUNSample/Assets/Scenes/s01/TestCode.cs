using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class TestCode : MonoBehaviourPunCallbacks
{

    public Button m_buttonTest;
    public Button m_buttonSync;

    public Deck m_deck = new Deck();
    public Card m_card = new Card();



    void SyncCard(Card val)
    {
        photonView.RPC("RPCSyncCard", RpcTarget.All, val);
    }

    [PunRPC]
    private void RPCSyncCard(Card val)
    {
        m_card = val;
    }



    bool Test_card()
    {
        var tcd = new Card();
        if (tcd !=null) {
            //tcd.name = "hoge";
            SyncCard(tcd);

            //if ( m_card.name != tcd.name ) {
            //    return false;
            //}

        }
        return true;

    }

    void test_deck(){
        var deck = new Deck();
        deck.MakeDeck();

        var deck_o = Deck.Test(deck);

        int max = deck_o.CountOfCard();
        for (int i = 0; i < max; i++){

            var A = deck.GetCard(i);
            var B = deck_o.GetCard(i);
            if( A.fixid != B.fixid){
                Debug.Assert(false);
            }


        }

    }

    // Start is called before the first frame update
    void Start()
    {

     //   if (photonView.IsMine)

        {


            m_buttonSync = GameObject.Find("SyncButton").GetComponent<Button>();
            if (m_buttonSync!=null) {
                m_buttonSync.onClick.AddListener(() => {
                   // GameManager.instance.m_isMinePlayer.DoSync();

                });
            }


            m_buttonTest = GameObject.Find("TestButton").GetComponent<Button>();
            if (m_buttonTest!=null) {
                m_buttonTest.onClick.AddListener(() => {
                // GameManager.instance.m_isMinePlayer.DoSync();
                    Test_card();
                    test_deck();
                });
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
