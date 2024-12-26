using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CentralBarUiControl : MonoBehaviour
{
    [SerializeField] SO_ValueMaker Value;
    [SerializeField] List<TextMeshProUGUI> Texts;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ValueUi();
    }
    void ValueUi()
    {
        for(int i = 0; i< Texts.Count; i++)
        {
            Texts[i].text = Value.Amount[i].ToString();
        }
    }
}
