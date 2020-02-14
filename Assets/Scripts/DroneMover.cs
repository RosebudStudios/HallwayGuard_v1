using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMover : MonoBehaviour
{
    private Transform startmarker, endmarker;
    public Transform[] waypoints;

    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    int currentStartPoint;
    int currentEndPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentStartPoint = 0;
        currentEndPoint = 1;
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
        float distcovered = (Time.time - startTime) * speed;
        float fracJourney = distcovered / journeyLength;
        transform.position = Vector3.Lerp(startmarker.position, endmarker.position, fracJourney);
        if (fracJourney >= 1f)
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
}
