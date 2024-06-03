using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : BaseEntity
{
    private int m_MaxMana, m_CurrentMana;
    private Rarity m_Rarity;
    private Subclass m_Subclass;

    //private Item m_Item;

    private List<Trait> m_Traits;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int MaxMana { get { return m_MaxMana; } set { m_MaxMana = value; } }
    public int CurrentMana { get { return m_CurrentMana; } set { m_CurrentMana = value; } }
    public Subclass Subclass { get { return m_Subclass; } set { m_Subclass = value; } }
    public Rarity Rarity { get { return m_Rarity; } set { m_Rarity = value; } }


}
