using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Direction _playerDirection = Direction.Right;
    bool _isDead = false;
    bool _hasWin = false;
    SinglePoint _currentPointOn;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            print("PointsManager Singleton Already Existing");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        #region teleport player to first cell
        _currentPointOn = PointsManager.instance.startPoint;
        transform.position = _currentPointOn.transform.position;
        #endregion
    }
    private void Update()
    {
        #region Check Loose Condition
        if (_isDead)
        {
            //loose screen
        }
        #endregion

        #region Check Win Condition
        if (_currentPointOn == PointsManager.instance.endPoint && !_hasWin)
        {
            _hasWin = true;
            //win screen/next level
        }
        #endregion

        #region inputs
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer(Direction.Right);
        else if (Input.GetKeyDown(KeyCode.Q)) MovePlayer(Direction.Left);
        else if (Input.GetKeyDown(KeyCode.Space)) ChangePath();//ChangePath(dir);
        #endregion

        #region debug 
        //print(_playerDirection);
        #endregion
            
    }

    void MovePlayer(Direction playerDirection)
    {
        _playerDirection = playerDirection;
        SinglePoint[] ListToLookAt = playerDirection == Direction.Left ? _currentPointOn._leftBranch : _currentPointOn._rightBranch;
        for(int i =0; i<ListToLookAt.Length; i++)
        {
            if (ListToLookAt[i]._isActive && ListToLookAt[i]!=null)
            {
                _currentPointOn = ListToLookAt[i];
                transform.position = _currentPointOn.transform.position;
                break;
            }
        }
        //print("cannot move in this direction");
    }
    void ChangePath()
    {
        //print("ESPACE");
        PointsManager.instance.ChangePath(_currentPointOn);
    }

}
