using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class ItemInspector : OdinEditorWindow
{
    [TableList]
    [SerializeField] List<So_Clothe_Settings> Clothes;


    [HorizontalGroup("Float Group")]
    [SerializeField] float AnchorXaxisBasePoint,AnchoryaxisBasePoint, DistanceBetweenObject;
    private float AnchorXaxisUpdated;
    private float AnchorYaxisUpdated;
    [HorizontalGroup("Second Float group")]
    [SerializeField] float MaxWidthLimit, LowerDistance;

    [SerializeField] GameObject instantiateGameobject;


    [MenuItem("Game Settings/Main Menu")]
    public static void showWindow()
    {
        GetWindow<ItemInspector>().Show();
        //GetWindow<ItemInspector>().StartingVariebles();
    }
    public void StartingVariebles()
    {
        AnchorXaxisUpdated = AnchorXaxisBasePoint;
        AnchorYaxisUpdated = AnchoryaxisBasePoint; 
        Debug.Log("Deðiþkenler Dengelendi");
    }
    

    [Button("Debugger")]
    void DebuggerButton()
    {
        Debug.Log("ckick");
       for(int i = 0; i< Clothes.Count; i++)
        {
            if (i / MaxWidthLimit - 1 == 0)
            {
                Debug.Log(i / MaxWidthLimit - 1);
                AnchorYaxisUpdated = AnchorYaxisUpdated- LowerDistance;
                AnchorXaxisUpdated = AnchorXaxisBasePoint;
            }
            Instantiate(instantiateGameobject, new Vector2(AnchorXaxisUpdated, AnchorYaxisUpdated), Quaternion.identity);
            AnchorXaxisUpdated = AnchorXaxisUpdated + DistanceBetweenObject;
        }
    }

}
