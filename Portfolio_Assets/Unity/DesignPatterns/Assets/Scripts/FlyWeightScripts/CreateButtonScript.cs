using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtonScript : MonoBehaviour
{
    // Shape Toggles
    public Toggle sphereCheck;  public Toggle cubeCheck;  public Toggle cylinderCheck;

    // Color Toggles
    public Toggle blueCheck;  public Toggle greenCheck;  public Toggle redCheck;

    // Size Toggles
    public Toggle smallCheck;  public Toggle mediumCheck;  public Toggle largeCheck;

    void Start()
    {
        
    }

    public void OnClick()
    {
        string shape = "";
        Color color;
        string size = "";
        
        // Checking which shape toggle is on
        if(sphereCheck.isOn)
        {
            shape = "sphere";
        }
        else if (cubeCheck.isOn)
        {
            shape = "cube";
        }
        else // cylinderCheck.isOn
        {
            shape = "cylinder";
        }

        //Checking which color toggle is on
        if (blueCheck.isOn)
        {
            color = Color.blue;
        }
        else if (greenCheck.isOn)
        {
            color = Color.green;
        }
        else // redCheck.isOn
        {
            color = Color.red;
        }

        // Checking which size toggle is on
        if (smallCheck.isOn)
        {
            size = "small";
        }
        else if (mediumCheck.isOn)
        {
            size = "medium";
        }
        else // largeCheck.isOn
        {
            size = "large";
        }

        GetComponent<ShapeFlyweightFactory>().CreateShape(shape, size, color);

    }

    /* Test code
    public void CreateShape()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 1f, 0);
    }
    */
}
