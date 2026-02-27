using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //anomalies
    public GameObject[] anomalyObjects;
    private int anomalyCount;
    private bool needsUpdate;
    
    void Start()
    {
        //testing
        anomalyCount = anomalyObjects.Length;
        needsUpdate = true;
    }

    void Update()
    {
        if (anomalyCount == 1 && needsUpdate)
        {
            for (int i = 0; i < anomalyObjects.Length; i++)
            {
                anomalyObjects[i].GetComponent<AnomalyFunction>().UpdateStress(1);
            }
        }
    }

    //method that ups count and sets needsUpdate to true
}
