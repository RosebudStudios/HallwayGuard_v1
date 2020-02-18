using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour
{
    public GameObject player;
    CharacterController characterCollider;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterCollider.height = 1.5f;
           
        }
        else
        {
            characterCollider.height = 2.3f;
        }
    }
}
