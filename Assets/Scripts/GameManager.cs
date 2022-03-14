using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject winPannel, losePannel;
    public bool camMove = true;

    private void Awake()
    {
        if (Instance != null)
        {
            print("GameManager Singleton Already Existing");
            return;
        }
        Instance = this;
    }

    public int scoreValue = 0;
    public bool isDead = false;
    public bool canPlayerMove = true;

    [SerializeField] Text _score;

    public void Win()
    {
        print("you won");
        //_WinPannel.SetActive(true);
        
        if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1)!= null)//move a 
            StartCoroutine(WLDelay(SceneManager.GetActiveScene().buildIndex + 1, 1));
        else
        {
            //show thanks for playing
        }
        
    }

    public void Loose()
    {
        //_LosePannel.SetActive(true);
        print("you lost");
        StartCoroutine(WLDelay(SceneManager.GetActiveScene().buildIndex, 0.2f));

    }

    public void AddScore(int score)
    {
        scoreValue = score;
        _score.text = scoreValue.ToString();
    }

    IEnumerator WLDelay(int a, float time)
    {
        camMove = false;
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(a);
    }


}
