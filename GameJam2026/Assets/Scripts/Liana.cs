using UnityEngine;
using UnityEngine.InputSystem;

public class Liana : MonoBehaviour
{
    //Referencia del pivot de la liana
    [SerializeField] Transform liana;
    //Referencia del script del jugador
    [SerializeField] Movimiento player;
    //Lo que dura la rotacion
    [SerializeField] float maxRotationDuration = 3;
    //Fuerza de impulso del jugador
    [SerializeField] float forceImpulse = 1000;
    //Comprueba en que direccion se mueve la liana
    bool left = false;
    //Comprueba el porcentaje de rotacion
    float currentRotation;
    //Son los limites hasta donde puede rotar la liana
    Vector3 RotationMax = new Vector3(0, 0, 45);
    Vector3 RotationMin = new Vector3(0, 0, -45);
    //Guarda el sistema de inputs de la liana
    PlayerInput input;

    private void Awake()
    {
        input = GetComponentInChildren<PlayerInput>();
        //Se desactiva por defecto
        input.enabled = false;
    }
    //Es para el control del jugador en la liana
    public void OnLiana(InputAction.CallbackContext contextLiana)
    {
        //Se impulsa al jugador hacia la derecha si la liana se mueve hacia la derecha
        if (contextLiana.performed && !left)
        {
            input.enabled = false;
            player.transform.parent = null;
            player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
            player.GetRigidBody().AddForce(Vector2.right * forceImpulse, ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.identity;
            player.enabled = true;
        }
        //Se impulsa al jugador hacia la izquierda si la liana se mueve hacia la izquierda
        else if(contextLiana.performed && left)
        {
            input.enabled = false;
            player.transform.parent = null;
            player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
            player.GetRigidBody().AddForce(Vector2.left * forceImpulse, ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.identity;
            player.enabled = true;
        }
    }
    //Se utiliza para establecer al jugador lo necesario para cambiar entre el control del jugador y el de la liana
    internal void SetPlayer(Movimiento _player)
    {
        player = _player;
        player.transform.parent = liana.transform;
        input.enabled = true;
        player.GetRigidBody().bodyType = RigidbodyType2D.Kinematic;
        player.GetRigidBody().linearVelocity = Vector3.zero;
        player.enabled = false;
    }
    void Update()
    {
        //Se calcula el porcentaje de la rotacion en cada frame
        currentRotation += Time.deltaTime;
        float t = currentRotation / maxRotationDuration;
        if (!left)
        {
            //Se mueve en cada frame desde la posicion de la liana hasta la rotacion máxima en el intervalo dado
            liana.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMax), t);
            if (liana.rotation.z >= Quaternion.Euler(RotationMax).z)
            {
                //Si se ha llegado al limite de rotacion se resetea la rotacion y se cambia para que rote hacia la derecha
                currentRotation = 0;
                left = true;
            }
        }
        else 
        {
            //Se mueve en cada frame desde la posicion de la liana hasta la rotacion minima en el intervalo dado
            liana.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMin), t);
            if (liana.rotation.z <= Quaternion.Euler(RotationMin).z)
            {
                //Si se ha llegado al limite de rotacion se resetea la rotacion y se cambia para que rote hacia la izquierda
                currentRotation = 0;
                left = false;
            }
        }
    }
}
