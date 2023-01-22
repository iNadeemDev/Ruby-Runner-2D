using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw1Patrolling : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool isRight = true;
    private float speed = 0.008f;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;


    // Start is called before the first frame update
    void Start()
    {
        pointAPosition = new Vector3(pointA.position.x, 0, 0);
        pointBPosition = new Vector3(pointB.position.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
        Vector3 thisPosition = new Vector3(transform.position.x, 0, 0);
        if (isRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed);
            if (thisPosition.Equals(pointBPosition))
            {
                //Debug.Log ("Position b");
                isRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed);
            if (thisPosition.Equals(pointAPosition))
            {
                //Debug.Log ("Position a");
                isRight = true;
            }
        }
    }

    private void OnBecameVisible()
    {
        AudioManager.instance.PlayMySound("Saw");
    }

    private void OnBecameInvisible()
    {
        AudioManager.instance.StopMySound("Saw");
    }
}
