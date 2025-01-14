using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Traps"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("CheckPoint"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyProjectile"));
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 1) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            anim.SetTrigger("explode");
            hit = true; hit = true;
            boxCollider.enabled = false; boxCollider.enabled = false;
            collision.GetComponent<Health>().TakeDamage(1); collision.GetComponent<Health>().TakeDamage(1);
        }
        else if (collision.tag == "Ground")
        {
            anim.SetTrigger("explode");
            hit = true;
            boxCollider.enabled = false;
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}