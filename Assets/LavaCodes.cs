using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LavaCodes : MonoBehaviour
{
    [SerializeField] GameObject Lava;
    [SerializeField] float speed;
    [SerializeField] CharacterMovement character;

    [SerializeField] List<ObjectAllDatas> AnchorPoints = new List<ObjectAllDatas>();




    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();
    }
    void SpeedControl()
    {
        /*
        if (character.SpeedMeter - speed > 3 && character.SpeedMeter - speed < 6)
            Lava.GetComponent<Image>().rectTransform.DOAnchorPosY(AlertLevel[1], .2f);
        */
    }

   
        
}
[System.Serializable]
public class ObjectLocation
{
    public List<float> Location;
}
[System.Serializable]
public class ObjectAllDatas
{
    public List<ObjectLocation> AllObject;
}

