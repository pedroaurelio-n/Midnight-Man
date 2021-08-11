using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private int offset = 0;
    [SerializeField] private bool runOnlyOnce = false;
    [SerializeField] private Renderer myRenderer;

    private float timer;
    private float timerMax = .1f;

    private void Awake()
    {
        if (myRenderer == null)
            myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = timerMax;

            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);

            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
