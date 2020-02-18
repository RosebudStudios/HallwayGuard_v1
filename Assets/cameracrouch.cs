using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracrouch : MonoBehaviour
{
    public bool crouch ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position += -Vector3.up * 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.position += Vector3.up * 2;
        }
    }
}
