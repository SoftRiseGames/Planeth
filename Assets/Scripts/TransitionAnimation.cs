using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        MenuButtonEvents.isTransition += PlayTransition;
    }
    private void OnDisable()
    {
        MenuButtonEvents.isTransition -= PlayTransition;
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayTransition()
    {
        GetComponent<Animator>().Play(0);
    }
}
