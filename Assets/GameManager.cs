using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] EquippedItem ItemHolder;
    private ScriptableObjectDataManager dataManager;

    private void Awake()
    {
        dataManager = ScriptableObjectDataManager.Instance;
        dataManager.StartLoadData(GameTotalCoin, ItemHolder);
    }
  
    private void OnEnable()
    {
       
    }
    private void Update()
    {
        LoadWearData();
    }
    void LoadWearData()
    {
       
        foreach(So_Clothe_Settings equippedItem in ItemHolder.EquippedData)
        {
            Debug.Log("kontrol");
            dataManager.LoadWearData(equippedItem);
        }
        
    }

}
