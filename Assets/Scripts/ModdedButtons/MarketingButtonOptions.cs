using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketingButtonOptions : MonoBehaviour
{
    public So_Clothe_Settings ButtonSettingObject;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] TextMeshProUGUI ObjectName;
    [SerializeField] Image background;
    [SerializeField] Image Skin;
    [SerializeField] SO_ValueMaker GameTotalCoin;
    public bool isTaken;

    private void Awake()
    {
        //ScriptableObjectDataManager.Instance.LoadEquippedData();
    }
    private void Start()
    {
       
        ifTake();
        ReCreate();
    }
    private void Update()
    {
       
    }
    void ifTake()
    {
        if (ButtonSettingObject.isTaken == true)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        
    }
    public void ReCreate()
    {
        background.sprite = ButtonSettingObject.Background;
        Skin.sprite = ButtonSettingObject.Skin;
        ObjectName.text = ButtonSettingObject.ObjectName;
    }

    public void TakeItem()
    {
        
        int CheckerInt = 0;
      
        foreach(int i in GameTotalCoin.Amount)
        {
            if (i >= ButtonSettingObject.RequirementItem[CheckerInt])
            {
                Debug.Log("buyukveesit");
                CheckerInt = CheckerInt + 1;
            }
            else
                return;

        }
        if (GameTotalCoin.Amount.Count == CheckerInt)
        {
            this.ButtonSettingObject.isTaken = true;
            for (int i = 0; i< ButtonSettingObject.RequirementItem.Count; i++)
            {
                GameTotalCoin.Amount[i] = GameTotalCoin.Amount[i] - ButtonSettingObject.RequirementItem[i];
            }
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(this.ButtonSettingObject);
            ScriptableObjectDataManager.Instance.SaveData(ButtonSettingObject);
         
        }
        
      
    }

    
}
