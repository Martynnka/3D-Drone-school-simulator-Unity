using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RealDroneController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Forward")]
    public float maxForwardSpeed = 14f;
    public float acceleration = 3f;
    public float brakePower = 1.5f;   // mažesnė = ilgiau rieda

    [Header("Up / Down")]
    public float liftSpeed = 5f;
    public float liftSmooth = 3f;

    [Header("Pitch / Loop")]
    public float pitchSpeed = 90f;
    public float pitchSmooth = 6f;

    [Header("Yaw")]
    public float yawSpeed = 120f;
    public float yawSmooth = 6f;

    [Header("Side Yaw")]
    public float sideYawSmooth = 6f;

    [Header("Roll (turn feel)")]
    public float maxYawTilt = 45f;
    public float tiltSmooth = 12f;

    [Header("Visual")]
    public Transform visual;

    float currentSpeed;
    float currentLift;
    float currentYaw;
    float currentSideYaw;
    float currentPitch;
    float currentRoll;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.drag = 0f;
        rb.angularDrag = 0f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        // ===== INPUT (GetKey is OK) =====
        bool accelerate = Input.GetKey(KeyCode.W);

        float yawInput = 0f;
        if (Input.GetKey(KeyCode.Q)) yawInput = -1f;
        if (Input.GetKey(KeyCode.E)) yawInput = 1f;

        float sideInput = 0f;
        if (Input.GetKey(KeyCode.A)) sideInput = -1f;
        if (Input.GetKey(KeyCode.D)) sideInput = 1f;

        float pitchInput = 0f;
        if (Input.GetKey(KeyCode.Space)) pitchInput = 1f;
        if (Input.GetKey(KeyCode.LeftShift)) pitchInput = -1f;

        float upDown = 0f;
        if (Input.GetKey(KeyCode.Z)) upDown = 1f;
        if (Input.GetKey(KeyCode.X)) upDown = -1f;

        // ===== FORWARD INERTIA (W) =====
        float targetSpeed = accelerate ? maxForwardSpeed : 0.2f;
        float accelRate = accelerate ? acceleration : brakePower;

        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            accelRate * Time.fixedDeltaTime
        );

        // ===== UP / DOWN INERTIA (Z / X) =====
        currentLift = Mathf.Lerp(
            currentLift,
            upDown * liftSpeed,
            liftSmooth * Time.fixedDeltaTime
        );

        // ===== FINAL MOVEMENT =====
        Vector3 forwardVelocity = transform.forward * currentSpeed;
        Vector3 verticalVelocity = Vector3.up * currentLift;

        rb.velocity = forwardVelocity + verticalVelocity;

        // ===== YAW INERTIA (Q / E) =====
        currentYaw = Mathf.Lerp(
            currentYaw,
            yawInput * yawSpeed,
            yawSmooth * Time.fixedDeltaTime
        );

        transform.Rotate(
            Vector3.up,
            currentYaw * Time.fixedDeltaTime,
            Space.World
        );

        // ===== SIDE TURN INERTIA (A / D) =====
        currentSideYaw = Mathf.Lerp(
            currentSideYaw,
            sideInput * yawSpeed,
            sideYawSmooth * Time.fixedDeltaTime
        );

        transform.Rotate(
            Vector3.up,
            currentSideYaw * Time.fixedDeltaTime,
            Space.World
        );

        // ===== PITCH INERTIA =====
        currentPitch = Mathf.Lerp(
            currentPitch,
            pitchInput * pitchSpeed,
            pitchSmooth * Time.fixedDeltaTime
        );

        transform.Rotate(
            Vector3.right,
            -currentPitch * Time.fixedDeltaTime,
            Space.Self
        );

        // ===== ROLL (VISUAL ONLY, SMOOTH) =====
        float targetRoll = -yawInput * maxYawTilt;

        currentRoll = Mathf.Lerp(
            currentRoll,
            targetRoll,
            tiltSmooth * Time.fixedDeltaTime
        );

        if (visual != null)
        {
            visual.localRotation = Quaternion.Euler(0f, 0f, currentRoll);
        }
    }
}
