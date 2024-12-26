using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform rectransform;
    Rect safeArea;
    Vector2 minAnchor;
    Vector2 maxAnchor;
    private void Awake()
    {
        rectransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectransform.anchorMin = minAnchor;
        rectransform.anchorMax = maxAnchor;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
