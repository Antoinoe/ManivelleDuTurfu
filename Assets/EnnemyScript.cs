using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    [SerializeField] GameObject boundary1, boundary2;
    [SerializeField] float speed;
    public bool canMove = true;

    Vector2 _directionHeading;
    Rigidbody2D _rb;
    GameObject _currentDestination,_previousBoundaryTouched;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //transform.position = boundary1.transform.position;
        _previousBoundaryTouched = boundary1;
        SetTrajectory(boundary2);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            _rb.velocity = _directionHeading * speed* Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Boundary")
        {
            print("changement de sens");
            //change de sens
            if (_currentDestination == boundary1)
            {
                SetTrajectory(boundary1);
            }
            else
            {
                SetTrajectory(boundary2);
            }
        }
    }

    void SetTrajectory(GameObject destination)
    {
        //StartCoroutine(Delay(_previousBoundaryTouched, 1.5f));
        _directionHeading = (destination.transform.position - transform.position).normalized;
        print(_directionHeading);
        if(destination == boundary1)
        {
            _currentDestination = boundary2;
            _previousBoundaryTouched = boundary1;
        }
        else
        {
            _currentDestination = boundary1;
            _previousBoundaryTouched = boundary2;
        }
    }

    IEnumerator Delay(GameObject obj, float time)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }
}
