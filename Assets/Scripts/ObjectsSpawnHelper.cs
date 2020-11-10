using UnityEngine;

public class ObjectsSpawnHelper : MonoBehaviour
{
    Vector3 _groundPosition;
    Vector3 _playerPosition, _upToothPosition, _downToothPosition;
    float _objectDistanceToBackground = 2f;
    float _platformDistance = -1f;
    GameObject  player;
    GameObject _ground, _platform, _topTooth, _bottomTooth;    
    void Start()
    {
    _groundPosition = new Vector3(-1.1f, -2.5f, -2f);
    _playerPosition = new Vector3(-0.15f, -2.25f, -2f);
    _upToothPosition = new Vector3(-3.5f, 3.58f, -2f);
    _downToothPosition = new Vector3(-3.5f, -6.61f, -2.1f);
    CreateStartObjects();
    
    }
    void CreateStartObjects()
    {
        _topTooth = Instantiate(Resources.Load("Props/TopTooth"), _upToothPosition, Quaternion.identity) as GameObject;
        _bottomTooth = Instantiate(Resources.Load("Props/BottomTooth"), _downToothPosition, Quaternion.identity) as GameObject;
        _ground = Instantiate(Resources.Load("Props/Ground"), _groundPosition, Quaternion.identity) as GameObject;
        player = Instantiate(Resources.Load("player"), _playerPosition, Quaternion.identity) as GameObject;        
        _platform = Instantiate(Resources.Load("Props/Platform"), Vector3.zero, Quaternion.identity) as GameObject;
        _platform.transform.position = new Vector3(_platform.transform.position.x + _platformDistance/2, _platform.transform.position.y + _platformDistance, _platform.transform.position.z - _objectDistanceToBackground);
    }
   
}
