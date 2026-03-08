using UnityEngine;

public class TriggerForPelican : MonoBehaviour
{
    //Accede al transform del pelicano
    [SerializeField] Transform pelican;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PelicanSet(Movimiento player)
    {
        //Recibe el parametro del jugador y lo coloca encima del pelicano
        player.transform.position = pelican.position + new Vector3(0, 1, 0);
        //Se hace hijo del pelicano
        player.transform.parent = pelican.transform;
        //Se hace el cuerpo kinematico
        player.GetRigidBody().bodyType = RigidbodyType2D.Kinematic;
        //Se hace cero la velocidad lineal
        player.GetRigidBody().linearVelocity = Vector3.zero;
        //Se desactiva el script del jugador
        player.enabled = false;
        //Se activa el control del pelicano
        PelicanControl p = pelican.gameObject.GetComponent<PelicanControl>();
        p.enabled = true;

    }
    public void PlayerSet(Movimiento player)
    {
        //Recibe el parametro del jugador
        //Se deja de hacer hijo del pelicano
        player.transform.parent = null;
        //Se vuelve el cuerpo dinamico
        player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
        //Se activa el script del jugador
        player.enabled = true;
        //Se desactiva este trigger
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
