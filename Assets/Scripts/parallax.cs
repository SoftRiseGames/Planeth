using UnityEngine;

public class parallax : MonoBehaviour
{

    [Range(-1f, 100f)]
    public float scrollspeed = 100f;
    private float offset;
    private Material mat;

    bool isParallaxStart;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void OnEnable()
    {
        Controlls.Launch += ParallaxStartControl;
    }
    private void OnDisable()
    {
        Controlls.Launch -= ParallaxStartControl;
    }
    private void Update()
    {
        if(isParallaxStart)
            ParallaxStart();

    }

    void ParallaxStartControl()
    {
        isParallaxStart = true;
    }
    void ParallaxStart()
    {
        offset += (Time.deltaTime * scrollspeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
