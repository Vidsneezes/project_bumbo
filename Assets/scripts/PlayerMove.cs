using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed;

    public Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float foward = Input.GetAxis("Vertical");

        velocity.x = moveHorizontal;
        velocity.z = foward;
        
    }

    private void FixedUpdate()
    {
        characterController.Move(velocity * Time.fixedDeltaTime * moveSpeed);    
    }
}
