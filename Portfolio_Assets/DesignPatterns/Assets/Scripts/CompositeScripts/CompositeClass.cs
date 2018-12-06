// Structural Pattern:COMPOSITE
using System;
using System.Collections;

//the single interface for primitives & composite types.|
abstract class Component
{
    abstract public void AddChild(Component c);
    abstract public void Traverse();
    abstract public bool isLeaf();
    abstract public string getValue();
    abstract public void getChildren();
    abstract public int numChildren();
    abstract public Component getChild(int child);
}
//A primitive type.
class Leaf : Component
{
    private string value = "";
    private bool isLeafNode;

    public Leaf(string str)
    {
        value = str;
        isLeafNode = true;
    }
    public override void AddChild(Component c)
    {
        //no action; This method is not necessary for Leaf
    }
    public override void getChildren()
    {
        //no action; This method is not necessary for Leaf
    }
    public override Component getChild(int child)
    {
        return null;
    }

    public override int numChildren()
    {
        return 0;
    }
    public override void Traverse()
    {
        Console.WriteLine("Leaf:" + value);
    }
    public override bool isLeaf()
    {
        return isLeafNode;
    }
    public override string getValue()
    {
        return value;
    }

}
//A composite type.
class Composite : Component
{
    private string value = "";
    private bool isLeafNode;
    private ArrayList ComponentList = new ArrayList();

    public Composite(string str)
    {
        value = str;
        isLeafNode = false;
    }
    public override void AddChild(Component c)
    {
        ComponentList.Add(c);
    }
    public override void Traverse()
    {
        Console.WriteLine("Composite:" + value);

        foreach (Component c in ComponentList)
        {

            c.Traverse();
        }
    }
    public override bool isLeaf()
    {
        return isLeafNode;
    }
    public override string getValue()
    {
        return value;
    }
    public override void getChildren()
    {
        foreach (Component c in ComponentList)
        {
            Console.WriteLine("Composite:" + c.getValue());
        }

    }
    public override int numChildren()
    {
        return ComponentList.Count;
    }

    public override Component getChild(int child)
    {
        Component c = (Component)ComponentList[child - 1];
        return c;
    }

}
