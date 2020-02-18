using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileshooter : MonoBehaviour
{
    public GameObject prefab;
    public Transform empspawn;

    public GameObject gc;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {


        if (gc.GetComponent<GameController>().disrupter > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject projectile = Instantiate(prefab) as GameObject;
                projectile.transform.position = empspawn.position;
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * 40;
                gc.GetComponent<GameController>().disrupter--;
            }
        }
    }
}
