using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public delegate void SetIsAction(bool isAction);
    public static SetIsAction ToggleCameraMovement;
    
   public void OnClickSetting()
    {
        bool isActive = Menu.activeSelf;
        Menu.SetActive(!isActive);
        ToggleCameraMovement?.Invoke(isActive);
    }
}
