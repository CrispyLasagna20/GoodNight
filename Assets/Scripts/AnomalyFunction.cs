using UnityEngine;

public class AnomalyFunction : MonoBehaviour
{
    public Sprite[] anomalySprites;
    private float timer;
    private float timeout = 5f;
    private bool canSpawn;

    void Start()
    {
        canSpawn = true;
    }

    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
        }
        if (timer > timeout)
        {
            SpawnAnomaly();
        }
    }
    public void SpawnAnomaly()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[Random.Range(1,anomalySprites.Length)];
        canSpawn = false;
        print("spawned == can't spawn");
        timer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canSpawn = false;
        print("collision == can't spawn");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canSpawn = true;
        print("no collision == can spawn");
    }
}
