using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text goldCountText;
    public Text silverCountText;
    public Text woodCountText;
    public Text oreCountText;
    public Text fursCountText;
    public Text dyeCountText;
    public Text foodCountText;
    public Text candyCountText;
    public Text drinksCountText;

    private Items myItems;

    private InventoryPouch MoneySubPouch;
    private InventoryPouch CraftingItemsSubPouch;
    private InventoryPouch EatablesSubPouch;

    // Use this for initialization
    void Start ()
    {

        //create subPouchs
        MoneySubPouch = new AddMoney();
        CraftingItemsSubPouch = new AddCraftingItem();
        EatablesSubPouch = new AddEatables();

        //chain them together
        MoneySubPouch.SetNextSubPouch(CraftingItemsSubPouch);
        CraftingItemsSubPouch.SetNextSubPouch(EatablesSubPouch);

        /* Test Code
        myItems = new Items(PouchItems.Gold.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
        */


        
    }
	
	// Update is called once per frame
	void Update ()
    {
        goldCountText.text = "" + PouchCnts.get_GoldCnt;
        silverCountText.text = "" + PouchCnts.get_SilverCnt;
        woodCountText.text = "" + PouchCnts.get_WoodCnt;
        oreCountText.text = "" + PouchCnts.get_OreCnt;
        fursCountText.text = "" + PouchCnts.get_FurCnt;
        dyeCountText.text = "" + PouchCnts.get_DyeCnt;
        foodCountText.text = "" + PouchCnts.get_FoodCnt;
        candyCountText.text = "" + PouchCnts.get_CandyCnt;
        drinksCountText.text = "" + PouchCnts.get_DrinkCnt;
    }

    public void AddGold()
    {
        myItems = new Items(PouchItems.Gold.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddSilver()
    {
        myItems = new Items(PouchItems.Silver.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddWood()
    {
        myItems = new Items(PouchItems.Wood.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddOre()
    {
        myItems = new Items(PouchItems.Ore.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddFurs()
    {
        myItems = new Items(PouchItems.Furs.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddDye()
    {
        myItems = new Items(PouchItems.Dye.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddFood()
    {
        myItems = new Items(PouchItems.Food.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddCandy()
    {
        myItems = new Items(PouchItems.Candy.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddDrinks()
    {
        myItems = new Items(PouchItems.Drink.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }

    public void AddTrash()
    {
        myItems = new Items(PouchItems.Trash.ToString(), 1);
        MoneySubPouch.AddItemtoSubPouch(myItems);
    }
}
