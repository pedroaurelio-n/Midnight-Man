using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableRenderer : MonoBehaviour
{
    private void Start()
    {
        gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite);
        gameObject.TryGetComponent<TilemapRenderer>(out TilemapRenderer tilemap);

        if (sprite != null)
            sprite.enabled = false;

        if (tilemap != null)
            tilemap.enabled = false;
    }
}
