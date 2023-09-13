using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireControls : MonoBehaviour
{
    public GameObject bullet;
    public void Attack(InputAction.CallbackContext context)
    {
        var transform1 = transform;
        var position = transform1.position;
        var pos = new Vector3(
            position.x + 1,
            position.y, 
            position.z
        );
        Instantiate(bullet, pos, transform1.rotation);
    }
}
