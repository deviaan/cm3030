using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireControls : MonoBehaviour
{
    [SerializeField] public float fireRate = 0.3f;
    [SerializeField] public Animator animator;
    [SerializeField] public float bulletForce = 30.0f;
    [SerializeField] public PlayerHealth playerHealth;
    [SerializeField] public AudioSource splatSoundEffect;
    public GameObject bullet;
    public float nextShot;
    public bool isShooting = false;
    private bool _isFacingRight = true;
    
    public void Attack(InputAction.CallbackContext context)
    {
        if (Time.time > nextShot && playerHealth.PlayerCanAct())
        {
            isShooting = true;
            animator.SetBool("IsShooting", true);
            splatSoundEffect.Play();
            CreateBullet();
            nextShot = Time.time + fireRate;
        }
    }

    private void CreateBullet()
    {
        var position = transform.position;
        var bulletPos = new Vector3(
            position.x + 0.3f * (_isFacingRight ? 1 : -1),
            position.y, position.z
        );

        var newBullet = Instantiate(bullet, bulletPos, transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(
            bulletForce * (_isFacingRight ? 1 : -1), 0
        ), ForceMode2D.Impulse);
    }

    public void ToggleFiring()
    {
        isShooting = false;
    }

    public void Flip()
    {
        _isFacingRight = !_isFacingRight;
    }
}
