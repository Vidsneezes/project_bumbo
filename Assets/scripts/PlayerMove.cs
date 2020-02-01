using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed;

    public Vector3 velocity;

    public GameObject mainCameraPolly;

    public List<GameObject> detectedObjects;

    public GameObject exclamationMark;

    public GameObject dialogueUI;

    public TextMeshProUGUI message;
    public GameObject rootObject;
    public Animator animator;

    public GameObject elevatorUpProp;
    public Camera awayCamera;

    const float flyawayLimit = 10.6f;

    private bool talking;
    private bool flyingAway;

    // Start is called before the first frame update
    void Start()
    {
        flyingAway = false;
        talking = false;
        exclamationMark.SetActive(false);
        elevatorUpProp = GameObject.FindGameObjectWithTag("elevatorUpProp");
        awayCamera = GameObject.FindGameObjectWithTag("awaycam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if(flyingAway)
        {
            characterController.enabled = false;

            velocity.x = 0;
            velocity.y = 0;
            if(transform.parent == null)
            {
                transform.SetParent(elevatorUpProp.transform);
            }
            transform.localPosition = Vector3.up * 0.5f;
            animator.SetInteger("rolling", -1);

            Vector3 moveUp = elevatorUpProp.transform.position;
            moveUp.y += 4 * Time.deltaTime;
            elevatorUpProp.transform.position = moveUp;
            if(moveUp.y > flyawayLimit)
            {
                Debug.Log("reached Level");
            }

            return;
        }

        bool talk = Input.GetKeyDown(KeyCode.Return);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float foward = Input.GetAxis("Vertical");



        if (!talking)
        {
            velocity.x = moveHorizontal;
            velocity.z = foward;

            if (velocity.magnitude > 0)
            {
                animator.SetInteger("rolling", 1);
            }
            else
            {
                animator.SetInteger("rolling", -1);
            }

            if (detectedObjects.Count > 0)
            {
                exclamationMark.SetActive(true);
            }
            else
            {
                exclamationMark.SetActive(false);
            }

            if (talk && detectedObjects.Count > 0)
            {
                dialogueUI.gameObject.SetActive(true);
                message.text = "Hello friend";
                talking = true;
            }
        }
        else
        {

            animator.SetInteger("rolling", -1);
            if (talk)
            {
                talking = false;
                dialogueUI.gameObject.SetActive(false);
                message.text = "Hello friend";
            }
        }




    }

    private void FixedUpdate()
    {
        if (!flyingAway)
        {
            characterController.Move(velocity * Time.fixedDeltaTime * moveSpeed);
        }
    }

    private void LateUpdate()
    {
        mainCameraPolly.transform.position = transform.position;
        Vector3 localFoward = transform.InverseTransformDirection(velocity.normalized);
        if (localFoward.magnitude > 0)
        {
            rootObject.transform.forward = localFoward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("elevator") && flyingAway == false)
        {
            Debug.Log("load level");
            flyingAway = true;
            awayCamera.depth = 1;
        }

        if(!detectedObjects.Contains(other.gameObject) && other.CompareTag("event"))
        {
            detectedObjects.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (detectedObjects.Contains(other.gameObject) && other.CompareTag("event"))
        {
            detectedObjects.Remove(other.gameObject);
        }
    }
  
}
