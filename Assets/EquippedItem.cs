using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippedItemHolder", menuName = "EquippedItemHolder/EquippedItemHolder")]
public class EquippedItem : ScriptableObject
{
    public List<So_Clothe_Settings> EquippedData;
}
