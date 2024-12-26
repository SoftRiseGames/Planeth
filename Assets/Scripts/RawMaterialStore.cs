using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RawMateraiStore : MonoBehaviour
{
    public So_RawMaterialScript ButtonSettingObject;
    [SerializeField] TextMeshProUGUI ObjectName;
    [SerializeField] Image background;
    [SerializeField] Image Skin;
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] int HowManyTake;

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
        if (ButtonSettingObject.ObjectCount == 0)
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

        ButtonSettingObject.ObjectCount = ButtonSettingObject.ObjectCount - 1;
        int CheckerInt = 0;
        foreach (int i in GameTotalCoin.Amount)
        {
            if (i >= ButtonSettingObject.MaterialRequipment[CheckerInt])
            {
                CheckerInt = CheckerInt + 1;
            }
            else
                return;

        }
        if (GameTotalCoin.Amount.Count == CheckerInt)
        {

            ButtonSettingObject.ObjectCount = ButtonSettingObject.ObjectCount - HowManyTake;

            if (ButtonSettingObject.rawitem == RawItemTypes.leather)
                GameTotalCoin.Amount[1] = GameTotalCoin.Amount[1] + (ButtonSettingObject.EarnPerItem * HowManyTake);
            else if (ButtonSettingObject.rawitem == RawItemTypes.iron)
                GameTotalCoin.Amount[2] = GameTotalCoin.Amount[2] + (ButtonSettingObject.EarnPerItem * HowManyTake);
            else if (ButtonSettingObject.rawitem == RawItemTypes.gold)
                GameTotalCoin.Amount[3] = GameTotalCoin.Amount[3] + (ButtonSettingObject.EarnPerItem * HowManyTake);
            else if (ButtonSettingObject.rawitem == RawItemTypes.HardenedSteel)
                GameTotalCoin.Amount[4] = GameTotalCoin.Amount[4] + (ButtonSettingObject.EarnPerItem * HowManyTake);



            for (int i = 0; i < ButtonSettingObject.MaterialRequipment.Count; i++)
            {
                GameTotalCoin.Amount[i] = GameTotalCoin.Amount[i] - (ButtonSettingObject.MaterialRequipment[i]*HowManyTake);
            }
            ScriptableObjectDataManager.Instance.SaveRawMaterialData(ButtonSettingObject);
            

        }


    }


}
