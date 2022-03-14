using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Direction _playerDirection = Direction.Right;
    private bool _isDead = false;
    private bool _hasWin = false;
    SinglePoint _currentPointOn;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            print("PointsManager Singleton Already Existing");
            return;
        }
        Instance = this;
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
        //print(GameManager.instance.name);
        #region Check Loose Condition
        if (_isDead)
        {
            GameManager.Instance.Loose();
        }
        #endregion

        #region Check Win Condition

        try
        {
            if (_currentPointOn == (PointsManager.instance.endPoint && !_hasWin))
            {
                print("eyo wtf");
                _hasWin = true;
                GameManager.Instance.Win();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        #endregion

        #region inputs

        if (!GameManager.Instance.camMove) return;
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer(Direction.Right);
        else if (Input.GetKeyDown(KeyCode.Q)) MovePlayer(Direction.Left);
        else if (Input.GetKeyDown(KeyCode.Space)) ChangePath();//ChangePath(dir);

        #endregion

        #region debug 
        //print(_playerDirection);
        #endregion
            
    }

    private void MovePlayer(Direction playerDirection)
    {
        
        _playerDirection = playerDirection;
        var ListToLookAt = playerDirection == Direction.Left ? _currentPointOn.leftBranch : _currentPointOn.rightBranch;
        foreach (var t in ListToLookAt)
        {
            if(!t || !t.isActive) continue;
            _currentPointOn = t;
            transform.position = _currentPointOn.transform.position;
            break;
        }
        Camera.main.GetComponent<FollowPlayer>().FollowTheFuckingPlayer(gameObject);
        //print("cannot move in this direction");
    }
    private void ChangePath()
    {
        //print("ESPACE");
        PointsManager.instance.ChangePath(_currentPointOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Ennemy") return;
        print("touched ennemy bruh");
        _isDead = true;
    }

}
