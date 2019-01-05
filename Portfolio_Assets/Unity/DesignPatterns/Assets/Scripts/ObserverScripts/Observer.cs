using UnityEngine;
using System.Collections;

namespace ObserverPattern 
{
    //Wants to know when another object does something interesting 
    public abstract class Observer
    {
        public abstract void OnNotify(Color myColor);
    }

    public class shape : Observer 
    {
        //The shape gameobject which will do something
        GameObject shapeObj;
        //What will happen when this shape gets an event
        ShapeEvents shapeEvent;

        public shape(GameObject shapeObj, ShapeEvents shapeEvent)
        {
            this.shapeObj = shapeObj;
            this.shapeEvent = shapeEvent;
        }

        //What the shape will do if the event fits it (will always fit but you will probably change that on your own)
        public override void OnNotify(Color myColor)
        {
            ChangeColor(myColor, shapeEvent.GetDelay());
            
        }

        //The shape will change color and delay
        void ChangeColor(Color myColor, float delayTime)
        {
            shapeObj.GetComponent<ObserverObject>().newColor = myColor;
            shapeObj.GetComponent<ObserverObject>().delayTime = delayTime;
                        
        }


    }
}
