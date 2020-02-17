using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMover : MonoBehaviour
{
    public int dronestate;

    private Transform startmarker, endmarker;
    public Transform[] waypoints;

    public Transform player;

    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    public Light lt;
    public Light alarm;

    private int alertcounter;
    private int recovercounter;

    int currentStartPoint;
    int currentEndPoint;

    public LayerMask RayCastLayers;
    
    // Start is called before the first frame update
    void Start()
    {
        currentStartPoint = 0;
        currentEndPoint = 1;
        dronestate = 0;
        alertcounter = 0;
        recovercounter = 0;
        lt.color = Color.green;
        alarm.intensity = 0;
        SetPoints();
    }

    void SetPoints()
    {
        startmarker = waypoints[currentStartPoint];
        endmarker = waypoints[currentEndPoint];

        startTime = Time.time;
        journeyLength = Vector3.Distance(startmarker.position, endmarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Scout Mode
        if (dronestate == 0)
        {
            lt.color = Color.green;
            alarm.intensity = 0;

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endmarker.position, step);
            if (Vector3.Distance(transform.position, endmarker.position) < 0.001f)
            {
                currentStartPoint++;
                currentEndPoint++;

                if (currentStartPoint == waypoints.Length)
                {
                    currentStartPoint = 0;
                }

                if (currentEndPoint == waypoints.Length)
                {
                    currentEndPoint = 0;
                }

                SetPoints();
            }
        }

        //Alert Mode
        if (dronestate == 1)
        {
            lt.color = Color.red;
            alarm.intensity = Mathf.Lerp(0,1,2);

            RaycastHit hit;

            //If the sentry cannot see the player
            if (Physics.Linecast(transform.position, player.position, out hit, RayCastLayers))
            {
                recovercounter++;

                if (recovercounter >= 150)
                {
                    recovercounter = 0;
                    dronestate = 0;
                }
            }

            //If the sentry sees the player
            else
            {

            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dronestate = 1;
        }
    }
}
