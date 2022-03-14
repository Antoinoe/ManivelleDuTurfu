using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePoint : MonoBehaviour
{
    [SerializeField] public SinglePoint[] leftBranch, rightBranch; // all possible way
    [SerializeField] private Sprite[] sprites; //[1]is on [0]is off
    [SerializeField] private SpriteRenderer sr;
    public bool isActive = false;

    private void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        //name = _leftBranch.Length > 1 || _rightBranch.Length > 1 ? "BranchPoint" : "Point";

    }

    public void ChangeStateTo(int state)
    {
        sr.sprite = state == 1 ? sr.sprite = sprites[1] : sr.sprite = sprites[0];
        isActive = state == 1 ? true : false;
    }


}
