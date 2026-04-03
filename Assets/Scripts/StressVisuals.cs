using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StressVisuals : MonoBehaviour
{
    private int prevLevel = 0;
    private int curLevel = 0;
    //public Sprite[] mainProgression;
    //public Sprite[] stressLoops;
    //private Sprite[] animationQueue;
    private Queue<Sprite> animationQueue = new Queue<Sprite>();
    private bool updateNeeded = false;
    //each stress transition
    public Sprite[] anim0To1;
    public Sprite[] anim1To2;
    public Sprite[] anim2To1;
    public Sprite[] anim1To0;
    //each stress loop
    public Sprite[] stress1Loop;
    public Sprite[] stress2Loop;
    //ending transition
    public Sprite[] anim2To3;
    //animation timing
    private float frameOffset = 0.15f;
    private float frameTimer = 0;

    void Update()
    {
        //runs an animation of stress changes
        if (updateNeeded)
        {
            //only updates timer when an animation would be playing
            frameTimer += Time.deltaTime;
           
            //print("running update/animation");

            //plays new frame each time frameOffset is surpassed
            if (frameTimer > frameOffset)
            {
                Sprite curFrame = animationQueue.Dequeue();
                gameObject.GetComponent<SpriteRenderer>().sprite = curFrame;
                frameTimer = 0f;
            }
            //diables animation running once queue empty
            if (animationQueue.Count == 0)
            {
                if (curLevel == 1)
                {
                    foreach (Sprite frame in stress1Loop)
                    {
                        animationQueue.Enqueue(frame);
                    }
                }
                else if (curLevel == 2)
                {
                    foreach (Sprite frame in stress2Loop)
                    {
                        animationQueue.Enqueue(frame);
                    }
                }
                else if (curLevel == 3)
                {
                    GameManager.instance.ChangeSceneTo("LoseScreen");
                }
                else
                {
                    updateNeeded = false;
                }   
            }

        }
    }

    public void UpdateAnimation(int newLevel)
    {
        prevLevel = curLevel;
        curLevel = newLevel;

        //print("Stress Level == " + curLevel);

        //stress goes up
        if (curLevel > prevLevel)
        {
            //print("stress went up");
            //up to stress 1
            if (curLevel == 1)
            {
                foreach (Sprite frame in anim0To1)
                {
                    animationQueue.Enqueue(frame);
                }
            }
            //up to stress 2
            else if (curLevel == 2)
            {
                foreach (Sprite frame in anim1To2)
                {
                    animationQueue.Enqueue(frame);
                }
            }
            else if (curLevel == 3)
            {
                foreach (Sprite frame in anim2To3)
                {
                    animationQueue.Enqueue(frame);
                }
            }
        }

        //stress goes down
        else if (curLevel < prevLevel)
        {
            //print("stress went down");
            //down to stress 1
            if (curLevel == 1)
            {
                foreach (Sprite frame in anim2To1)
                {
                    animationQueue.Enqueue(frame);
                }
            }
            //down to stress 0
            else if (curLevel == 0)
            {
                foreach (Sprite frame in anim1To0)
                {
                    animationQueue.Enqueue(frame);
                }
            }
        }
        //print(animationQueue);
        updateNeeded = true;
    }
}
