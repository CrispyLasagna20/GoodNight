using TMPro;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{ 
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Vector3[] coords;
    private int curIndex = 1;
    [SerializeField] private AudioManager audioReference;
    private string[] clicks = { "click1", "click2" };
    private bool buttonPressed = false;
    private float releaseTimer = 0f;
    private float releaseTimeout = 0.2f;
    private bool clickable = false;
    private bool runFlash = false;
    private bool fadeIn = false;
    [SerializeField] private TutorialText textReference;

    void Update()
    {
        if (buttonPressed)
        {
            releaseTimer += Time.deltaTime;
        }
        if (releaseTimer >= releaseTimeout)
        {
            buttonPressed = false;
            releaseTimer = 0;
        }
        if (runFlash)
        {
            Color tempColor = this.GetComponent<SpriteRenderer>().color;
            if (!fadeIn)
            {
                tempColor.a -= 0.003f;
            }
            else if (fadeIn)
            {
                tempColor.a += 0.003f;
            }
            if (tempColor.a <= 0)
            {
                fadeIn = true;
            }
            else if (tempColor.a >= 1)
            {
                fadeIn = false;
            }
            this.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    public void OnMouseDown()
    {
        if (clickable)
        {
            string curClick = clicks[Random.Range(0, 1)];
            if (curIndex != 3)
            {
                audioReference.UpdatePitch(curClick, Random.Range(0.9f, 1.1f));
                audioReference.PlaySound(curClick);
            }
            else
            {
                audioReference.PlaySound("correct1");
            }
            buttonPressed = true;
        }
    }

    public void OnMouseUp()
    {
        if (buttonPressed && clickable)
        {
            if (curIndex == 1 || curIndex == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            if (curIndex == 3)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
            }
            if (curIndex == 4)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            clickable = false;
            runFlash = false;
            Color tempColor = this.GetComponent<SpriteRenderer>().color;
            tempColor.a = 1;
            this.GetComponent<SpriteRenderer>().color = tempColor;
            curIndex++;
                textReference.NextPhrase();
        }
    }

    public void MakeVisible()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[curIndex];
        gameObject.GetComponent<Transform>().position = coords[curIndex];
        clickable = true;
        runFlash = true;
    }
}
