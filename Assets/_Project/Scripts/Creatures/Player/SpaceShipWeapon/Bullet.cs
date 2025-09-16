using UnityEngine;

namespace _Project.Scripts.Creatures.Player.SpaceShipWeapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 15f;
        
        private void OnEnable()
        {
            Invoke(nameof(Deactivate), 5f);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }

        public void Launch(Vector3 direction)
        {
            direction.Normalize();
            transform.up = direction;
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
