using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Piece : God
{
    Option<Item> m_Item;

    public Option<Item> Item { get { return m_Item; } set { m_Item = value; } }
}
