using UnityEngine;
using DG.Tweening;
public class PopUpEventListen : MonoBehaviour
{

    public GameObject StoreMenuUI;
    public GameObject EquippedItemMenuUI;
    public GameObject RawMaterialMenuUI;
    private void OnEnable()
    {

        MenuButtonEvents.isStorePopUp += StoreUIAnim;
        MenuButtonEvents.isEquippedPopUp += EquippedItemAnim;
        MenuButtonEvents.isRawMaterialPopUp += RawMaterialAnim;

        MenuButtonEvents.isRawMaterialPopUpOff += CloseRawMaterialUIPanel;
        MenuButtonEvents.isEquippedPopUpOff += CloseEquippedUIPanel;
        MenuButtonEvents.isStorePopUpOff += CloseStoreUIPanel;
    }
    private void OnDisable()
    {
        MenuButtonEvents.isStorePopUp -= StoreUIAnim;
        MenuButtonEvents.isEquippedPopUp -= EquippedItemAnim;
        MenuButtonEvents.isRawMaterialPopUp -= RawMaterialAnim;

        MenuButtonEvents.isRawMaterialPopUpOff -= CloseRawMaterialUIPanel;
        MenuButtonEvents.isEquippedPopUpOff -= CloseEquippedUIPanel;
        MenuButtonEvents.isStorePopUpOff -= CloseStoreUIPanel;

    }
   
    void StoreUIAnim()
    {
        StoreMenuUI.GetComponent<Animator>().SetBool("isPopUp", true);
    }

    void EquippedItemAnim()
    {
        EquippedItemMenuUI.GetComponent<Animator>().SetBool("isPopUp", true);
    }

    void RawMaterialAnim()
    {
        RawMaterialMenuUI.GetComponent<Animator>().SetBool("isPopUp", true);
    }

    void CloseStoreUIPanel()
    {
        StoreMenuUI.GetComponent<Animator>().SetBool("isPopUp", false);
    }
    void CloseEquippedUIPanel()
    {
        EquippedItemMenuUI.GetComponent<Animator>().SetBool("isPopUp", false);
    }
    void CloseRawMaterialUIPanel()
    {
        RawMaterialMenuUI.GetComponent<Animator>().SetBool("isPopUp", false);
    }
}
