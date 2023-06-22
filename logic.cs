using UnityEngine;
using UnityEngine.SceneManagement;

public class Logica : MonoBehaviour
{
    public float tempoDeSeguimento = 1f; // Tempo em segundos para seguir o mouse 
    private Vector3 posicaoMouse;
    private float tempoClique;
    public GameObject telaGameOver;
    private bool colisaoDetectada = false;
    
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!colisaoDetectada)
        {
            if (Input.mousePresent && Input.GetMouseButtonDown(0))
            {
                posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                posicaoMouse.z = 0;

                if (GetComponent<Collider2D>().OverlapPoint(posicaoMouse))
                {
                    tempoClique = Time.time;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (gameObject.CompareTag("Player"))
                {
                    Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
                    Vector3 desiredPosition = transform.position + mouseDelta / 4f;

                    // Verificar colisão com as paredes
                    Collider2D[] colliders = Physics2D.OverlapPointAll(desiredPosition);
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag("wall"))
                        {
                            // Impedir que o objeto entre na parede
                            return;
                        }
                    }

                    // Atualizar a posição do objeto
                    transform.position = desiredPosition;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Policial") &&
        gameObject.layer == LayerMask.NameToLayer("Inimigo"))

        {
            Debug.Log("Colidiu com um objeto da layer 'Policial'!");

            // Reiniciar o jogo
            ReiniciarJogo();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Saida") &&
        gameObject.layer == LayerMask.NameToLayer("Inimigo"))

        {
            Debug.Log("o ladaro escapou!");

            // Reiniciar o jogo
            ReiniciarJogo();
        }
    }

    


    private void ReiniciarJogo()
    {
        // Recarregar a cena atual para reiniciar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
