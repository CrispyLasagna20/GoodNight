using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //anomaly management
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
    
    //win & lose timer
    [SerializeField] private float LoseTimer = 15;
    private bool runLoseTimer = false;
    [SerializeField] private float WinTimer = 190; //3 mintues + 10 second grace period
    public GameObject clock;

    void Start()
    {
        for (int i = 0; i < anomalyObjects.Length; i++)
        {
            anomalyObjects[i].GetComponent<AnomalyFunction>().SetupTimer(Random.Range(10.0f, 40.0f));
            clock.GetComponent<ClockManager>().ClockUpdate(0);
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
                //Time.timeScale = 0;
                //print("YOU JUST LOST THE GAME");
                GameManager.instance.ChangeSceneTo("LoseScreen");
            }
        }
        WinTimer -= Time.deltaTime;
        if (WinTimer <= 0)
        {
            //Time.timeScale = 0;
            //print("YIPEE YOU WIN");
            GameManager.instance.ChangeSceneTo("WinScreen");
        }
        else if (WinTimer < 60)
        {
            clock.GetComponent<ClockManager>().ClockUpdate(3);

        }
        else if (WinTimer < 120)
        {
            clock.GetComponent<ClockManager>().ClockUpdate(2);

        }
        else if (WinTimer < 180)
        {
            clock.GetComponent<ClockManager>().ClockUpdate(1);

        }
    }

    public void UpdateCount(int count)
    {
        //updates count and checks if stress needs updated
        anomalyCount += count;
        RunUpdate();
    }

    public void UpdatePenalty(int penalty)
    {
        //adds penalty and checks if stress needs updated
        globalPenalty += penalty;
        RunUpdate();
    }

    public void RunUpdate()
    {
        //enables a check for stress level & starts LoseTimer if count exceeds high min
        needsUpdate = true;
        if ((anomalyCount + globalPenalty) >= highMin) runLoseTimer = true;
        else runLoseTimer = false;
    }
}
