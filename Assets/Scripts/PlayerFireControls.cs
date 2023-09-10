using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireControls : MonoBehaviour
{
    public GameObject bullet;
    public void Attack(InputAction.CallbackContext context)
    {
        var transform1 = transform;
        Instantiate(bullet, transform1.position, transform1.rotation);
    }
}
