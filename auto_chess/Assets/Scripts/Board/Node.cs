using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    readonly GameObject m_Tile;
    int m_Index;
    bool m_IsOccupied = false;
    //bool m_IsValidMove = true;
    bool m_IsReserved = false;
    string m_Owner;
    public bool Reserve(string owner)
    {
        if (IsValidMove)
        {
            m_Owner = owner;
            m_IsReserved = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsValidMove { get { return !m_IsReserved && !m_IsOccupied; } }



    //public bool IsReserved { get { return m_IsReserved; } set { 
    //        if (!m_IsReserved && !m_IsOccupied)
    //        m_IsReserved = value; 
    //    } }
    //public bool IsValidMove { get { return m_IsOccupied == false && m_IsReserved == false; }
    //    set { }

    //}
    public Node(GameObject tile, int index, bool isOccupied)
    {
        m_Tile = tile;
        m_Index = index;
        m_IsOccupied = isOccupied;
    }

    public void SetIndex(int index)
    {
        m_Index = index;
    }

    public Node(Node other)
    {
        m_Tile = other.m_Tile;
        m_Index = other.m_Index;
        m_IsOccupied |= other.m_IsOccupied;
    }

    public Vector3 Position()
    {
        return m_Tile.transform.position;
    }

    public int Index { get { return m_Index; } }
    public GameObject Tile { get { return m_Tile; } }
    public bool IsOccupied { get { return m_IsOccupied; } }

    public void Release(string id)
    {
        if (id == m_Owner)
        {
            m_IsOccupied = false;
            m_IsReserved = false;
        }
    }

    public string Owner { get { return m_Owner; } } 

    //public bool Occupy()
    //{
    //    //if (IsVal)
    //}
}
