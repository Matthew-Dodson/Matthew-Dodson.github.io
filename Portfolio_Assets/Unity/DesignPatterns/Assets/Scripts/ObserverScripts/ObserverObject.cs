using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class ObserverObject : MonoBehaviour
    {
        //Subject mysubject = new Subject();
        public Color myColor;
        public Color newColor;
        public float delayTime = 0f;

        private Renderer rend;
        private IEnumerator coroutine;

        // Use this for initialization
        void Start()
        {
            rend = GetComponent<Renderer>();
            myColor = rend.material.color;

        }

        // Update is called once per frame
        void Update()
        {
            if(myColor != newColor)
            {
                myColor = rend.material.color;
                coroutine = TimerEnumerator(delayTime);
                StartCoroutine(coroutine);
            }
        }

        IEnumerator TimerEnumerator(float waitTime)
        {
            //Debug.Log("Starting Timer at time = " + Time.time);
            //Debug.Log("Starting Timer with waitTime = " + waitTime.ToString());
            yield return new WaitForSeconds(waitTime);
            UpdateColor();
        }

        void UpdateColor()
        {
            //Debug.Log("After waittime at time = " + Time.time);
            rend.material.color = newColor;
        }
    }
}
