using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private GameObject player;

    private bool isStunned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (isStunned) return;

        if (player != null)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.AddForce(dir * speed);
        }
    }

    public void Stun(float duration)
    {
        StopAllCoroutines(); // กัน stun ซ้อน
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(duration);

        isStunned = false;
    }
}