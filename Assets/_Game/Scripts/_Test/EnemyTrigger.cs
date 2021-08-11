using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform newPosition;
    [SerializeField] private bool isGuaranteed;
    [SerializeField] private bool isTurbo;
    [SerializeField] private bool deactivateEnemy;
    [SerializeField] private int chance;

    private bool isActive;

    private void Start()
    {
        isActive = true;
    }

    private void ActivateEnemy()
    {
        if (isTurbo)
        {
            enemy.ActivateEnemyTurbo(newPosition);
        }

        else
            enemy.ActivateEnemy(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && isActive && !GameManager.Instance.isEnemyActive)
        {
            if (deactivateEnemy)
            {
                enemy.DeactivateEnemy();
                isActive = false;
                return;
            }

            if (isGuaranteed)
            {
                ActivateEnemy();
                isActive = false;
                return;
            }

            int n = Random.Range(1, chance);

            if (n == 1)
            {
                ActivateEnemy();
            }
        }
    }
}
