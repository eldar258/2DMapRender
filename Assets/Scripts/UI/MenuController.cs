using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public Text SpriteName;
    void OnEnable()
    {
        SetTextSpriteTopLeftCornerOfCamera();
    }
    private void SetTextSpriteTopLeftCornerOfCamera()
    {
        var hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)), Camera.main.transform.forward);
        SpriteName.text = hit2d.collider != null ? hit2d.collider.gameObject.name : string.Empty;
    }
}
