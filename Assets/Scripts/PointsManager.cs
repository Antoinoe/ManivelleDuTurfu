using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            print("PointsManager Singleton Already Existing");
            return;
        }
        instance = this;
    }

    [SerializeField] public SinglePoint startPoint, endPoint;
    [SerializeField] private GameObject Level;
    
    void Start()
    {
        SetupPath(startPoint);
    }

    private void SetupPath(SinglePoint point)
    {
        SinglePoint basePoint = point;
        SinglePoint currentPoint = point;
        ResetPath();
        currentPoint.ChangeStateTo(1);

        while (currentPoint.leftBranch[0] != null)
        {
                currentPoint = currentPoint.leftBranch[0];
                currentPoint.ChangeStateTo(1);
        }
        currentPoint = basePoint; //reset de la pos
        while (currentPoint.rightBranch[0] != null)
        {
                currentPoint = currentPoint.rightBranch[0];
                currentPoint.ChangeStateTo(1);
        }
    }

    private static void SetupPathByDirection(SinglePoint p, Direction d, int it)
    {
        var doOnce = true;
        if(d == Direction.Right)
        {
            while (p.rightBranch[0] != null)
            {
                if (doOnce)
                {
                    if (it++ >= p.rightBranch.Length-1)
                        it = 0;
                    p = p.rightBranch[it];
                    doOnce = false;
                }
                else
                    p = p.rightBranch[0];
                if (p != null)
                    p.ChangeStateTo(1);
            }
        }
        else
        {
            while (p.leftBranch[0] != null)
            {
                if (doOnce)
                {
                    if (it++ >= p.leftBranch.Length-1)
                        it = 0;
                    p = p.leftBranch[it];
                    doOnce = false;
                }
                else
                    p = p.leftBranch[0];
                if(p!=null)
                    p.ChangeStateTo(1);
            }
        }
        //print("changed path");
    }

    private void ResetPath()
    {
        var allpoints = new List<SinglePoint>();

        //Reset all SinglePointState
        for (var i = 0; i < Level.transform.childCount; i++)
        {
            allpoints.Add(Level.transform.GetChild(i).GetComponent<SinglePoint>());
            allpoints[i].ChangeStateTo(0);
        }
        //print("reset modafuka");
    }

    public void ChangePath(SinglePoint position)
    {
        //print("changing...");
        var indexAtLeft = GetNextIndexAtDirection(position, Direction.Left);
        var indexAtRight = GetNextIndexAtDirection(position, Direction.Right);
        //print("indexAtLeft : " + indexAtLeft + "- indexAtRight : " + indexAtRight);
        ResetPath();
        position.ChangeStateTo(1);
        SetupPathByDirection(position, Direction.Left, indexAtLeft);
        SetupPathByDirection(position, Direction.Right, indexAtRight);
    }

    private static int GetNextIndexAtDirection(SinglePoint p, Direction d)
    {
        var la = d==Direction.Left ?p.leftBranch : p.rightBranch;
        for (var i =0; i<la.Length; i++)
        {
            if (la[i] && la[i].isActive)
            {
                return i;
            } 
        }
        //Debug.LogError("Didn't find any active cell next door...");
        return -1;
    }
}

public enum Direction
{
    Left,
    Right
};


