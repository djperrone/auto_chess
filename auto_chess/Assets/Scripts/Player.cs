using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    None = 0,
    A,
    B,
}

public class Player : MonoBehaviour
{
    Leader m_Leader;
    List<Piece> m_Pieces;
    private int m_Health;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Health { get { return m_Health; } set { m_Health = value; } }
}
