using UnityEngine;

namespace _Project.Scripts.UI
{
    public interface IHUDView
    {
        void SetCoordinates(Vector2 position);
        void SetRotation(float angle);
        void SetSpeed(float speed);
        void SetLaserCharges(int charges);
        void SetNextLaserTime(float progress);
    }
}