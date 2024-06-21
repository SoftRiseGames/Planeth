using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ClockManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    void Update()
    {
        DateTime now = DateTime.Now;
        clockText.text = now.ToString("HH:mm");
    }
}
