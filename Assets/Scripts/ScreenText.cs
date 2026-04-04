using UnityEngine;
using TMPro;

public class LoseScreen : MonoBehaviour
{
    //timing
    private float charTimer = 0f;
    private float charTimeout = 0.1f;
    private float transitionTimer = 0f;
    [SerializeField] private float transitionTimeout;
    private float waitTimer = 0f;
    [SerializeField] private float waitTimeout;
    private bool waitFinished = false;
    //text
    [SerializeField] private string[] phrases;
    private char[] charPhrase;
    private int curIndex = 0;
    private bool printFinished = false;
    //sfx
    [SerializeField] private AudioManager audioReference;
    private string[] clicks = { "click1", "click2" };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string chosenPhrase = phrases[Random.Range(0, phrases.Length)];
        charPhrase = chosenPhrase.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimer > waitTimeout)
        {
            waitFinished = true;
        }
        else
        {
            waitTimer += Time.deltaTime;
        }
        if (curIndex == charPhrase.Length)
        {
            printFinished = true;
        }
        if (!printFinished && waitFinished)
        {
            charTimer += Time.deltaTime;
        }
        else if (printFinished)
        {
            transitionTimer += Time.deltaTime;
        }
        if (charTimer >= charTimeout)
        {
            string tempText = this.GetComponent<TextMeshProUGUI>().text;
            tempText = tempText + charPhrase[curIndex];
            curIndex++;
            this.GetComponent<TextMeshProUGUI>().text = tempText;
            string curClick = clicks[Random.Range(0, 1)];
            audioReference.UpdatePitch(curClick, Random.Range(0.9f, 1.1f));
            audioReference.PlaySound(curClick);
            charTimer = 0;
        }
        else if (transitionTimer >= transitionTimeout)
        {
            GameManager.instance.ChangeSceneTo("TitleScreen");
        }
    }
}
