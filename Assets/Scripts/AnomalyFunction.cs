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
        //checks if spawning anomaly criteria is met
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
        //disallows spawning since in view
        viewing = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //allows spawning once out of view
        viewing = false;
    }

    public void OnMouseDown()
    {
        //resets anomaly if present
        if (!canSpawn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = anomalySprites[0];
            canSpawn = true;
            this.transform.parent.GetComponent<AnomalyManager>().UpdateCount(-1);
        }
    }

    public void SpawnAnomaly()
    {
        //spawns certain anomaly depending on stress
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
        //updates other stuff
        this.transform.parent.GetComponent<AnomalyManager>().UpdateCount(1);
        canSpawn = false;
        timer = 0f;
    }

    public void UpdateStress(int newStress)
    {
        //updates stress dependent on manager stress
        stress = newStress;
    }
}