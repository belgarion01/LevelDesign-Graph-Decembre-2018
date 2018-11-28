using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque : MonoBehaviour
{
    public Collider[] Ennemies;
    public float radius;

    void Update()
    {
        Ennemies = Physics.OverlapSphere(transform.position, radius, 1 << 9);
        if (Input.GetKeyDown(KeyCode.R)) {
            Attack();
        }
    }

    void Attack() {
        foreach (Collider ennemy in Ennemies) {
            ennemy.GetComponent<Ennemy>().health--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
