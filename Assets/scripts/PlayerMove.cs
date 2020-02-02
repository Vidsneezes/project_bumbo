using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public NavMeshAgent navMesh;
    public float moveSpeed;
    public NavMeshSurface navMeshSurface;

    public Vector3 velocity;

    public GameObject mainCameraPolly;

    public List<GameObject> detectedObjects;

    public GameObject exclamationMark;

    public GameObject rootObject;
    public Animator animator;

    public GameObject elevatorUpProp;
    public Camera awayCamera;

    public MeshRenderer orbRenderer;

    public MeshRenderer headRenderer;

    public Npc headNpc;
    public Npc prideNpc;
    public Npc slothNpc;

    public GameObject prideArm_1;
    public GameObject prideArm_2;

    public GameObject slothLeg_1;
    public GameObject slothLeg_2;

    public GameObject lockedDoor_1;
    public GameObject lockedDoor_2;


    const float flyawayLimit = 10.6f;
    Vector3 fixedCamViewStart = new Vector3(12.3f, 5.64f, 0);
    Vector3 fixedWalkAngle = new Vector3(0.7f, 0.7f, 0);

    Vector3 fixedCamViewEnd = new Vector3(-41, 5.64f, 0);

    public bool hasArms;
    public bool hasLegs;
    public bool openDoor_1;
    public bool openDoor_2;


    private Npc currentNpc;

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

        hasArms = false;
        hasLegs = false;

        openDoor_1 = false;
    }

    // Update is called once per frame
    void Update()
    {


        if(flyingAway)
        {
            timer += Time.deltaTime * 0.89f;
            characterController.enabled = false;

            velocity.x = 0;
            velocity.z = 0;
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

                Npc npc = detectedObjects[0].GetComponent<Npc>();
                if(npc != null)
                {
                    exclamationMark.SetActive(false);
                    currentNpc = npc;
                    currentNpc.ShowCamera();
                    DialogueManager.instance.LoadDialogue(npc.GetId());
                    talking = true;
                }

            }
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;

            animator.SetInteger("rolling", -1);
            if (talkButton)
            {
                if (DialogueManager.instance.AtEndOfBlurb())
                {
                    currentNpc.HideCamera();
                    currentNpc = null;
                    CloseDialogue();
                }
                else
                {
                    DialogueManager.instance.NextMessage();
                }
            }

            if(wordTakeButton && DialogueManager.instance.hasCurrentWord == false)
            {

                DialogueManager.instance.AddCurrentWord();

                currentNpc.HideCamera();
                currentNpc = null;
                CloseDialogue();
            }

            if(DialogueManager.instance.hasCurrentWord && useWordButton)
            {
                if (DialogueManager.instance.UseWord())
                {
                    if (currentNpc == headNpc)
                    {
                        headNpc.gameObject.SetActive(false);
                        GetHead();
                    }else if(currentNpc == prideNpc)
                    {
                        GetArms();
                    }else if(currentNpc == slothNpc)
                    {
                        GetLegs();
                    }
                }
            }
        }

        navMesh.Move(velocity * Time.deltaTime * moveSpeed);
    }

    void CloseDialogue()
    {
        DialogueManager.instance.CloseDialogue();
        talking = false;
        exclamationMark.SetActive(false);
        detectedObjects.Clear();
    }

    public void GetArms()
    {
        prideArm_1.gameObject.SetActive(false);
        prideArm_2.gameObject.SetActive(false);
        hasArms = true;
    }

    public void GetLegs()
    {
        slothLeg_1.gameObject.SetActive(false);
        slothLeg_2.gameObject.SetActive(false);
        hasLegs = true;
    }

    public void GetHead()
    {
        orbRenderer.enabled = false;
        headRenderer.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        //characterController.Move(velocity * Time.fixedDeltaTime * moveSpeed);
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

        if(other.CompareTag("lockeddoor_1") && hasArms && openDoor_1 == false)
        {
            openDoor_1 = true;
            lockedDoor_1.gameObject.SetActive(false);
        }

        if(other.CompareTag("lockeddoor_2") && hasLegs && openDoor_2 == false)
        {
            openDoor_2 = true;
            lockedDoor_2.gameObject.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!detectedObjects.Contains(other.gameObject) && other.CompareTag("event"))
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
