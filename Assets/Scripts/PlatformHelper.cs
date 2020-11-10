using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlatformHelper : MonoBehaviour
{
    float _distanceToPlayer = 1f;
    float _speed = 1000f;
    float _speedAfterTrigger = 0.5f;
    bool _trigger = false;
    int _jumpcounter = 0;    
    Rigidbody2D _rigidbody;
    Vector3 _nextPlatformPosition;
    TextHelper _pointsText;
    GameObject _pointsGO;    

    // Start is called before the first frame update
    void Start()
    {
        _pointsGO = GameObject.Find("Canvas/Points");
        _pointsText = _pointsGO.GetComponent<TextHelper>();
        _pointsText.Texter = gameObject.transform;
        _pointsGO.GetComponent<Text>().text = PlayerHelper._points.ToString();
        _nextPlatformPosition = new Vector3(-0.5f, -3f, -2f);
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("player"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _distanceToPlayer);
            if ((hit.collider != null) & (_trigger == false))
            {
                _trigger = true;
                _jumpcounter++;                
            }
        }

        else return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_trigger == true)
        {
            if ((_jumpcounter > 0) & collision.name.Contains("player"))
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;                
                StartCoroutine(DestroyPlatform());
            }
        }
        
    }
    

    private void Update()
    {
        if (_trigger == true)
        {

            transform.position = Vector3.Lerp(transform.position, _nextPlatformPosition, Time.deltaTime * _speedAfterTrigger);
            
        }
        if(_jumpcounter > 1)
        {
            _rigidbody.AddForce(Vector2.down * _speed);
        }

    }
     
IEnumerator  DestroyPlatform()
    {
        
        yield return new WaitForSeconds(3f);
        _jumpcounter++;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
