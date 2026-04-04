using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //anomaly management
    public GameObject[] anomalyObjects;
    [SerializeField] private int anomalyCount;
    private bool needsUpdate = false;
    private bool stressChanged = false;
    [SerializeField] private GameObject stressVisuals;
    [SerializeField] private int stress = 0;
    private int lowMax = 4;
    private int midMin = 5;
    private int midMax = 8;
    private int highMin = 9;
    [SerializeField] private int globalPenalty = 0;

    //scene sounds
    [SerializeField] private AudioManager audioReference;
    private float volume = 0f;
    private float targetVolume = 0.05f;
    private float changeRate = 0.001f;
    private string[] stressSounds = { "darkAmbience1", "static1" };
    private string[] disturbances = {"breath1", "rawr1", "thump1", "thump2", "thump3", "whisper1"};
    private float disturbanceTimer = 0;
    private float disturbanceTimeout;

    //win & lose timer
    [SerializeField] private float LoseTimer = 15;
    private bool loseActivated = false;
    private bool runLoseTimer = false;
    [SerializeField] private float WinTimer = 190; //3 mintues + 10 second grace period
    [SerializeField] private GameObject clock;

    void Start()
    {
        for (int i = 0; i < anomalyObjects.Length; i++)
        {
            anomalyObjects[i].GetComponent<AnomalyFunction>().SetupTimer(Random.Range(10.0f, 40.0f));
            clock.GetComponent<ClockManager>().ClockUpdate(0);
        }
        audioReference.PlaySound("darkAmbience1");
        audioReference.PlaySound("darkAmbience2");
        audioReference.PlaySound("static1");
        disturbanceTimeout = Random.Range(20.0f, 50.0f);
    }

    void Update()
    {
        //adjusts volume of ambience when needed
        if (volume != targetVolume)
        {
            if (volume < targetVolume)
            {
                volume += changeRate;
            }
            else if (volume > targetVolume)
            {
                volume -= changeRate;
            }
            foreach (string sound in stressSounds)
            {
                if (sound == "static1")
                {
                    float specialVolume = volume / 10;
                    audioReference.UpdateVolume(sound, specialVolume);
                }
                else
                {
                    audioReference.UpdateVolume(sound, volume);
                }
                
            }
        }
        //updates stess when needed
        if (needsUpdate && !loseActivated)
        {
            //changes stress depending on anomaly count
            if (stress != 0 && anomalyCount == (lowMax - globalPenalty))
            {
                stress = 0;
                stressChanged = true;
                targetVolume = 0.05f;
                audioReference.UpdatePitch("clockTick1", 1f);
            }
            else if (stress != 1 && anomalyCount == (midMin - globalPenalty) || anomalyCount == (midMax - globalPenalty))
            {
                stress = 1;
                stressChanged = true;
                targetVolume = 0.2f;
                audioReference.UpdatePitch("clockTick1", 0.8f);
            }
            else if (stress != 2 && anomalyCount == (highMin - globalPenalty))
            {
                stress = 2;
                stressChanged = true;
                targetVolume = 0.8f;
                audioReference.UpdatePitch("clockTick1", 0.6f);
            }
            //updates all stress if needed
            if (stressChanged)
            {
                for (int i = 0; i < anomalyObjects.Length; i++)
                {
                    anomalyObjects[i].GetComponent<AnomalyFunction>().UpdateStress(stress);
                }
                stressVisuals.GetComponent<StressVisuals>().UpdateAnimation(stress);
            }
            needsUpdate = false;
        }
        //counts down lose timer when needed
        if (runLoseTimer)
        {
            LoseTimer -= Time.deltaTime;
            if (LoseTimer <= 0 && !loseActivated)
            {
                //Time.timeScale = 0;
                //print("YOU JUST LOST THE GAME");
                loseActivated = true;
                targetVolume = 1f;
                stressVisuals.GetComponent<StressVisuals>().UpdateAnimation(3);

            }
        }
        //counts down win timer, adjusts clock sprite, and runs win function
        WinTimer -= Time.deltaTime;
        if (WinTimer <= 0 && !loseActivated)
        {
            //Time.timeScale = 0;
            //print("YIPEE YOU WIN");
            GameManager.instance.ChangeSceneTo("WinAnim");
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
        //periodically plays random disturbance noise
        disturbanceTimer += Time.deltaTime;
        if (disturbanceTimer >= disturbanceTimeout)
        {
            string temp = disturbances[Random.Range(0, disturbances.Length)];
            print(temp);
            audioReference.PlaySoundRandomly(temp);
            disturbanceTimeout = Random.Range(20.0f, 50.0f);
            disturbanceTimer = 0;
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
        //adds penalty
        globalPenalty += penalty;
        //increase stressVisual alpha with each new penalty
        Color tempColor = stressVisuals.GetComponent<SpriteRenderer>().color;
        tempColor.a += 0.05f;
        stressVisuals.GetComponent<SpriteRenderer>().color = tempColor;
        //checks if stress needs updated
        RunUpdate();
    }

    public void RunUpdate()
    {
        //enables a check for stress level & starts LoseTimer if count exceeds high min
        needsUpdate = true;
        if (anomalyCount >= (highMin - globalPenalty)) runLoseTimer = true;
        else runLoseTimer = false;
    }
}
