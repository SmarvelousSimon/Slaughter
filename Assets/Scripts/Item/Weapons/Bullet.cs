namespace slaughter.de.Item.Weapons
{
    // public class Bullet : MonoBehaviour, IPoolable
    // {
    //     public GameObject Shooter;
    //     private Boolean start;
    //     private float damage = 50;
    //
    //     private void Awake()
    //     {
    //         if (Shooter == null)
    //         {
    //             Shooter = GameObject.FindWithTag("Player");
    //         }
    //     }
    //
    //     private void OnCollisionEnter2D(Collision2D collision)
    //     {
    //         if (collision.gameObject)
    //         {
    //             Debug.Log("collision.gameObject");
    //             collision.gameObject.GetComponent<EnemyController>().takeDamage(damage);
    //         }
    //
    //         ObjectPool.SharedInstance.ReturnToPool(this.gameObject);
    //     }
    //
    //
    //     private float speed = 500;
    //
    //
    //     private void OnEnable()
    //     {
    //         Fire(Shooter.GetComponent<Transform>(), speed, 1);
    //     }
    //
    //     private void Fire(Transform direction, float speed, int lifeTime)
    //     {
    //         Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //         rb.AddForce(direction.right * (-speed));
    //         StartCoroutine(TakeBulletToPool(lifeTime));
    //     }
    //
    //     private IEnumerator TakeBulletToPool(int lifeTime)
    //     {
    //         yield return new WaitForSeconds(lifeTime);
    //         ObjectPool.SharedInstance.ReturnToPool(this.gameObject);
    //     }
    // }
}