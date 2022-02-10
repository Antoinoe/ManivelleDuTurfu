using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            print("GameManager Singleton Already Existing");
            return;
        }
        instance = this;
    }

    public int scoreValue = 0;
    public int nbOfEnnemyKilled = 0;
    public bool isDead = false;
    public bool canPlayerMove = true;
    public bool isShooting = false;

    [SerializeField] Text _score;

    public void Win()
    {

    }

    public void Loose()
    {

    }

    public void AddScore(int score)
    {
        scoreValue = score;
        _score.text = scoreValue.ToString();
    }

}
