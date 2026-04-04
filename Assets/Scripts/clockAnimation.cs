using UnityEngine;

public class clockAnimation : MonoBehaviour
{
    private float waitTimer = 0f;
    private float waitTimeout = 3f;
    private bool waitFinished = false;
    private float transitionTimer = 0f;
    private float transitionTimeout = 1f;
    [SerializeField] private AudioManager audioReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitFinished)
        {
            waitTimer += Time.deltaTime;
        }
        else
        {
            transitionTimer += Time.deltaTime;
        }
        if (waitTimer > waitTimeout && !waitFinished)
        {
            this.GetComponent<ClockManager>().ClockUpdate(0);
            audioReference.UpdateVolume("clockTick1", 0);
            audioReference.PlaySound("start1");
            waitFinished = true;
        }
        if (transitionTimer > transitionTimeout)
        {
            GameManager.instance.ChangeSceneTo("WinScreen");
        }
    }
}
