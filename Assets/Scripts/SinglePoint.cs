using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePoint : MonoBehaviour
{
    [SerializeField] public SinglePoint[] _leftBranch, _rightBranch; // all possible way
    [SerializeField] private Sprite[] _sprites; //[1]is on [0]is off
    [SerializeField] private SpriteRenderer _sr;
    public bool _isActive = false;

    void Start()
    {
        _sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        //name = _leftBranch.Length > 1 || _rightBranch.Length > 1 ? "BranchPoint" : "Point";

    }

    public void ChangeStateTo(int state)
    {
        _sr.sprite = state == 1 ? _sr.sprite = _sprites[1] : _sr.sprite = _sprites[0];
        _isActive = state == 1 ? true : false;
    }


}
