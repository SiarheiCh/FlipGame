using UnityEngine;

public class TextHelper : MonoBehaviour
{
    public Transform Texter;
   
    // Update is called once per frame
    void Update()
    {
        if (Texter == null)
            return;
        Vector3 platformPos = new Vector3(Texter.position.x, Texter.position.y +0.5f, Texter.position.z);
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(platformPos);
    }
}
