using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject baseEntityPrefab;

    static int Index(int i, int j)
    {
        return m_Width * i + j;
    }
    static Tuple<int, int> Index2D(int index)
    {
        return new Tuple<int, int>(index / m_Height, index % m_Width);
    }

    bool started = false;
    List<Node> m_Nodes;
    const int m_Width = 8, m_Height = 8;

    List<GameObject> m_Pieces;

    //}
    void Start()
    {
        m_Nodes = new List<Node>();
        var playArea = this.gameObject.transform.Find("PlayArea");
        //float timer = 0f;
        for (int i = 0; i < playArea.transform.childCount; i++)
        {
            var go = playArea.transform.GetChild(i);
            m_Nodes.Add(new Node(go.gameObject, i, false));
        }

        m_Nodes = m_Nodes.OrderBy(tile => -tile.Position().z).ThenBy(tile => tile.Position().x).ToList();

        var obj = Instantiate(baseEntityPrefab);
        obj.transform.position = new Vector3(m_Nodes[0].Position().x, m_Nodes[0].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[0].Position().z);
        //obj.GetComponent<BaseEntity>().Node = m_Nodes[0];
        obj.GetComponent<BaseEntity>().SetIndex(0);


        var obj2 = Instantiate(baseEntityPrefab);
        obj2.transform.position = new Vector3(m_Nodes[15].Position().x, m_Nodes[15].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[15].Position().z);
        //obj2.GetComponent<BaseEntity>().Node = m_Nodes[15];
        obj2.GetComponent<BaseEntity>().SetIndex(15);


        var obj3 = Instantiate(baseEntityPrefab);
        obj3.transform.position = new Vector3(m_Nodes[35].Position().x, m_Nodes[35].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[35].Position().z);
        obj3.GetComponent<BaseEntity>().SetIndex(35) ;

        m_Pieces = new List<GameObject>
        {
            obj,
            obj2,
            obj3
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            foreach (var obj in m_Pieces)
            {
                Debug.Log(obj.GetComponent<BaseEntity>().Index);
                StartCoroutine(ColorNeighborsRed(obj.GetComponent<BaseEntity>().Index));
            }
            //StartCoroutine(ColorNeighborsRed(m_Pieces[0].GetComponent<BaseEntity>().Node.Index));
            //StartCoroutine(ColorNeighborsRed(5));
            //StartCoroutine(ColorNeighborsRed(20));
            //StartCoroutine(ColorNeighborsRed(40));
            started = true;
        }
    }

    IEnumerator ColorNeighborsRed(int index)
    {
        //foreach (var go in m_Tiles)
        //{
        //    //Debug.Log(go.name);
        //    //Debug.Log(go.position);
        //    go.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //    yield return new WaitForSeconds(1.0f);
        //}

        //for (int i = 0; i < m_Width; i++)
        //{
        //    for (int j = 0; j < m_Height; j++)
        //    {
        //        int index = Board.Index(i, j);
        //        var tile = m_Tiles[index];
        //        tile.Tile.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //        yield return new WaitForSeconds(0.5f);

        //    }
        //}

        List<Node> nodes = AdjacencyList(index);

        //foreach(var node in nodes)
        //{
        //    node.Tile.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //    yield return new WaitForSeconds(0.5f);
        //}

        //nodes = AdjacencyList(5);

        foreach (var node in nodes)
        {
            node.Tile.gameObject.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
        }

    }

    List<Node> AdjacencyList(int index)
    {
        (int i, int j) = Board.Index2D(index);
        List<Node> adjacentNodes = new List<Node>();

        for (int k = i - 1; k <= i + 1; k++)
        {
            if (k >= 0 && k < m_Height)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    if (l >= 0 && l < m_Width)
                    {
                        if (k != i || l != j)
                            adjacentNodes.Add(m_Nodes[Board.Index(k, l)]);
                    }
                }
            }
        }

        return adjacentNodes;
    }

}


