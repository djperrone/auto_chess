using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    readonly GameObject m_Tile;
    readonly int m_Index;
    bool m_IsOccupied;

    public Node(GameObject tile, int index, bool isOccupied)
    {
        m_Tile = tile;
        m_Index = index;
        m_IsOccupied = isOccupied;
    }

    public Vector3 Position()
    {
        return m_Tile.transform.position;
    }

    public int Index { get { return m_Index; } }
    public GameObject Tile{ get { return m_Tile; } }
    public bool IsOccupied { get { return m_IsOccupied; } set { m_IsOccupied = value; } }
}
