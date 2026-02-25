using UnityEngine;

public class AnomalyFunction : MonoBehaviour
{
    public Sprite[] anomalyArray;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = anomalyArray[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
