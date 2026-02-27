using UnityEngine;

public class AnomalyFunction : MonoBehaviour
{
    //sprites
    public Sprite[] anomalySprites;
    //timer
    private float timer;
    private float timeout = 5f;
    //spawning
    private bool canSpawn = true;
    private bool viewing = false;
    private int stress = 0;
    private int lowMax;
    private int midMax;
    private int highMax;

    void Start()
    {
        //sets stress range max index
        lowMax = (anomalySprites.Length - 1) / 3;
        midMax = ((anomalySprites.Length - 1) / 3) * 2;
        highMax = ((anomalySprites.Length - 1) / 3) * 3;
    }

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
        if (!canSpawn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[0];
            canSpawn = true;
        }
    }

    public void SpawnAnomaly()
    {
        if (stress == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[Random.Range(midMax + 1, highMax + 1)];
        }
        if (stress == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[Random.Range(lowMax+1, midMax+1)];
        }
        if (stress == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[Random.Range(1, lowMax + 1)];
        }

        canSpawn = false;
        //print("spawned == can't spawn");
        timer = 0f;
    }

    public void UpdateStress(int newStress)
    {
        stress = newStress;
        //print("Stress Updated");
    }
}


//UPDATE ANOMALY COUNT FOR MANAGER FROM HERE NEXT