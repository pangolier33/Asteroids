using UnityEngine;
using System.Collections;
using TMPro;

public class SpaceShipMovement : MonoBehaviour
{
    [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipMaxVelocity = 10f;
    [SerializeField] private float shipRotationSpeed = 180f;

    private Rigidbody2D shipRigidbody;
    private bool isAlive = true;
    private bool isAccelerating = false;

    public Rigidbody2D GetRigidbody => shipRigidbody;

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            shipRigidbody.AddForce(shipAcceleration * transform.up);
            shipRigidbody.linearVelocity = Vector2.ClampMagnitude(shipRigidbody.linearVelocity, shipMaxVelocity);
        }
    }

    public void HandleShipAcceleration()
    {
        isAccelerating = true;
    }

    public void HandleShipStopAcceleration()
    {
        isAccelerating = false;
    }

    public void HandleShipRotation(float direction)
    {
        shipRigidbody.AddTorque(direction * shipRotationSpeed * Time.deltaTime);
    }
}
