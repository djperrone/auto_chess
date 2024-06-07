using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject baseEntityPrefab;
    public GameObject thirdPersonPrefab;
    private int count = 0;

    public GameObject SpawnPiece(int index, Color color)
    {
        var obj = Instantiate(baseEntityPrefab);
        string name = "pieceID" + count++.ToString();
        obj.GetComponent<BaseEntity>().Name = name;
        obj.transform.position = new Vector3(m_Nodes[index].Position().x, m_Nodes[index].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[index].Position().z);
        //obj2.GetComponent<BaseEntity>().Node = m_Nodes[15];
        obj.GetComponent<BaseEntity>().TileIndex = index;
        m_Nodes[index].Reserve(name);
        obj.GetComponentInChildren<Renderer>().material.color = color;
        return obj;
    }

    public GameObject SpawnThirdPersonPrefab(int index, Color color)
    {
        var obj = Instantiate(thirdPersonPrefab);
        string name = "pieceID" + count++.ToString();
        obj.GetComponent<ThirdPersonController>().Name = name;
        obj.transform.position = new Vector3(m_Nodes[index].Position().x, m_Nodes[index].Position().y + 3.0f, m_Nodes[index].Position().z);
        //obj.transform.position = new Vector3(m_Nodes[index].Position().x, m_Nodes[index].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[index].Position().z);
        //obj2.GetComponent<BaseEntity>().Node = m_Nodes[15];
        obj.GetComponent<ThirdPersonController>().TileIndex = index;
        m_Nodes[index].Reserve(name);
        obj.GetComponentInChildren<Renderer>().material.color = color;
        return obj;
    }

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
        for (int i = 0; i < m_Nodes.Count; i++)
        {
            m_Nodes[i].SetIndex(i);
        }

       
        var obj = SpawnThirdPersonPrefab(0, Color.white);




        //var obj2 = SpawnPiece(15, Color.blue);
        var obj2 = SpawnThirdPersonPrefab(15, Color.blue);

        var obj3 = SpawnThirdPersonPrefab(35, Color.green);
        //var obj3 = SpawnPiece(35, Color.green);
        var obj4 = SpawnThirdPersonPrefab(57, Color.yellow);
        //var obj4 = SpawnPiece(57, Color.yellow);
        m_Pieces = new List<GameObject>
        {
            obj,
            obj2,
            obj3
        };

        obj.GetComponent<ThirdPersonController>().Target = obj2.GetComponent<ThirdPersonController>();
        //obj.GetComponent<BaseEntity>().Target = obj2.GetComponent<BaseEntity>();

        obj2.GetComponent<ThirdPersonController>().Target = obj3.GetComponent<ThirdPersonController>();
        //obj2.GetComponent<BaseEntity>().Target = obj3.GetComponent<BaseEntity>();
        obj3.GetComponent<ThirdPersonController>().Target = obj4.GetComponent<ThirdPersonController>();
        //obj3.GetComponent<BaseEntity>().Target = obj4.GetComponent<BaseEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!started)
        //{
        //    foreach (var obj in m_Pieces)
        //    {
        //        Debug.Log(obj.GetComponent<BaseEntity>().TileIndex);
        //        StartCoroutine(ColorNeighborsRed(obj.GetComponent<BaseEntity>().TileIndex));
        //    }
        //    //StartCoroutine(ColorNeighborsRed(m_Pieces[0].GetComponent<BaseEntity>().Node.Index));
        //    //StartCoroutine(ColorNeighborsRed(5));
        //    //StartCoroutine(ColorNeighborsRed(20));
        //    //StartCoroutine(ColorNeighborsRed(40));
        //    started = true;
        //}
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
            yield return new WaitForSeconds(0.1f);
        }

    }

    public Node At(int index)
    {
        return m_Nodes[index];
    }

    public List<Node> AdjacencyList(int index)
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

    public static List<int> AdjacentIndices(int index)
    {
        (int i, int j) = Board.Index2D(index);
        List<int> adjacentIndices = new List<int>();

        for (int k = i - 1; k <= i + 1; k++)
        {
            if (k >= 0 && k < m_Height)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    if (l >= 0 && l < m_Width)
                    {
                        if (k != i || l != j)
                            adjacentIndices.Add(Board.Index(k, l));
                    }
                }
            }
        }

        return adjacentIndices;
    }

    public static int ChebyshevDistance(int x1, int y1, int x2, int y2)
    {
        // Calculate the Chebyshev distance
        int dx = Math.Abs(x2 - x1);
        int dy = Math.Abs(y2 - y1);

        return Math.Max(dx, dy);
    }

    // Treats positions in 1d array as if they were in 2d
    public static int ChebyshevDistance1D(int i, int j)
    {
        // Calculate the Chebyshev distance

        var (x1, y1) = Board.Index2D(i);
        var (x2, y2) = Board.Index2D(j);

        return ChebyshevDistance(x1, y1, x2, y2);
    }

    //public int Distance(int i, int j)
    //{
    //    // Calculate the Chebyshev distance

    //    var node1 = Board.At(i);
    //    var node1 = m_
    //    return ChebyshevDistance(x1, y1, x2, y2);
    //}

}


