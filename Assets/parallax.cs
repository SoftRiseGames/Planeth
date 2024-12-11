using UnityEngine;

public class parallax : MonoBehaviour
{

    [Range(-1f, 100f)]
    public float scrollspeed = 100f;
    private float offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += (Time.deltaTime * scrollspeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
