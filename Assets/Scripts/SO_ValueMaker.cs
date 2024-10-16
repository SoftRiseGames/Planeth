using UnityEngine;

[CreateAssetMenu(menuName = "Value/GameTakenTotalValue",fileName = "GameTakenTotalValue")]
public class SO_ValueMaker : ScriptableObject
{
    public int Amount
    {
        get => _amount;
        set
        {
            _amount = Mathf.Max(0, value);
        }
    }
    [SerializeField] int _amount;

}
