using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMonster : MonoBehaviour
{
    public Card m_card;
    public int fixid;

    void Start()
    {
        m_renderer = this.GetComponentInChildren<MeshRenderer>();
        Debug.Assert(m_renderer);
    }

    MeshRenderer m_renderer = null;
    void Update()
    {
        if( m_card !=null)
            fixid = m_card.fixid;

        if ( m_card!=null){
            var id = m_card.GetFixID();
            FixData cd = GameManager.instance.GetFixData(id);
            if (cd!=null) {
                m_renderer.material.mainTexture = cd.mat.mainTexture;
            }
        }

    }

    public void SetData(Card cd){
        m_card = cd;
    }
}
