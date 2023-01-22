using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject player;
    float distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector3.Distance(player.transform.position, transform.position);

        //if(distance < 6)
        //{
        //    //AudioManager.instance.StopMySound("FootStep");
        //    //AudioManager.instance.StopAllSounds();
        //    AudioManager.instance.PlayMySound("FootStep");
        //}
        //else if(distance>6 && AudioManager.instance.IsPlayingMySound("Water"))
        //{
        //    AudioManager.instance.StopMySound("Water");
        //}


    }
}