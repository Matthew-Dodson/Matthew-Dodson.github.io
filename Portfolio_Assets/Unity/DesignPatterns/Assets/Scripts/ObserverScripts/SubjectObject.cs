using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class SubjectObject : MonoBehaviour
    {
        public int secsToWait = 20;
        public int totalSec = 0;
        public Color currentColor;

        public GameObject shape1;
        public GameObject shape2;
        public GameObject shape3;
        public GameObject shape4;
        public GameObject shape5;

        private Renderer rend;
        private Color[] colorList = new Color[5];

        //Will send notifications that something has happened to whoever is interested
        Subject subject = new Subject();

        // Use this for initialization
        void Start()
        {
            rend = GetComponent<Renderer>();

            colorList[0] = Color.blue;
            colorList[1] = Color.cyan;
            colorList[2] = Color.green;
            colorList[3] = Color.red;
            colorList[4] = Color.yellow;

            // Implement Code to make this dynamic
            shape shapeobj1 = new shape(shape1, new ChangeColorNow());
            shape shapeobj2 = new shape(shape2, new ChangeColorNow());
            shape shapeobj3 = new shape(shape3, new ChangeColorDelay());
            shape shapeobj4 = new shape(shape4, new ChangeColorDelay());
            shape shapeobj5 = new shape(shape5, new ChangeColorDelay());

            subject.AddObserver(shapeobj1);
            subject.AddObserver(shapeobj2);
            subject.AddObserver(shapeobj3);
            subject.AddObserver(shapeobj4);
            subject.AddObserver(shapeobj5);

            InvokeRepeating("TimerInvoke", 1, 1);

        }

        void TimerInvoke()
        {
            if (totalSec < secsToWait)
            {
                totalSec++;
            }

            else
            {
                rend.material.color = colorList[Random.Range(0, colorList.Length)];
                totalSec = 0;
                currentColor = rend.material.color;

                //Debug.Log("Calling subject.Notify");

                subject.Notify(currentColor);
            }
        }

    }
}
