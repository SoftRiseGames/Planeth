using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    [SerializeField] float intencity;
    [SerializeField] float time;
    private float timerHolder;
    [SerializeField] CinemachineVirtualCamera instance;
   
    private void OnEnable()
    {
        CharacterDedectionControl.isEnemyCollide += CamShake;
    }
    private void OnDisable()
    {
        CharacterDedectionControl.isEnemyCollide -= CamShake;
    }

    void CamShake()
    {
        timerHolder = time;
        instance.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intencity;
        
    }

    private void Update()
    {
        timerHolder -= Time.deltaTime;
        if (timerHolder <= 0)
            instance.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        
    }


}
