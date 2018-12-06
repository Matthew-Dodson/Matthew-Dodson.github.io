using UnityEngine;
using System.Collections;


namespace ObserverPattern
{
    //Events
    public abstract class ShapeEvents
    {
        public abstract float GetDelay();
    }


    public class ChangeColorNow : ShapeEvents
    {
        public override float GetDelay()
        {
            return 0f;
        }
    }


    public class ChangeColorDelay : ShapeEvents
    {
        public override float GetDelay()
        {
            return 10f;
        }
    }

}
