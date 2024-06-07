using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    private int m_MaxHealth, m_CurrentHealth, m_AttackPower, m_Protection, m_CritChance, m_AttackSpeed;
    private int m_Range = 1;
    private string m_Name;
    int m_TileIndex;
    //Node m_Node;
    BaseEntity m_Target;
    int m_LastKnownTargetIndex;
    Stack<int> m_Path;
    //float m_Range = 1.50f;

    private bool m_IsMoving = false;
    private float m_MovementSpeed = 2.50f;

    public int TileIndex { get { return m_TileIndex; } set { m_TileIndex = value; } }

    public BaseEntity Target
    {
        get { return m_Target; }
        set
        {
            if (m_Target)
            {
                Debug.Log(this.Name + "already has target");
            }
            else
            {
                Debug.Log("setting target" + value.Name);
                m_Target = value;
                m_LastKnownTargetIndex = value.GetComponent<BaseEntity>().TileIndex;
                CalcPathToTarget();
            }
        }
    }

    public Stack<int> Path
    {
        get { return m_Path; }
        set
        {
            if (value != null)
            {
                Debug.Log("setting path");
                Debug.Log(value);
                m_Path = value;
                m_IsMoving = true;
            }
            else
            {
                Debug.Log("Path not found");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (m_Target == null)
        {
            // find targdet
            return;
        }
        Board board = GameObject.Find("GameBoard").GetComponent<Board>();

        if(m_Target.TileIndex != m_LastKnownTargetIndex && !IsInRange(m_Target))
        {
            // might need rotate if un rabge
            CalcPathToTarget();
        }

        if (IsInRange(m_Target) && !IsMoving)
        {
            //if (m_IsMoving)
            //{
            //    var nextNode = board.At(TileIndex);
            //    //transform.position = new Vector3(nextNode.Position().x, transform.position.y, nextNode.Position().z);

            //    m_IsMoving = false;
            //}
            //Attack
            return;
        }
        if (!IsInRange(m_Target) && !IsMoving)
        {
            CalcPathToTarget();
        }

        if (IsMoving && m_Path.Count > 0)
        {
            int nextIndex = m_Path.Peek();
            if (board.At(nextIndex).Owner != Name)
            {
                CalcPathToTarget();
            }
            var target = board.At(nextIndex);
            board.At(nextIndex).Tile.GetComponent<Renderer>().material.color = gameObject.GetComponentInChildren<Renderer>().material.color;

            if (!MoveTowards(target))
            {
            }
            else
            {
                // Finished Moving to target but not finished path
                board.At(m_TileIndex).Release(Name);
                m_TileIndex = nextIndex;
                board.At(nextIndex).Reserve(Name);
                m_Path.Pop();

                if (m_Path.Count  == 0)
                {
                    m_IsMoving = false;
                }
            }
        }
        else
        {
            m_IsMoving = false;
        }

        Debug.Log(Name + " is moving?: " + m_IsMoving.ToString());

    }

    public void CalcPathToTarget()
    {
        Board board = GameObject.Find("GameBoard").GetComponent<Board>();
        m_Path = PathFinding.FindPath(TileIndex, m_Target.TileIndex, board);

        m_LastKnownTargetIndex = m_Target.TileIndex;
        m_IsMoving = true;
        if (m_Path.Count > 0)
        {
            board.At(m_Path.Peek()).Reserve(Name);
        }

    }

    // true if at position, false if still needs to move
    protected bool MoveTowards(Node nextNode)
    {
        Vector3 direction = (nextNode.Position() - this.transform.position);
        if (direction.sqrMagnitude <= 0.01f)
        {
            transform.position = new Vector3(nextNode.Position().x, transform.position.y, nextNode.Position().z);
            //animator.SetBool("walking", false);
            return true;
        }
        //animator.SetBool("walking", true);

        this.transform.position += direction.normalized * m_MovementSpeed * Time.deltaTime;
        //Debug.Log("pos: " + this.transform.position.ToString());
        return false;
    }


    public int MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
    public int CurrentHealth { get { return m_CurrentHealth; } set { m_CurrentHealth = value; } }

    public int AttackPower { get { return m_AttackPower; } set { m_AttackPower = value; } }
    public int AttackSpeed { get { return m_AttackSpeed; } set { m_AttackSpeed = value; } }
    public int Protection { get { return m_Protection; } set { m_Protection = value; } }
    public int Range { get { return m_Range; } set { m_Range = value; } }
    public int CritChance { get { return m_CritChance; } set { m_CritChance = value; } }
    public string Name { get { return m_Name; } set { m_Name = value; } }
    public Vector3 Position { get { return gameObject.transform.position; } set { gameObject.transform.position = value; } }

    public bool IsMoving { get { return m_IsMoving; } set { m_IsMoving = value; } }

    public bool IsInRange(BaseEntity other)
    {
        return Board.ChebyshevDistance1D(m_TileIndex, other.TileIndex) <= m_Range;
        //return Distance(other) <= m_Range;
    }

    public float Distance(BaseEntity other)
    {
        return Vector3.Distance(Position, other.Position);
    }
}
