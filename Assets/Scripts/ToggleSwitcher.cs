using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitcher : MonoBehaviour
{
    private Toggle myToggle;
    private void Start()
    {
        myToggle = GetComponent<Toggle>();
    }

    public void ChangeState(Toggle toggle)
    {
        myToggle.isOn = toggle.isOn;
    }
}
