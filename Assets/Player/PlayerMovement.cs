using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float weaponReach = 1;
    ThirdPersonCharacter character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectMode = false;   //TODO consider making static l8r
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;

    }
    private void Update()
    {
        //TODO add buttonBinding later
        if (Input.GetKeyDown(KeyCode.G)) //Switches between classic Diablo-style movement and Wasd controls
        {
            isInDirectMode = !isInDirectMode;
            print("DirectMode is: " + isInDirectMode);
            currentDestination = this.transform.position;
        }
    }
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }
    void ProcessDirectMovement() //Wasd-style movement
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = vert * camForward + hori * Camera.main.transform.right;

        character.Move(movement, false, false);
    }
    void ProcessMouseMovement() //Handles mouseMovement and other input - Diablo-movement
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            //print("Cursor raycast hit " + cameraRaycaster.hit.collider.gameObject.name.ToString());
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    clickPoint.Set(clickPoint.x, 0,clickPoint.z); //TODO: Make a better fix that works on different elevations
                    currentDestination = ShortDestination(clickPoint, weaponReach);
                    break;
                default:
                    print("WHY OH WHY!?");
                    break;
            }
        }
        WalkToDestination();
    }

    private void WalkToDestination() 
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (Vector3.Magnitude(playerToClickPoint) >= 0.05f)
            character.Move(playerToClickPoint, false, false); //TODO: Create my own character move-script
        else
            character.Move(Vector3.zero, false, false);
    }
    Vector3 ShortDestination(Vector3 destination, float shortening) //
    {
        if (Vector3.Magnitude(destination - transform.position) >= shortening)
        {
            Vector3 reductionVector = (destination - transform.position).normalized * shortening;
            return destination - reductionVector;
        }
        else
        {
            return transform.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination,0.1f);
        Gizmos.DrawSphere(clickPoint,0.15f);

        Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, weaponReach);
    }

}