using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StressVisuals : MonoBehaviour
{
    private int prevLevel = 0;
    private int curLevel = 0;
    public Sprite[] mainProgression;
    public Sprite[] stressLoops;
    //private Sprite[] animationQueue;
    private Queue<Sprite> animationQueue;
    private bool updateNeeded = false;
    //each stress transition
    private Queue<Sprite> anim0To1;
    private Queue<Sprite> anim1To2;
    private Queue<Sprite> anim2To1;
    private Queue<Sprite> anim1To0;
    //each stress loop
    private Queue<Sprite> stress1Loop;
    private Queue<Sprite> stress2Loop;
    //animation timing
    private float frameOffset = 0.5f;
    private float frameTimer = 0;

    void Start()
    {
        print(animationQueue);
    }

    void Update()
    {
        //runs an animation of stress changes
        if (updateNeeded)
        {
            frameTimer += Time.deltaTime;

            print("running update/animation");
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
                updateNeeded = false;
            }
        }
    }

    public void UpdateAnimation(int newLevel)
    {
        prevLevel = curLevel;
        curLevel = newLevel;

        print("Stress Level == " + curLevel);

        //stress goes up
        if (curLevel > prevLevel)
        {
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
        }

        //stress goes down
        else if (curLevel < prevLevel)
        {
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
        print(animationQueue);
        updateNeeded = true;
    }
}

//add something that automatically fills queues on start
