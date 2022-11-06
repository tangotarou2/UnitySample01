using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 UnityEditor２個立ち上げて、片方はMasterClient（実質サーバー）として開発を進める。
 */
public class PlayerMasterClient : MonoBehaviourPunCallbacks
{
    public static PlayerMasterClient instance;
    EntryTicket[] m_ticketArray;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (this.photonView.IsMine) {
            StartCoroutine("CoSync");
        }

    }

    IEnumerator CoSync()
    {
        float span = 1F;
        while (true) {
            yield return new WaitForSeconds(span);
        //    photonView.RPC("RPCSyncData", RpcTarget.All, m_ticketArray);
        }
    }

    //[PunRPC]
    //private void RPCSyncData(Ticket[] list)
    //{
    //    m_ticketArray = list;
    //}


    public void ClearTicket()
    {
        if (m_ticketArray == null) return ;

        foreach (var ticket in m_ticketArray) {
            ticket.Clear();
        }
        m_ticketArray = null;
    }

    public bool UseTicketFromID(int punid)
    {
        if (m_ticketArray == null) return false;
        foreach (var ticket in m_ticketArray) {
            if (ticket.isBlank()) {

                ticket.SetPunID(punid);
                ticket.photonView.RequestOwnership();

                return true;
            }
        }
        return false;
    }
    public EntryTicket GetTicketFromID(int punid){

        if (m_ticketArray == null) return null;

        foreach (var ticket in m_ticketArray) {
            if(ticket.GetPunID() == punid){
                return ticket;
            }
        }
        return null;
    }

    void Update()
    {
        if (photonView.IsMine) {

        }

        if (m_ticketArray ==null) {

            var objs = GameObject.FindGameObjectsWithTag("Ticket");

            m_ticketArray = new EntryTicket[objs.Length];

            int i = 0;
            foreach (var obj in objs) {
                var entryTicket = obj.GetComponent<EntryTicket>();
                if (entryTicket) {
                    m_ticketArray[i++] = entryTicket;
                }
            }
        }
        
    }
}
