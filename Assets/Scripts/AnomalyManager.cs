using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //anomalies
    public GameObject[] anomalyObjects;
    private int anomalyCount;
    private bool needsUpdate = false;
    private bool stressChanged = false;
    private int stress = 0;
    private int lowMax = 4;
    private int midMin = 5;
    private int midMax = 8;
    private int highMin = 9;
    
    void Start()
    {

    }

    void Update()
    {
        if (needsUpdate)
        {
            //changes stress depending on anomaly count
            if (stress != 0 && anomalyCount == lowMax)
            {
                stress = 0;
                stressChanged = true;
            }
            else if (stress != 1 && anomalyCount == midMin || anomalyCount == midMax)
            {
                stress = 1;
                stressChanged = true;
            }
            else if (stress != 2 && anomalyCount == highMin)
            {
                stress = 2;
                stressChanged = true;
            }
            //updates all stress if needed
            if (stressChanged)
            {
                for (int i = 0; i < anomalyObjects.Length; i++) anomalyObjects[i].GetComponent<AnomalyFunction>().UpdateStress(stress);
            }
            needsUpdate = false;
        }
    }

    public void UpdateCount(int count)
    {
        //updates count and checks if stress needs updated
        anomalyCount += count;
        needsUpdate = true;
    }

    //method that ups count and sets needsUpdate to true
}
