using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Dir
{
    droite,
    haut,
    bas
}
public class Manivelle : MonoBehaviour
{
    public Dir dir = Dir.haut;
    public static Manivelle instance;
    public int vitesse = 0;
    public int maxVitesse = 30;
    public SpriteRenderer circle;
    public float vitesseChute = 0.5f; 
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InvokeRepeating(nameof(ReduceVit), 0, vitesseChute);
    }
    private void Update()
    {
        Move();
        ChangeAlpha();
    }
    private void Move()
    {
        if (Input.GetKeyDown("z")&&dir == Dir.droite)
        {
            vitesse++;
            dir = Dir.haut;
            print("haut");
        }
        else if (Input.GetKeyDown("s") && dir == Dir.haut)
        {
            vitesse++;
            dir = Dir.bas;
            print("haut");

        }
        else if (Input.GetKeyDown("q") && dir == Dir.bas)
        {
            vitesse++;
            dir = Dir.droite;
            print("bas");

            // PlayerController.instance.MovePlayer(Direction.Right);
        }
        if (vitesse > maxVitesse) vitesse = maxVitesse;
        //Debug.Log(vitesse);
        //move a droite
        
        
    }

    private void ChangeAlpha()
    {
        if (!circle) return;
        var tempColor = circle.color;
        var x= (float)vitesse / (float)maxVitesse;
        tempColor.g = x;
        tempColor.r = x;
        tempColor.b = x;
        circle.color = tempColor;
    }
    void ReduceVit()
    {
        vitesse = vitesse > 0 ? vitesse - 1 : 0;
        Debug.Log(vitesse.ToString());
    }
}
