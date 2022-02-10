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

    public void SetupPath(SinglePoint point)
    {
        SinglePoint basePoint = point;
        SinglePoint currentPoint = point;
        ResetPath();
        currentPoint.ChangeStateTo(1);

        while (currentPoint._leftBranch[0] != null)
        {
                currentPoint = currentPoint._leftBranch[0];
                currentPoint.ChangeStateTo(1);
        }
        currentPoint = basePoint; //reset de la pos
        while (currentPoint._rightBranch[0] != null)
        {
                currentPoint = currentPoint._rightBranch[0];
                currentPoint.ChangeStateTo(1);
        }
    }

    void SetupPathByDirection(SinglePoint p, Direction d, int it)
    {
        bool doOnce = true;
        if(d == Direction.Right)
        {
            while (p._rightBranch[0] != null)
            {
                if (doOnce)
                {
                    if (it++ >= p._rightBranch.Length-1)
                        it = 0;
                    p = p._rightBranch[it];
                    doOnce = false;
                }
                else
                    p = p._rightBranch[0];
                if (p != null)
                    p.ChangeStateTo(1);
            }
        }
        else
        {
            while (p._leftBranch[0] != null)
            {
                if (doOnce)
                {
                    if (it++ >= p._leftBranch.Length-1)
                        it = 0;
                    p = p._leftBranch[it];
                    doOnce = false;
                }
                else
                    p = p._leftBranch[0];
                if(p!=null)
                    p.ChangeStateTo(1);
            }
        }
        print("changed path");
    }

    public void ResetPath()
    {
        List<SinglePoint> allpoints = new List<SinglePoint>();

        //Reset all SinglePointState
        for (int i = 0; i < Level.transform.childCount; i++)
        {
            allpoints.Add(Level.transform.GetChild(i).GetComponent<SinglePoint>());
            allpoints[i].ChangeStateTo(0);
        }
        print("reset modafuka");
    }

    public void ChangePath(SinglePoint position)
    {
        print("changing...");
        int indexAtLeft = GetNextIndexAtDirection(position, Direction.Left);
        int indexAtRight = GetNextIndexAtDirection(position, Direction.Right);
        print("indexAtLeft : " + indexAtLeft + "- indexAtRight : " + indexAtRight);
        ResetPath();
        position.ChangeStateTo(1);
        SetupPathByDirection(position, Direction.Left, indexAtLeft);
        SetupPathByDirection(position, Direction.Right, indexAtRight);
    }

    int GetNextIndexAtDirection(SinglePoint p, Direction d)
    {
        SinglePoint[] la = d==Direction.Left ?p._leftBranch : p._rightBranch;
        for (int i =0; i<la.Length; i++)
        {
            if (la[i]._isActive && la[i] !=null)
            {
                return i;
            } 
        }
        Debug.LogError("Didn't find any active cell next door...");
        return -1;
    }
}

public enum Direction
{
    Left,
    Right
};


