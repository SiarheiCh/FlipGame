using UnityEngine;
using UnityEngine.UI;

public class CanvasHelper : MonoBehaviour
{
    GameObject _startTopText, _startCenterText;
    Vector3 _centerTextPosition, _topTextPosition;
    float _hideLazyBtnDistance = -1000f;
   
    private void Awake()
    {
        _startTopText = Instantiate(Resources.Load("UI/StartTopText"), Vector3.zero, Quaternion.identity, gameObject.transform) as GameObject;
        _startCenterText = Instantiate(Resources.Load("UI/StartCenterText"), Vector3.zero, Quaternion.identity, gameObject.transform) as GameObject;

    }
    void Start()
    {
        _centerTextPosition = new Vector3(0f, -150f, 0f);
        _topTextPosition = new Vector3(0f, 50f, 0f);
        _startCenterText.transform.localPosition = _centerTextPosition;
        _startTopText.transform.localPosition = _topTextPosition;
        GameObject _reloadBtn = GameObject.Find("Canvas/Reload") as GameObject; //Sorry za kostili s heidom knopok, ustal. Po horoshemu cherez instance i Unity Action
        _reloadBtn.transform.position = new Vector3(transform.localPosition.x, transform.position.y +_hideLazyBtnDistance, transform.localPosition.z);
        GameObject _exitBtn = GameObject.Find("Canvas/Exit") as GameObject;
        _exitBtn.transform.position = new Vector3(transform.localPosition.x, transform.position.y + _hideLazyBtnDistance, transform.localPosition.z);
        Text _pointsText = GameObject.Find("Canvas/PointsGameOver").GetComponent<Text>() as Text;
        _pointsText.text = "";
    }
    
    // Update is called once per frame
    void Update()
    {
        if (PlayerHelper._gamestart == true)
        {
            Destroy(_startTopText);
            Destroy(_startCenterText);
        }
    }
}
