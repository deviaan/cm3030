using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireControls : MonoBehaviour
{
    [SerializeField] public float fireRate = 0.3f;
    public GameObject bullet;
    private float nextShot;
    
    public void Attack(InputAction.CallbackContext context)
    {
        if (Time.time > nextShot)
        {
            var transform1 = transform;
            var position = transform1.position;
            var pos = new Vector3(
                position.x + 1,
                position.y,
                position.z
            );
            Instantiate(bullet, pos, transform1.rotation);
            nextShot = Time.time + fireRate;
        }
    }
}
