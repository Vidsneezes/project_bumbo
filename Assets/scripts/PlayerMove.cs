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

    public GameObject rootObject;
    public Animator animator;

    public GameObject elevatorUpProp;
    public Camera awayCamera;

    const float flyawayLimit = 10.6f;
    Vector3 fixedCamViewStart = new Vector3(12.3f, -89.8f, 0);
    Vector3 fixedCamViewEnd = new Vector3(-41, -89.8f, 0);


    private bool talking;
    private bool flyingAway;
    float timer = 0;

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
            timer += Time.deltaTime * 0.89f;
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

            if (timer < 1)
            {
                awayCamera.transform.rotation = Quaternion.Euler(Vector3.Lerp(fixedCamViewStart, fixedCamViewEnd, timer));
            }
            return;
        }

        bool talkButton = Input.GetKeyDown(KeyCode.Return);
        bool wordTakeButton = Input.GetKeyDown(KeyCode.K);
        bool useWordButton = Input.GetKeyDown(KeyCode.J);
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

            if (talkButton && detectedObjects.Count > 0)
            {
                DialogueManager.instance.LoadDialogue(Blurbber.SimpleBlurb());
                talking = true;
            }
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;

            animator.SetInteger("rolling", -1);
            if (talkButton)
            {
                if (DialogueManager.instance.AtEndOfBlurb())
                {
                    CloseDialogue();
                }
                else
                {
                    DialogueManager.instance.NextMessage();
                }
            }

            if(wordTakeButton)
            {
                DialogueManager.instance.AddCurrentWord("Hello");
                CloseDialogue();
            }

            if(DialogueManager.instance.hasCurrentWord && useWordButton)
            {
                DialogueManager.instance.UseWord();
            }
        }

    }

    void CloseDialogue()
    {
        DialogueManager.instance.CloseDialogue();
        talking = false;
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
            timer = 0;
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
