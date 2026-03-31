using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public Sprite[] clocks;

    //updates clock depending on time
    public void ClockUpdate(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = clocks[index];
    }
}
