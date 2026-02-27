using UnityEngine;

public class AnomalyFunction : MonoBehaviour
{
    public Sprite[] anomalySprites;
    private float timer;
    private float timeout = 5f;
    private bool canSpawn = true;
    private bool viewing = false;

    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
        }
        if (timer > timeout && !viewing)
        {
            SpawnAnomaly();
        }
    }
    public void SpawnAnomaly()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[Random.Range(1,anomalySprites.Length)];
        canSpawn = false;
        //print("spawned == can't spawn");
        timer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        viewing = true;
        //print("collision == can't spawn");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        viewing = false;
        //print("no collision == can spawn");
    }

    public void OnMouseDown()
    {
        print("CLICK DETECTED");
        if (!canSpawn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[0];
            canSpawn = true;
        }
    }
}
