using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Sprite[] buttons;
    [SerializeField] private AudioManager audioReference;
    private string[] clicks = {"click1", "click2"};
    private bool buttonPressed = false;
    private float releaseTimer = 0f;
    private float releaseTimeout = 0.2f;
    [SerializeField] private string destination;

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
            gameObject.GetComponent<SpriteRenderer>().sprite = buttons[0];
        }
    }

    public void OnMouseDown()
    {
        string curClick = clicks[Random.Range(0, 1)];
        audioReference.UpdatePitch(curClick, Random.Range(0.9f, 1.1f));
        audioReference.PlaySound(curClick);
        gameObject.GetComponent<SpriteRenderer>().sprite = buttons[1];
        buttonPressed = true; 
    }
    
    public void OnMouseUp()
    {
        if (buttonPressed)
        {
            GameManager.instance.ChangeSceneTo(destination);
        }
    }
}
