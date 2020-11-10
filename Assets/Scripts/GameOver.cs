using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Animator _cameraA;  
    void Start()
    {
        _cameraA = FindObjectOfType<Camera>().GetComponent<Animator>();
        _cameraA.SetBool("Shake", true);
        StartCoroutine(RecallUI());

    }
    
    IEnumerator RecallUI()
    {
        yield return new WaitForSeconds(1f);
       
        GameObject _reloadBtn = GameObject.Find("Canvas/Reload") as GameObject;
        _reloadBtn.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        GameObject _exitBtn = GameObject.Find("Canvas/Exit") as GameObject;

        Vector3 _exitBtnStartPosition = new Vector3(115f, 117f, 0f);
        _exitBtn.transform.localPosition = _exitBtnStartPosition;
        Text _pointsText = GameObject.Find("Canvas/PointsGameOver").GetComponent<Text>() as Text;
        _pointsText.text = PlayerHelper._points + "   Points";

    }
}
