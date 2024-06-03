using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap m_Grid;
    // Start is called before the first frame update
    void Update()
    {
        Tilemap tiles = m_Grid.GetComponent<Tilemap>();
        //Debug.Log(tiles.localBounds);
        //Debug.Log(tiles.cellBounds);
    }

    // Update is called once per frame
    void Start()
    {
        Debug.Log(m_Grid.size);
        Debug.Log("hello");
        Tilemap tilemap = m_Grid.GetComponent<Tilemap>();
        //-3.5,0,1.5
        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                for (int z = tilemap.cellBounds.min.z; z < tilemap.cellBounds.max.z; z++)
                {

                    Debug.Log(tilemap.GetTile(new Vector3Int(x, y, z)));

                }
            }

        }
    }
}
