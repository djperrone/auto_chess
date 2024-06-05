using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject baseEntityPrefab;
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

        //var obj = Instantiate(baseEntityPrefab);
        //obj.transform.position = new Vector3(m_Nodes[0].Position().x, m_Nodes[0].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[0].Position().z);
        ////obj.GetComponent<BaseEntity>().Node = m_Nodes[0];
        //obj.GetComponent<BaseEntity>().TileIndex = 0;
        //m_Nodes[0].Reserve(obj.gameObject.name);

        var obj = SpawnPiece(0, Color.white);


        //var obj2 = Instantiate(baseEntityPrefab);
        //obj2.transform.position = new Vector3(m_Nodes[15].Position().x, m_Nodes[15].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[15].Position().z);
        ////obj2.GetComponent<BaseEntity>().Node = m_Nodes[15];
        //obj2.GetComponent<BaseEntity>().TileIndex = 15;
        //m_Nodes[15].Reserve(obj2.gameObject.name);
        //obj2.GetComponentInChildren<Renderer>().material.color = Color.blue;
        var obj2 = SpawnPiece(15, Color.blue);



        //var obj3 = Instantiate(baseEntityPrefab);
        //obj3.transform.position = new Vector3(m_Nodes[35].Position().x, m_Nodes[35].Position().y + obj.gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y, m_Nodes[35].Position().z);
        //obj3.GetComponent<BaseEntity>().TileIndex = 35;
        //m_Nodes[35].Reserve(obj3.gameObject.name);
        //obj3.GetComponentInChildren<Renderer>().material.color = Color.green;

        var obj3 = SpawnPiece(35, Color.green);
        var obj4 = SpawnPiece(57, Color.yellow);
        m_Pieces = new List<GameObject>
        {
            obj,
            obj2,
            obj3
        };

        //Stack<int> path = PathFinding.FindPath(obj.GetComponent<BaseEntity>().TileIndex, obj2.GetComponent<BaseEntity>().TileIndex, this);
        obj.GetComponent<BaseEntity>().Target = obj2.GetComponent<BaseEntity>();
        //obj.GetComponent<BaseEntity>().Calc;
        //obj.GetComponent<BaseEntity>().Path = path;

        //Stack<int> path2 = PathFinding.FindPath(obj2.GetComponent<BaseEntity>().TileIndex, obj.GetComponent<BaseEntity>().TileIndex, this);
        obj2.GetComponent<BaseEntity>().Target = obj3.GetComponent<BaseEntity>();
        obj3.GetComponent<BaseEntity>().Target = obj4.GetComponent<BaseEntity>();
        //obj2.GetComponent<BaseEntity>().Path = path;
        Debug.Log("path :");


        //var p2 = path.ToList();
        //foreach(var p in p2)
        //{
        //    Debug.Log(p.ToString());
        //}

        //foreach(var p in path)
        //{
        //    var node = m_Nodes[p];
        //    node.Tile.GetComponent<Renderer>().material.color = Color.red;
        //}
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


