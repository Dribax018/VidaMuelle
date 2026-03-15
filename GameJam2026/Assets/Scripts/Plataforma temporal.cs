using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plataformatemporal : MonoBehaviour
{

    [SerializeField] private float tiempoespera;

    private Rigidbody2D rb2D;

    [SerializeField] private float velocidadrotacion;

    private Animator animator;
    private bool caida = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Caida(other));
        }



    }
    private IEnumerator Caida(Collision2D other)
    {
        yield return new WaitForSeconds(tiempoespera);
        caida = true;
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.AddForce(new Vector2(0.1f, 0));
    }
}
