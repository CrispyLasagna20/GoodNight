using UnityEngine;

public class AnomalyFunction : MonoBehaviour
{
    public Sprite[] anomalyArray;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = anomalyArray[Random.Range(1,anomalyArray.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
