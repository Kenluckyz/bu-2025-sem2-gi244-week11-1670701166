using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public Rigidbody box;

    private Coroutine goodbyeRoutine;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        // InvokeRepeating(nameof(RandomSpawn), 0, 5f);

        // StartCoroutine(Hello());
        //goodbyeRoutine =StartCoroutine(Goodbye());
        //StartCoroutine(MoveBox());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }
    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator MoveBox()
    {
        while (true)
        {
            box.linearVelocity = 10 * Vector3.up;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.right;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.down;
            yield return new WaitForSeconds(3);
            box.linearVelocity = 10 * Vector3.left;
            yield return new WaitForSeconds(3);
        }
    }

    private void Update()
    {
        if (Time.time > 1)
        {
            if(goodbyeRoutine != null)
            {
                StopCoroutine(goodbyeRoutine);
            }
        }
    }
    IEnumerator Goodbye()
    {
        while (true)
        {
            Debug.Log("Bye" + Time.frameCount + " " + Time.time);
            //yield return new WaitForSeconds(1);
            yield return null;

            if(Time.time > 5)
                yield break;

            //StartCoroutine(Hello());
            yield return Hello();
        }
    }
    IEnumerator Hello()
    {
        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
    }
}
