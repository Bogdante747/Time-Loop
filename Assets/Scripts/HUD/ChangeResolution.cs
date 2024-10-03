using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeResolution : MonoBehaviour
{
   public Dropdown Dropdown;
   public Toggle Toggle;

   void Start()
   {
    Screen.fullScreen = true;

    Toggle.isOn = true;
   }

   public void ScreenMod()
   {
    Screen.fullScreen = Toggle.isOn;
   }

   public void Change()
   {
    if(Dropdown.value == 0)
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }
    if(Dropdown.value == 1)
    {
        Screen.SetResolution(1366, 768, Screen.fullScreen);
    }
    if(Dropdown.value == 2)
    {
        Screen.SetResolution(1600, 900, Screen.fullScreen);
    }
    if(Dropdown.value == 3)
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
   }
}
