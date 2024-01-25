using UnityEngine;

public class Car : MonoBehaviour
{
    public Player belongsTo;
    
    public float speed = 5f; // The speed of the movement
    public float acceleration = 10f; // The acceleration factor
    public float deceleration = 20f; // The deceleration factor
    public float maxSpeed = 10f; // The maximum speed

    private Rigidbody _rb;
    private Vector3 _currentVelocity = Vector3.zero;

    private bool _accelerate;
    private bool _decelerate;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        switch (belongsTo)
        {
            case Player.PlayerOne:
                HandlePlayerInput("P1_Left", "P1_Right", "P1_Back", "P1_Forward", out horizontal, out vertical);
                break;

            case Player.PlayerTwo:
                HandlePlayerInput("P2_Left", "P2_Right", "P2_Back", "P2_Forward", out horizontal, out vertical);
                break;

            default:
                horizontal = 0f;
                vertical = 0f;
                break;
        }

        // Store horizontal and vertical for Gizmos
        _lastHorizontal = horizontal;
        _lastVertical = vertical;

        // Check if the "Up" key is pressed to start acceleration
        if (vertical > 0f)
        {
            Accelerate();
        }

        // Check if the "Back" key is pressed to start deceleration
        if (vertical < 0f)
        {
            Decelerate();
        }

        // Calculate the target position based on input
        Vector3 targetPosition = transform.position + new Vector3(horizontal, 0f, vertical);

        // Call the Seek function with acceleration, deceleration, and max speed
        Seek(targetPosition);
    }

    private float _lastHorizontal;
    private float _lastVertical;

    private void OnDrawGizmos()
    {
        // Draw Gizmos for current velocity, desired velocity, and steering force
        Gizmos.color = Color.blue; // Desired velocity in blue
        Gizmos.DrawLine(transform.position, transform.position + _currentVelocity);

        Gizmos.color = Color.green; // Current velocity in green
        Gizmos.DrawLine(transform.position, transform.position + _rb.velocity);

        Gizmos.color = Color.red; // Steering force in red
        Vector3 targetPosition = transform.position + new Vector3(_lastHorizontal, 0f, _lastVertical);
        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * speed;
        Vector3 steeringForce = desiredVelocity - _rb.velocity;
        Gizmos.DrawLine(transform.position, transform.position + steeringForce);
    }

    // Helper method to handle player input
    void HandlePlayerInput(string leftKey, string rightKey, string backKey, string forwardKey, out float horizontalResult, out float verticalResult)
    {
        horizontalResult = Input.GetKey(GameManager.Instance.GetKey(leftKey)) ? -1f : Input.GetKey(GameManager.Instance.GetKey(rightKey)) ? 1f : 0f;

        // Check if the "Up" key is pressed and start acceleration
        if (Input.GetKey(GameManager.Instance.GetKey(forwardKey)))
        {
            _accelerate = true;
            _decelerate = false; // Reset deceleration when accelerating
        }

        // Check if the "Back" key is pressed and start deceleration
        if (Input.GetKey(GameManager.Instance.GetKey(backKey)))
        {
            _decelerate = true;
            _accelerate = false; // Reset acceleration when decelerating
        }

        // If accelerating, set verticalResult to 1
        // If decelerating, set verticalResult to -1
        // If neither, set verticalResult to 0
        verticalResult = _accelerate ? 1f : _decelerate ? -1f : 0f;
    }

    // Method to handle acceleration
    void Accelerate()
    {
        _accelerate = true;
        _decelerate = false; // Reset deceleration when accelerating
    }

    // Method to handle deceleration
    void Decelerate()
    {
        _decelerate = true;
        _accelerate = false; // Reset acceleration when decelerating
    }

    private void Seek(Vector3 targetPosition)
    {
        // Calculate the desired velocity
        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * speed;

        // Calculate the acceleration
        Vector3 accelerationForce = (desiredVelocity - _currentVelocity) * (_accelerate ? acceleration : _decelerate ? deceleration : 0f);

        // Update the current velocity with acceleration
        _currentVelocity += accelerationForce * Time.deltaTime;

        // Clamp the magnitude of the velocity to the maximum speed
        _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, maxSpeed);

        // Calculate the steering force
        Vector3 steeringForce = _currentVelocity - _rb.velocity;

        // Apply the steering force to the Rigidbody
        _rb.AddForce(steeringForce);
    }
}
