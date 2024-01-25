using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Player { PlayerOne, PlayerTwo }

public class FlyingCarMovement : MonoBehaviour
{
    public float forwardPower;
    public float horizontalPower;

    public List<GameObject> springs;

    public GameObject centerOfMass;
    public GameObject propeller;

    public bool canMove;
    //public bool ignoreCollision;

    public Player belongsTo;

    private int _playerLayer;
    private int _obstacleLayer;


    public Rigidbody rb;

    [SerializeField] private LayerMask ground;

    private UIGame _ui;

    void HandlePlayerInput(string leftKey, string rightKey, string backKey, string forwardKey, out float horizontalResult, out float verticalResult)
    {
        horizontalResult = Input.GetKey(GameManager.Instance.GetKey(leftKey)) ? -1f : Input.GetKey(GameManager.Instance.GetKey(rightKey)) ? 1f : 0f;
        verticalResult = Input.GetKey(GameManager.Instance.GetKey(backKey)) ? -1f : Input.GetKey(GameManager.Instance.GetKey(forwardKey)) ? 1f : 0f;
    }

    void Start()
    {
        _ui = FindObjectOfType<UIGame>();
        if (_ui != null)
        {
            _ui.GetComponents(); // This line seems redundant, check if it's necessary
        }

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;

        _playerLayer = LayerMask.NameToLayer("Player");
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");

        canMove = false;
    }

    void Update()
    {
        HandleInput();
        ApplyForces();
        //ManageCollision();
    }

    void HandleInput()
    {
        switch (belongsTo)
        {
            case Player.PlayerOne:
                HandlePlayerInput("P1_Left", "P1_Right", "P1_Back", "P1_Forward", out horizontalPower, out forwardPower);
                break;

            case Player.PlayerTwo:
                HandlePlayerInput("P2_Left", "P2_Right", "P2_Back", "P2_Forward", out horizontalPower, out forwardPower);
                break;

            default:
                horizontalPower = 0f;
                forwardPower = 0f;
                break;
        }
    }

    void ApplyForces()
    {
        rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.forward) * forwardPower * 400f, propeller.transform.position);
        rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * horizontalPower * 300f);

        foreach (GameObject spring in springs)
        {
            ApplySpringForce(spring);
        }

        rb.AddForce(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f);
    }

    void ApplySpringForce(GameObject spring)
    {
        if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out var hit, 3f, ground))
        {
            rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(3f - hit.distance, 2) / 3f * 250f, spring.transform.position);
        }
    }

    // void ManageCollision()
    // {
    //     if (ignoreCollision)
    //     {
    //         Debug.Log("Collision Disabled");
    //         Physics.IgnoreLayerCollision(_playerLayer, _obstacleLayer);
    //         ignoreCollision = false;
    //     }
    //     else if (!ignoreCollision)
    //     {
    //         Debug.Log("Collision Enabled");
    //         Physics.IgnoreLayerCollision(_playerLayer, _obstacleLayer, false);
    //         ignoreCollision = true;
    //     }
    // }

    private void OnDrawGizmos()
    {
        // Draw Gizmos for desired velocity, current velocity, steering force, spring force, and alignment force
        DrawGizmoLine(Vector3.forward * forwardPower * 400f, Color.blue); // Desired velocity in blue
        DrawGizmoLine(rb.velocity, Color.green); // Current velocity in green
        DrawGizmoLine(Time.deltaTime * transform.TransformDirection(Vector3.up) * horizontalPower * 300f, Color.red); // Steering force in red

        foreach (GameObject spring in springs)
        {
            DrawGizmoLine(SpringForce(spring), Color.yellow); // Force from springs in yellow
        }

        DrawGizmoLine(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f, Color.cyan); // Force to align with forward direction in cyan
    }

    void DrawGizmoLine(Vector3 direction, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }

    Vector3 SpringForce(GameObject spring)
    {
        if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out var hit, 3f, ground))
        {
            return Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(3f - hit.distance, 2) / 3f * 250f;
        }
        return Vector3.zero;
    }

    private void Reset()
    {
        
    }
}
