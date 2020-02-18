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
    private int stuncounter;

    int currentStartPoint;
    int currentEndPoint;

    public LayerMask RayCastLayers;

    public GameObject gc;

    public Color c1 = Color.red;
    public Color c2 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        currentStartPoint = 0;
        currentEndPoint = 1;
        dronestate = 0;
        alertcounter = 0;
        recovercounter = 0;
        stuncounter = 0;
        lt.color = Color.green;
        alarm.intensity = 0;

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

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
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        //Scout Mode
        if (dronestate == 0)
        {
            lt.color = Color.green;
            lt.intensity = 7f;
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
            lt.intensity = 9f;
            alarm.intensity = Mathf.Lerp(0,1,2);

            RaycastHit hit;

            //If the sentry cannot see the player
            if (Physics.Linecast(transform.position, player.position, out hit, RayCastLayers))
            {
                
                recovercounter++;
                alertcounter--;

                if (recovercounter >= 150)
                {
                    recovercounter = 0;
                    dronestate = 0;
                }

            }

            //If the sentry sees the player
            else
            {
                recovercounter = 0;
                alertcounter++;
                if (alertcounter >= 120)
                {
                    alertcounter = 0;
                    gc.GetComponent<GameController>().health--;
                }


                lineRenderer.SetPosition(0,transform.position);
                lineRenderer.SetPosition(1, player.position);


            }

        }

        if (dronestate == 2)
        {
            lt.color = Color.yellow;
            lt.intensity = 0.5f;
            stuncounter++;
            if (stuncounter >= 300)
            {
                stuncounter = 0;
                dronestate = 0;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (dronestate == 0)
        {
            if (other.tag == "Player")
            {
                dronestate = 1;
            }
        }
    }
}
