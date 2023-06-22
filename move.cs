using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movesaida : MonoBehaviour
{
    public Transform target; // Alvo para mover o objeto
    public LayerMask followLayer; // Layer dos colisores a serem seguidos
    public LayerMask repelLayer; // Layer dos colisores a serem repelidos
    public LayerMask wallLayer; // Layer da parede
    public float followSpeed = 5f; // Fator de velocidade para seguir o alvo
    public float repelForce = 10f; // Força de repulsão

    private void Update()
    {
        Collider2D[] followColliders = Physics2D.OverlapCircleAll(transform.position, 100f, followLayer); // Encontrar todos os colisores 2D na camada de seguir em um raio de 100 unidades

        // Seguir o colisor mais próximo
        if (followColliders.Length > 0)
        {
            Collider2D closestCollider = null;
            float closestDistance = Mathf.Infinity;

            // Encontrar o colisor mais próximo na camada de seguir
            foreach (Collider2D collider in followColliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }

            if (closestCollider != null)
            {
                Vector2 delta = closestCollider.transform.position - transform.position;
                // Mover o objeto em direção ao alvo do colisor mais próximo com o fator de velocidade de seguir

                // Verificar colisão com a parede
                RaycastHit2D hit = Physics2D.Raycast(transform.position, delta.normalized, delta.magnitude, wallLayer);
                if (hit.collider == null)
                {
                    transform.position += (Vector3)delta.normalized * followSpeed * Time.deltaTime;
                }
            }
        }

        Collider2D[] repelColliders = Physics2D.OverlapCircleAll(transform.position, 100f, repelLayer); // Encontrar todos os colisores 2D na camada de repelir em um raio de 100 unidades

        // Repelir-se dos colisores na camada de repelir
        foreach (Collider2D collider in repelColliders)
        {
            Vector2 repelDirection = transform.position - collider.transform.position;
            // Aplicar uma força de repulsão para afastar-se dos colisores na camada de repelir
            transform.position += (Vector3)repelDirection.normalized * repelForce * Time.deltaTime;
        }
    }
}
