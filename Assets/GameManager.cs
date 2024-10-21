using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] EquippedItem ItemHolder;
    private ScriptableObjectDataManager dataManager;
    void Start()
    {
        dataManager = ScriptableObjectDataManager.Instance;
        dataManager.StartLoadData(GameTotalCoin, ItemHolder);
    }

}
