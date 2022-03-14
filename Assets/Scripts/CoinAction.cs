using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAction : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Color color;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        print("touched played bruh");
        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
