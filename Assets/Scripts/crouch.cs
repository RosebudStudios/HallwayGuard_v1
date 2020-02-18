using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour
{
    public GameObject player;
    public GameObject capsule;
    CharacterController characterCollider;
    CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = player.GetComponent<CharacterController>();
        collider = capsule.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
           // characterCollider.radius = 1f;
            characterCollider.height = 1.5f;
            collider.height = 0.5f;
           
        }
        else
        {
            //characterCollider.radius = 2.3f;
            characterCollider.height = 3.5f;
            collider.height = 2f;
        }
    }
}
