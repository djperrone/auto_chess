using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    public static Stack<int> RecoverPath(int startIndex, int endIndex, int currentIndex, Dictionary<int, int> cameFrom)
    {
        Stack<int> path = new Stack<int>();
        //path.Add(endIndex);
        while (currentIndex != startIndex)
        {
            path.Push(currentIndex);
            currentIndex = cameFrom[currentIndex];
        }
        //path.Add(startIndex);

        //path.Reverse();
        Debug.Log("found path with steps: " + path.Count.ToString());
        return path;

    }
    public static Stack<int> FindPath(int startIndex, int endIndex, Board board)
    {
        SortedSet<(float, int)> openList = new SortedSet<(float, int)>();
        Dictionary<int, int> cameFrom = new Dictionary<int, int>();
        Dictionary<int, float> costSoFar = new Dictionary<int, float>();

        cameFrom[startIndex] = startIndex;
        costSoFar[startIndex] = 0;

        openList.Add((0.0f, startIndex));
        int cnt = 0;
        var endPositions = board.AdjacencyList(endIndex);
        while (openList.Count > 0)
        {
            cnt += 1;
            Debug.Log("OpenList" + cnt.ToString());
            foreach(var item in openList)
            {
                Debug.Log(item.Item2 + " " + item.Item1);
            }
            (float f, int index) current = openList.Min;
            openList.Remove(current);
            if (endPositions.Contains(board.At(current.index)))
            {
                return RecoverPath(startIndex, endIndex, current.index, cameFrom);
            }
            Debug.Log("current: " + current.index.ToString());
            List<Node> neigbors = board.AdjacencyList(current.index);
            Debug.Log("neighbors loop");
            Debug.Log(neigbors.Count);

            string neighs = "";
            foreach (var neighbor in neigbors)
            {
                neighs += neighbor.Index.ToString() + " ";
            }
            Debug.Log("neighs: " + neighs);
            foreach (Node n in neigbors)
            {
                if (!n.IsOccupied)
                {
                    float g = 1.0f;
                    float newCost = costSoFar[current.index] + g;
                    if (!costSoFar.ContainsKey(n.Index) || newCost < costSoFar[n.Index])
                    {
                        float h = Vector3.Distance(n.Position(), board.At(endIndex).Position());
                        costSoFar[n.Index] = newCost;
                        float priority = newCost + h;
                        openList.Add((priority, n.Index));
                        cameFrom[n.Index] = current.index;
                    }
                }
            }
        }
        Debug.Log("No path found????");
        return null;
        //return Option.None<Stack<int>>();
    }
}
