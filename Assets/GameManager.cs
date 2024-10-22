using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] EquippedItem ItemHolder;
   
    private void Awake()
    {
       
    }
  
    private void OnEnable()
    {
       
    }
    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.K))
            ScriptableObjectDataManager.Instance.DeleteAllJsonFiles();
        */
    }
    void LoadWearData()
    {
       
        foreach(So_Clothe_Settings equippedItem in ItemHolder.EquippedData)
        {
            Debug.Log("kontrol");
        }
        
    }

}
