using UnityEngine;

public class SetMaterialTiling : MonoBehaviour
{
    Renderer materialRenderer;

    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        materialRenderer.material.SetTextureScale("_MainTex", new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y));
    }
}
