using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RawItem", menuName = "RawItem")]
public class So_RawMaterialScript : ScriptableObject
{
    public Sprite Background;
    public Sprite Skin;
    public string ObjectName;
    public int ObjectCount;
    public List<int> MaterialRequipment;
  
}
