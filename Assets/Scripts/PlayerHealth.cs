using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHealth = 5;
    [SerializeField] public Animator animator;
    [SerializeField] public float hitStun = 1.0f;
    [SerializeField] public float invulnerability = 2.0f;
    [SerializeField] public AudioSource ouchSoundEffect;
    [SerializeField] public AudioSource deadSoundEffect;
    [SerializeField] public AudioSource pieSoundEffect;
    private bool _isDead = false;
    public bool wasHit = false;
    public float hitStunClear;
    public float invulClear;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            PlayerWasHit();
        } else if (other.gameObject.CompareTag("Health"))
        {
            pieSoundEffect.Play();
            ++playerHealth;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            PlayerWasHit();
        }
    }

    private void PlayerWasHit()
    {
        if (!_isDead && !wasHit && PlayerCanBeHit())
        {
            --playerHealth;
            
            if (playerHealth <= 0)
            {
                deadSoundEffect.Play();
                _isDead = true;
                animator.SetBool("IsDead", true);
                Destroy(gameObject, 2);
            }
            else
            {
                ouchSoundEffect.Play();
                wasHit = true;
                animator.SetBool("IsHit", true);
                hitStunClear = Time.time + hitStun;
                invulClear = Time.time + invulnerability;
            }
        }
    }

    public void ClearHitStun()
    {
        wasHit = false;
    }

    public bool PlayerCanAct()
    {
        return !_isDead && Time.time > hitStunClear;
    }

    public bool PlayerCanBeHit()
    {
        return !_isDead && Time.time > invulClear;
    }
}
