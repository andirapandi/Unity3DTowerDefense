using UnityEngine;
using System.Collections;

public abstract class BaseMotor : MonoBehaviour
{

    protected CharacterController controller;
    protected BaseState state;
    protected Transform thisTransform;

    float baseSpeed = 5f;
    float baseGravity = 25f;
    private float terminalVelocity = 30f;
    private float groundRayDistance = .5f;
    float groundRayInnerOffset = .1f;

    public float Speed { get { return baseSpeed; } }
    public float Gravity { get { return baseGravity; } }
    public float TerminalVelocity { get { return terminalVelocity; } }
    public float VerticalVelocity { get; set; }
    public Vector3 MoveVector { get; set; }

    protected abstract void UpdateMotor();

    protected virtual void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        thisTransform = transform;
        state = gameObject.AddComponent<WalkingState>();
        state.Construct();
    }

    private void Update()
    {
        UpdateMotor();
    }

    protected virtual void Move()
    {
        controller.Move(MoveVector * Time.deltaTime);
    }

    public void ChangeState(string stateName)
    {
        System.Type t = System.Type.GetType(stateName);

        state.Destruct();
        state = gameObject.AddComponent(t) as BaseState;
        state.Construct();
    }

    public virtual bool CheckGrounded()
    {
        RaycastHit hit;
        Vector3 ray;

        float yRay = (controller.bounds.center.y - controller.bounds.extents.y) + .3f, // + security margin in case object slightly below floor
            centerX = controller.bounds.center.x - groundRayInnerOffset,
            centerZ = controller.bounds.center.z - groundRayInnerOffset,
            extentX = controller.bounds.extents.x - groundRayInnerOffset,
            extentZ = controller.bounds.extents.z - groundRayInnerOffset;


        // Middle Raycast
        ray = new Vector3(centerX, yRay, centerZ);
        Debug.DrawRay(ray, Vector3.down /* * 3*/, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX + extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down /* * 3*/, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX - extentX, yRay, centerZ + extentZ);
        Debug.DrawRay(ray, Vector3.down /* * 3*/, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX + extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down /* * 3*/, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }
        ray = new Vector3(centerX - extentX, yRay, centerZ - extentZ);
        Debug.DrawRay(ray, Vector3.down /* * 3*/, Color.green);
        if (Physics.Raycast(ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        return false;
    }
}
