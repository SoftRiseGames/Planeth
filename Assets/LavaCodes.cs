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

    [SerializeField] List<float> AnchorPoints;


    bool isGameStart = false;

    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameStart)
            SpeedControl();
    }

    private void OnEnable()
    {
        CharacterMovement.EnemyComeAndGameStart += GameStart;
    }
    private void OnDisable()
    {
        CharacterMovement.EnemyComeAndGameStart -= GameStart;
    }

    void GameStart()
    {
        isGameStart = true;
    }
    void SpeedControl()
    {
        
        if (character.SpeedMeter - speed > 8 && character.SpeedMeter - speed < 12)
            Lava.GetComponent<Image>().rectTransform.DOAnchorPosY(AnchorPoints[1], .2f);
        else if(character.SpeedMeter - speed > 4 && character.SpeedMeter - speed < 7)
            Lava.GetComponent<Image>().rectTransform.DOAnchorPosY(AnchorPoints[2], .2f);
        else if (character.SpeedMeter - speed <= 4)
            Lava.GetComponent<Image>().rectTransform.DOAnchorPosY(AnchorPoints[4], .2f);


    }

   
        
}


