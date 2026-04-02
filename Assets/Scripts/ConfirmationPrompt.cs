using UnityEngine;
using TMPro;

public class ConfirmationPrompt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string prePrompt = "Good Night ";
    private float timer = 0f;
    private float timeout = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tempColor = this.GetComponent<TextMeshProUGUI>().color;
        tempColor.r -= 0.002f;
        tempColor.g -= 0.002f;
        tempColor.b -= 0.002f;
        if (tempColor.a > 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (timer > timeout)
        {
            tempColor.a -= 0.001f;
        }
        this.GetComponent<TextMeshProUGUI>().color = tempColor;
    }

    public void RunConfirm(string anomalyName)
    {
        //changes text to match anomaly
        string tempText = prePrompt + anomalyName;
        this.GetComponent<TextMeshProUGUI>().text = tempText;
        //reset fade timer
        timer = 0;
        //makes fully visible
        Color tempColor = this.GetComponent<TextMeshProUGUI>().color;
        tempColor = new Color(255, 255, 255, 1); //.a = 1;
        this.GetComponent<TextMeshProUGUI>().color = tempColor;
    }
}
