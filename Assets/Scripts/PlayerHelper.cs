using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHelper : MonoBehaviour
{
    float _startSpeed = 2000f;
    float _speed = 2000f;    
    float _accelerate = 1f;
    float _distanceToPlatform = 1.7f;
    Vector3 _nextPlatformPosition;
    Vector3 _gemPosition;
    public static bool _activatePlatform;    
    public static bool _gamestart;
    public static int _gems;
    public static int _points;
    bool _isGrounded;
    bool _platformAlreadyCreated;
    bool _gemAlreadyCreated;
    Rigidbody2D _rigidbody;       
    SpriteRenderer _spriteRenderer;
    Sprite _usualPlayer, _jumpPlayer;
    GameObject _ground;
    Text _pointText, _gemsText;
    
    
    
    void Start()
    {
        _gemAlreadyCreated = false;
        _isGrounded = true;
        _gamestart = false;
        _activatePlatform = false;
        _gems = 0;
        _points = 0;
        gameObject.layer = 2;
        _nextPlatformPosition = new Vector3(-0.5f, 1.5f, -2f);
        _gemPosition = new Vector3(-0.2f, 3.5f, -2f);
        _rigidbody = GetComponent<Rigidbody2D>();        
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _usualPlayer = Resources.Load<Sprite>("usual");
        _jumpPlayer = Resources.Load<Sprite>("jump");
        _pointText = GameObject.Find("Canvas/PointsNumber").GetComponent<Text>();
        _gemsText = GameObject.Find("Canvas/GemsCounter").GetComponent<Text>();

    }

    void Update()
    {               
            if (Input.GetKeyDown(KeyCode.Space))
        {
            _gamestart = true;
                _speed = _startSpeed;            
            if (_isGrounded)
            {
                _spriteRenderer.sprite = _jumpPlayer;
                StartCoroutine(Accelerate());
            }
           else return;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {           
            if (_isGrounded)
            {                
                _rigidbody.AddForce(Vector2.up * _speed);
                _spriteRenderer.sprite = _usualPlayer;
                _isGrounded = false;
                _platformAlreadyCreated = false;
                
            }
            
            else return;

        }


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("gem"))
        {
            _gems++;
            _gemsText.text = _gems.ToString();
            _gemAlreadyCreated = false;
            Destroy(collision.gameObject);
            
        }

        if (collision.name.Contains("TopTooth"))
        {

            Vector3 positionTopToothBorder = new Vector3(-0.15f, 3.85f, -2f);
            Instantiate(Resources.Load("playerGO"), positionTopToothBorder, Quaternion.identity);
            Destroy(transform.gameObject);
            

        }
        if (collision.name.Contains("BottomTooth"))
        {
            
            Vector3 positionBottomToothBorder = new Vector3(-0.15f, -3.5f, -2f);
            Instantiate(Resources.Load("playerGO"), positionBottomToothBorder, Quaternion.identity);
            Destroy(transform.gameObject);
            

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;
        
        if (collision.gameObject.name.Contains("Ground"))
            {
                _ground = collision.gameObject;
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _distanceToPlatform);
        if (hit.collider != null)
        {
            if (hit.collider.name.Contains("Platform"))
            {                
                _activatePlatform = true;
                if (_platformAlreadyCreated == false)
                {
                    _points++;
                    _pointText.text = "Points   " + _points; 
                    StartCoroutine(CreatePlatform());
                    _platformAlreadyCreated = true;
                }
            }
        }
    }
    IEnumerator CreatePlatform()
    {
        yield return new WaitForSeconds(1.3f);
        GameObject platform = Instantiate(Resources.Load("Props/NextPlatform"), _nextPlatformPosition, Quaternion.identity) as GameObject;
        // Gem Instanse 
        if (_gemAlreadyCreated == false)
        {
            Instantiate(Resources.Load("gem"), _gemPosition, Quaternion.identity);
            _gemAlreadyCreated = true;
        }
        if (_ground != null)
        {
          Rigidbody2D _rigidbodyGround = _ground.GetComponent<Rigidbody2D>();
           _rigidbodyGround.constraints = RigidbodyConstraints2D.None;
           _rigidbodyGround.constraints = RigidbodyConstraints2D.FreezeRotation;
           _rigidbodyGround.AddForce(Vector2.down * Time.deltaTime);
            yield return new WaitForSeconds(1f);
            Destroy(_ground.gameObject);
        }
        
    }
   
    IEnumerator  Accelerate()
   {   
        while (!Input.GetKeyUp(KeyCode.Space))
        {
            yield return new WaitForEndOfFrame();
            _speed = _speed + _accelerate;
        }
        
    }
   
}
