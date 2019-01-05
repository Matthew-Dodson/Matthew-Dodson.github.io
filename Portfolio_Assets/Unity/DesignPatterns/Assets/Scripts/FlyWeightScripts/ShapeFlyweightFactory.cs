using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFlyweightFactory : MonoBehaviour
{
    private static List<GameObject> shapeList = new List<GameObject>();
    private static List<string> hashvalues = new List<string>();
    private static string hashstring;

    public void CreateShape(string shape, string size, Color color)
    {
        hashstring = shape + size + color;
        //Debug.Log("hashstring is " + hashstring);

        int index;
        GameObject newshape = null;
        Vector3 vsize = new Vector3();

        switch (size)
        {
            case "small":
                vsize = new Vector3(1f, 1f, 1f);
                break;
            case "medium":
                vsize = new Vector3(2f, 2f, 2f);
                break;
            case "large":
                vsize = new Vector3(3f, 3f, 3f);
                break;

        }
        //Debug.Log("count is " + shapeList.Count);

        if (shapeList.Count != 0)
        {
            index = searchList(hashstring);

            if (index >= 0)
            {
                //Positive value indicates that it found the hashstring -- means it already exists
                if (index > 1)
                {
                    //Swap with item at index 0
                    swapShapes(1, 0);
                    swapShapes(index, 0);
                    //Debug.Log("found the shape at index = " + index);
                }
            }
            else
            {
                //Negative value indicates that it didn't find the hashstring -- means it doesn't exist yet
                if (shapeList.Count == 1)
                {
                    newshape = genPrimitive(shape);
                    shapeList.Add(newshape);
                    hashvalues.Add(hashstring);
                    newshape.transform.position = new Vector3(5f, 2.5f, -45f);
                    newshape.transform.localScale = vsize;
                    newshape.GetComponent<Renderer>().material.color = color;
                    swapShapes(1, 0);
                    //Debug.Log("Inserting second shape");

                }
                else
                {
                    // Add item to List and swap with object at index 0
                    //Debug.Log("Did not find the shape: index = " + index);

                    int newindex = shapeList.Count;
                    newshape = genPrimitive(shape);
                    shapeList.Add(newshape);
                    hashvalues.Add(hashstring);
                    int position_xmodifier;
                    int position_zmodifier;
                    int multiplier = (newindex - 2) / 5;

                    position_xmodifier = (newindex - 2) - (multiplier * 5);
                    position_zmodifier = (newindex -2) / 5;

                    newshape.transform.position = new Vector3(-15f + position_xmodifier*5f, 4f, -15f - position_zmodifier*5f);

                    newshape.transform.localScale = vsize;
                    newshape.GetComponent<Renderer>().material.color = color;

                    swapShapes(1, 0);
                    swapShapes(newindex, 0);

                }

            }
        }
        else
        {
            //List is empty
            newshape = genPrimitive(shape);
            shapeList.Add(newshape);
            hashvalues.Add(hashstring);
            newshape.transform.position = new Vector3(-1f, 2.5f, -45f);
            newshape.transform.localScale = vsize;
            newshape.GetComponent<Renderer>().material.color = color;
            //Debug.Log("Inserting first shape");

        }
    }

    //Search the list for the hashstring.  Return negative if not found or return the index value
    public int searchList(string hashstring)  
    {   
        return hashvalues.IndexOf(hashstring);
    }

    //Create the primitive and return the object
    public GameObject genPrimitive(string shape) 
    {
        GameObject myshape = null;
        switch (shape)
        {
            case "cube":
                myshape = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case "sphere":
                myshape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case "cylinder":
                myshape = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
        }
        return myshape;
    }

    public void swapShapes(int index, int index2)
    {
        GameObject tempObj = new GameObject();
        string tempHash;
        Vector3 tempVector3 = new Vector3();

        tempObj = shapeList[index2];
        tempHash = hashvalues[index2];
        tempVector3 = shapeList[0].transform.position;
        //Debug.Log("tempHash is " + tempHash);

        shapeList[index2] = shapeList[index];
        hashvalues[index2] = hashvalues[index];
        //Debug.Log("hashvalues[index2] is " + hashvalues[index2]);

        shapeList[index] = tempObj;
        shapeList[index].transform.position = shapeList[index2].transform.position;
        hashvalues[index] = tempHash;
        //Debug.Log("hashvalues[index] is " + hashvalues[index]);

        shapeList[index2].transform.position = tempVector3;
        //Debug.Log("swapping index = " + index + " with index2 = " + index2);

    }

}