using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text coordinatesText;
    [SerializeField] private TMP_Text rotationText;
    [SerializeField] private TMP_Text velocityText;
    [SerializeField] private TMP_Text chargesText;
    [SerializeField] private TMP_Text nextLaserText;
    [SerializeField] private SpaceShipMovement _spaceShipMonement;
    [SerializeField] private SpaceShipWeapon _spaceShipWeapon;

    private Rigidbody2D shipRigidbody;
    private int _score;



    private void Start()
    {
        shipRigidbody = _spaceShipMonement.GetRigidbody;
    }

    private void Update()
    {
        UpdateShipInfoUI();
    }

    private void UpdateShipInfoUI()
    {
        UpdateCoordinatesUI();

        UpdateRotationUI();

        UpdateSpeedUI();

        UpdateLaserChargesUI();

        UpdateNextLaserTimeUI();
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle < 0) angle += 360;
        return angle;
    }

    private void UpdateCoordinatesUI()
    {
        Vector2 position = shipRigidbody.position;
        coordinatesText.text = $"Position:\nX:{position.x:F1}\nY:{position.y:F1}";
    }

    private void UpdateRotationUI()
    {
        float rotation = NormalizeAngle(shipRigidbody.rotation);
        rotationText.text = $"Rotation: {rotation:F0}°";
    }

    private void UpdateSpeedUI()
    {
        Vector2 velocity = shipRigidbody.linearVelocity;
        velocityText.text = $"Speed: {velocity.magnitude:F1}";
    }

    private void UpdateLaserChargesUI()
    {
        int laserCharges = _spaceShipWeapon.GetLaserCharges();
        chargesText.text = $"Laser charges: {laserCharges}";
    }

    private void UpdateNextLaserTimeUI()
    {
        float nextLaserTime = _spaceShipWeapon.GetNextChargeProgress();
        nextLaserText.text = $"Next laser time: {nextLaserTime:F0}";
    }
}
