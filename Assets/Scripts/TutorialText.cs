using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
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
    private int curPhrase = 0;
    private int curIndex = 0;
    private bool printFinished = false;
    //sfx
    [SerializeField] private AudioManager audioReference;
    private string[] clicks = { "click1", "click2" };
    //sprite object
    [SerializeField] private TutorialObject objectReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string chosenPhrase = phrases[curPhrase];
        charPhrase = chosenPhrase.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        //runs waitTimer until waitTimeout reached; then allows first text to print
        if (waitTimer > waitTimeout)
        {
            waitFinished = true;
        }
        else
        {
            waitTimer += Time.deltaTime;
        }
        //checks if all text printed
        if (!printFinished && curIndex == charPhrase.Length)
        {
            printFinished = true;
            objectReference.MakeVisible();
        }
        //prints text at certain rate if wait over and more text needed
        if (!printFinished && waitFinished)
        {
            charTimer += Time.deltaTime;
        }
        else if (printFinished && curPhrase == phrases.Length-1)
        {
            transitionTimer += Time.deltaTime;
        }
        if (transitionTimer > transitionTimeout)
        {
            GameManager.instance.ChangeSceneTo("TitleScreen");
        }
        //prints text on timeout
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
    }

    public void NextPhrase()
    {
        if (waitFinished)
        {
            //resets wait
            waitTimer = 0;
            waitFinished = false;
            //clears text
            string tempText = this.GetComponent<TextMeshProUGUI>().text;
            tempText = "";
            this.GetComponent<TextMeshProUGUI>().text = tempText;
            //updates to next phrase
            printFinished = false;
            curIndex = 0;
            curPhrase++;
            string chosenPhrase = phrases[curPhrase];
            charPhrase = chosenPhrase.ToCharArray();
        }
    }
}