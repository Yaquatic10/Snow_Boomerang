using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("Velocidad de ida")]
    public float initialSpeed = 10f; // Velocidad inicial del boomerang al lanzarse
    public float finalSpeed = 2f;   // Velocidad mínima al final del trayecto de ida
    public float maxDistance = 5f; // Distancia máxima que recorrerá el boomerang

    [Header("Velocidad de regreso")]
    public float returnSpeed = 15f; // Velocidad máxima al regresar
    public float returnAcceleration = 0.1f; // Aceleración al regresar al jugador

    [Header("Duración máxima")]
    public float maxLifetime = 10f; // Tiempo máximo de existencia del boomerang en segundos

    private Vector3 launchPosition; // Posición desde la que se lanzó
    private bool returning = false; // Indica si el boomerang está regresando
    private Transform player; // Referencia al jugador
    public GameObject ThrowingPlayer; // Objeto que lanzó el boomerang
    public Vector3 facingPosition; // Dirección inicial del boomerang

    private float lerpProgress = 0f; // Progreso del Lerp para el viaje de ida
    private float currentSpeed; // Velocidad actual del boomerang
    private float lifetime; // Tiempo transcurrido desde que se creó el boomerang

    void Start()
    {
        player = ThrowingPlayer.transform.parent; // Referencia al jugador
        launchPosition = transform.position;

        // Determinar dirección inicial basada en la escala del jugador
        facingPosition = player.localScale.x < 0 ? Vector3.left : Vector3.right;

        // Configurar la velocidad inicial
        currentSpeed = initialSpeed;

        // Inicializar el contador de tiempo de vida
        lifetime = 0f;
    }

    void Update()
    {
        // Incrementar el tiempo de vida
        lifetime += Time.deltaTime;

        // Destruir el boomerang si excede el tiempo máximo de vida
        if (lifetime >= maxLifetime)
        {
            Destroy(gameObject);
            return;
        }

        if (!returning)
        {
            // Desacelerar progresivamente durante el viaje de ida
            float progress = Vector3.Distance(launchPosition, transform.position) / maxDistance;
            currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, progress);

            // Movimiento hacia adelante
            transform.Translate(facingPosition * currentSpeed * Time.deltaTime);

            // Verificar si alcanzó la distancia máxima
            if (Vector3.Distance(launchPosition, transform.position) >= maxDistance)
            {
                returning = true; // Activar el regreso
                lerpProgress = 0f; // Reiniciar progreso del Lerp para el regreso
            }
        }
        else
        {
            // Regreso con aceleración progresiva
            lerpProgress += Time.deltaTime * returnAcceleration;
            transform.position = Vector3.Lerp(transform.position, player.position, lerpProgress);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisiona es el ThrowingPlayer
        if (other.gameObject == ThrowingPlayer)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (ThrowingPlayer != null)
        {
            ThrowingPlayer.transform.parent.gameObject.GetComponent<PlayerController>().currentBoomerang = null;
        }
    }
}