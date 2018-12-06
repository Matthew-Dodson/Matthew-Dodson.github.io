using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


    //define types of items to go into pouch
    public enum PouchItems
    {
        Gold, Silver,           // Money
        Wood, Ore, Furs, Dye,   //Crafting Items
        Food, Candy, Drink,      //Eatables
        Trash
    }

    public static class PouchCnts
    {
        static int gold_cnt, silver_cnt, drink_cnt, candy_cnt, food_cnt, wood_cnt, fur_cnt, dye_cnt, ore_cnt = 0;

        //Money Pouch
        public static int get_GoldCnt
        {
            get { return gold_cnt; }
        }

        public static void add_to_GoldCnt (int amount)
        {
            gold_cnt = gold_cnt + amount;
        }

        public static int get_SilverCnt
        {
            get { return silver_cnt; }
        }

        public static void add_to_SilverCnt(int amount)
        {
            silver_cnt = silver_cnt + amount;
        }

        //Eatable Pouch
        public static int get_DrinkCnt
        {
            get { return drink_cnt; }
        }

        public static void add_to_DrinkCnt(int amount)
        {
            drink_cnt = drink_cnt + amount;
        }

        public static int get_CandyCnt
        {
            get { return candy_cnt; }
        }

        public static void add_to_CandyCnt(int amount)
        {
            candy_cnt = candy_cnt + amount;
        }

        public static int get_FoodCnt
        {
            get { return food_cnt; }
        }

        public static void add_to_FoodCnt(int amount)
        {
            food_cnt = food_cnt + amount;
        }

        //Craftable Items Pouch
        public static int get_WoodCnt
        {
            get { return wood_cnt; }
        }

        public static void add_to_WoodCnt(int amount)
        {
            wood_cnt = wood_cnt + amount;
        }

        public static int get_FurCnt
        {
            get { return fur_cnt; }
        }

        public static void add_to_FurCnt(int amount)
        {
            fur_cnt = fur_cnt + amount;
        }

        public static int get_DyeCnt
        {
            get { return dye_cnt; }
        }

        public static void add_to_DyeCnt(int amount)
        {
            dye_cnt = dye_cnt + amount;
        }

        public static int get_OreCnt
        {
            get { return ore_cnt; }
        }

        public static void add_to_OreCnt(int amount)
        {
            ore_cnt = ore_cnt + amount;
        } 

    }

    public class Items
    {
        public string item  { get; protected set; }
        public int numItems { get; protected set; }

        //constructor
        public Items(string InvItem, int ItemAmt)
        {
            this.item = InvItem;
            this.numItems = ItemAmt;
        }
    }

    public interface InventoryPouch
    {
        void SetNextSubPouch(InventoryPouch nextSubPouch);
        void AddItemtoSubPouch(Items items);
    }

    public class AddMoney : InventoryPouch
    {
        protected InventoryPouch nextInSubPouch;

        public void SetNextSubPouch(InventoryPouch nextSubPouch)
        {
            this.nextInSubPouch = nextSubPouch;
        }

        public void AddItemtoSubPouch(Items request)
        {
            if (request.item == PouchItems.Gold.ToString())
            {

                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Money SubPouch");
                PouchCnts.add_to_GoldCnt(request.numItems);
                //Debug.Log("GoldCnt = " + PouchCnts.get_GoldCnt);

            }
            else if (request.item == PouchItems.Silver.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Money SubPouch");
                PouchCnts.add_to_SilverCnt(request.numItems);
                //Debug.Log("SilverCnt = " + PouchCnts.get_SilverCnt);
            }
            else if (nextInSubPouch != null)
            {
                nextInSubPouch.AddItemtoSubPouch(request);
            }
            else
            {
                Debug.Log("Error in request");
            }
        }
    }

    public class AddCraftingItem : InventoryPouch
    {
        protected InventoryPouch nextInSubPouch;

        public void SetNextSubPouch(InventoryPouch nextSubPouch)
        {
            this.nextInSubPouch = nextSubPouch;
        }

        public void AddItemtoSubPouch(Items request)
        {
            if (request.item == PouchItems.Wood.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Crafting Items SubPouch");
                PouchCnts.add_to_WoodCnt(request.numItems);
                //Debug.Log("WoodCnt = " + PouchCnts.get_WoodCnt);
            }
            else if (request.item == PouchItems.Ore.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Crafting Items SubPouch");
                PouchCnts.add_to_OreCnt(request.numItems);
                //Debug.Log("OreCnt = " + PouchCnts.get_OreCnt);
            }
            else if (request.item == PouchItems.Furs.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Crafting Items SubPouch");
                PouchCnts.add_to_FurCnt(request.numItems);
                //Debug.Log("FurCnt = " + PouchCnts.get_FurCnt);
            }
            else if (request.item == PouchItems.Dye.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Crafting Items SubPouch");
                PouchCnts.add_to_DyeCnt(request.numItems);
                //Debug.Log("DyeCnt = " + PouchCnts.get_DyeCnt);
            }
            else if (nextInSubPouch != null)
            {
                nextInSubPouch.AddItemtoSubPouch(request);
            }
            else
            {
                Debug.Log("Error in request!");
            }
        }
    }

    public class AddEatables : InventoryPouch
    {
        protected InventoryPouch nextInSubPouch;

        public void SetNextSubPouch(InventoryPouch nextSubPouch)
        {
            this.nextInSubPouch = nextSubPouch;
        }

        public void AddItemtoSubPouch(Items request)
        {
            if (request.item == PouchItems.Food.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Eatables SubPouch");
                PouchCnts.add_to_FoodCnt(request.numItems);
                //Debug.Log("FoodCnt = " + PouchCnts.get_FoodCnt);
            }
            else if (request.item == PouchItems.Candy.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Eatables SubPouch");
                PouchCnts.add_to_CandyCnt(request.numItems);
                //Debug.Log("CandyCnt = " + PouchCnts.get_CandyCnt);
            }
            else if (request.item == PouchItems.Drink.ToString())
            {
                //Debug.Log("Adding " + request.numItems + " of " + request.item + " to the Eatables SubPouch");
                PouchCnts.add_to_DrinkCnt(request.numItems);
                //Debug.Log("DrinkCnt = " + PouchCnts.get_DrinkCnt);
            }
            else if (nextInSubPouch != null)
            {
                nextInSubPouch.AddItemtoSubPouch(request);
            }
        }
    }

