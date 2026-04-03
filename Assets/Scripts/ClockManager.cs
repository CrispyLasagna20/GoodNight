using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public Sprite[] clocks;
    [SerializeField] private AudioManager audioReference;

    void Start()
    {
        audioReference.PlaySound("clockTick1");
    }

    //updates clock depending on time
    public void ClockUpdate(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = clocks[index];
    }
}
