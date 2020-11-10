using System.Collections;
using UnityEngine;


public class FirstPlatformHelper : MonoBehaviour
{
    float _distanceToPlayer = 1f;
    float _speed = 1000f; 
    int _jumpcounter;
    Rigidbody2D _rigidbody;
   
    void Start()
    {
        _jumpcounter = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (PlayerHelper._activatePlatform)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _distanceToPlayer);
            if (hit.collider != null)
            {
                _jumpcounter++;
                
            }
        }

        else return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("player"))
            if (PlayerHelper._activatePlatform & (_jumpcounter > 0))
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(DestroyPlatform());
            }
       
    }
    private void Update()
    {
       
        if (_jumpcounter > 2)
        {
            _rigidbody.AddForce(Vector2.down * _speed);

        }

    }
    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(3f);
        _jumpcounter++;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
