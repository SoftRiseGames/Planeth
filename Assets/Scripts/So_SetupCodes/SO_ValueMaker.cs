using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Value/GameTakenTotalValue", fileName = "GameTakenTotalValue")]
public class SO_ValueMaker : ScriptableObject
{
    public List<int> Amount
    {
        get => _amount;
        set
        {
            _amount = ClampListToMin(value, 0);
        }
    }

    [SerializeField] private List<int> _amount;

        public static List<int> ClampListToMin(List<int> list, int minValue)
    {
        List<int> result = new List<int>();
        foreach (int item in list)
        {
            result.Add(Mathf.Max(minValue, item));
        }
        return result;
    }
}
