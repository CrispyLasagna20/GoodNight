using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //anomalies
    public GameObject[] anomalyObjects;
    [SerializeField] private int anomalyCount;
    private bool needsUpdate = false;
    private bool stressChanged = false;
    [SerializeField] private int stress = 0;
    private int lowMax = 4;
    private int midMin = 5;
    private int midMax = 8;
    private int highMin = 9;
    [SerializeField] private int globalPenalty = 0;
    [SerializeField] private float LoseTimer = 15;
    private bool runLoseTimer = false;
    [SerializeField] private float WinTimer = 180;

    void Start()
    {
        for (int i = 0; i < anomalyObjects.Length; i++)
        {
            anomalyObjects[i].GetComponent<AnomalyFunction>().SetupTimer(Random.Range(7.0f, 30.0f));
        }
    }

    void Update()
    {
        if (needsUpdate)
        {
            //changes stress depending on anomaly count
            if (stress != 0 && anomalyCount == (lowMax - globalPenalty))
            {
                stress = 0;
                stressChanged = true;
            }
            else if (stress != 1 && anomalyCount == (midMin - globalPenalty) || anomalyCount == (midMax - globalPenalty))
            {
                stress = 1;
                stressChanged = true;
            }
            else if (stress != 2 && anomalyCount == (highMin - globalPenalty))
            {
                stress = 2;
                stressChanged = true;
            }
            //updates all stress if needed
            if (stressChanged)
            {
                for (int i = 0; i < anomalyObjects.Length; i++)
                {
                    anomalyObjects[i].GetComponent<AnomalyFunction>().UpdateStress(stress);
                }
            }
            needsUpdate = false;
        }
        if (runLoseTimer)
        {
            LoseTimer -= Time.deltaTime;
            if (LoseTimer <= 0)
            {
                Time.timeScale = 0;
                print("YOU JUST LOST THE GAME");
            }
        }
        WinTimer -= Time.deltaTime;
        if (WinTimer <= 0)
        {
            Time.timeScale = 0;
            print("YIPEE YOU WIN");
        }
    }

    public void UpdateCount(int count)
    {
        //updates count and checks if stress needs updated
        anomalyCount += count;
        needsUpdate = true;
        if ((anomalyCount + globalPenalty) >= anomalyObjects.Length) runLoseTimer = true;
        else runLoseTimer = false;
    }

    public void UpdatePenalty(int penalty)
    {
        globalPenalty += penalty;
    }
}
