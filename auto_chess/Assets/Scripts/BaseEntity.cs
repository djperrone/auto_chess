using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    private int m_MaxHealth, m_CurrentHealth, m_AttackPower, m_Protection, m_CritChance, m_Range, m_AttackSpeed;
    private string m_Name;
    int m_TileIndex;
    //Node m_Node;
    BaseEntity m_Target;

    public void SetIndex(int index)
    {
        m_TileIndex = index;
    }

    public int Index { get { return m_TileIndex; } set { m_TileIndex = value; } }
    //public Node Node { get { return m_Node; } set { m_Node = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
    public int CurrentHealth { get { return m_CurrentHealth; } set { m_CurrentHealth = value; } }
   
    public int AttackPower { get { return m_AttackPower; } set { m_AttackPower = value; } }
    public int AttackSpeed { get { return m_AttackSpeed; } set { m_AttackSpeed = value; } }
    public int Protection { get { return m_Protection; } set { m_Protection = value; } }
    public int Range { get { return m_Range; } set { m_Range = value; } }
    public int CritChance { get { return m_CritChance; } set { m_CritChance = value; } }
    public string Name { get { return m_Name; } set { m_Name = value; } }
    
}
