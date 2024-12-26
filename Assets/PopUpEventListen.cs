using UnityEngine;
using DG.Tweening;
public class PopUpEventListen : MonoBehaviour
{

    public GameObject StoreUI;
    public GameObject EquippedItemUI;

    private void OnEnable()
    {

        MenuButtonEvents.isStorePopUp += StoreUIAnim;
        MenuButtonEvents.isEquippedPopUp += EquippedItemAnim;
    }
    private void OnDisable()
    {
        MenuButtonEvents.isStorePopUp -= StoreUIAnim;
        MenuButtonEvents.isEquippedPopUp -= EquippedItemAnim;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StoreUIAnim()
    {
        StoreUI.transform.DOMove(new Vector2(0, 0), .5f);
    }

    void EquippedItemAnim()
    {
        //   EquippedItemUI.transform.DOMove(new Vector2(0, 0), .5f);
        EquippedItemUI.GetComponent<Animator>().SetBool("isPopUp", true);
    }
}
