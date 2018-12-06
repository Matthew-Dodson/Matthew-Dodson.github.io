using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;
    public Text playerDialogueText1;
    public Text playerDialogueText2;
    private Component curComp;

    // NPC Questions Texts
    private string InitialText; // NPC intro text
    private string DamageText; // NPC "damage class" question text
    private string DamageAttStyleText; // NPC damage -> attack style text
    private string BurstAttDistText; // NPC damage -> attack style -> burst damage -> attack distance text
    private string DPSAttDistText; // NPC damage -> attack style -> DPS damage -> attack distance text
    private string TankText; // NPC "tank class" question text
    private string HealerText; // NPC "healer class" question text
    private string BuffDebuffText; // NPC "buff or debuff class" text

    // Class Selected text
    // Order based on "highest left-most" leaf node
    private string KnightText;
    private string WhiteMageClericText;
    private string RogueText;
    private string RedMageText;
    private string WarriorText;
    private string RangerText;
    private string BardText;
    private string BlackMageText;

    //private Queue<string> sentences;

    // Use this for initialization
    void Start ()
    {
        // Defining the NPC's questions dialogue
        InitialText = @"Hello! I am here to help you determine what role you might like to play in a role-playing type game. 
Just answer a few simple questions and I will recommend a class (or range of classes) that fit your answers.";

        DamageText = "Do you want a class that focuses on dealing damage to enemies?";

        DamageAttStyleText = "Do you prefer to deal high amounts of damage in short bursts or deal a moderate amount of damage constantly?";

        BurstAttDistText = "Would you rather get up close and personal to burst down enemies or would you prefer to blow them away at range?";

        DPSAttDistText = "Would you rather stare down your enemies as you hack away at them or would you prefer to fire away from a distance?";

        TankText = "Do you want to play a durable class that is designed to keep enemies’ attention away from your more fragile teammates?";

        HealerText = "Do you want to play a class that is geared toward healing, restoring and reviving teammates?";

        BuffDebuffText = @"The only remaining option is a class that is either centered around empowering teammates or weakening enemies.
Which sounds better to you, buffing up teammates or debuffing enemies?";

        // Defining the Class Selected! dialogue
        KnightText = @"Play a Knight!"; // TODO: Write a description

        WhiteMageClericText = @"Play a White Mage or Cleric!";

        RogueText = @"Play a Rogue! Rogues are good at sneaking up to unsuspecting enemies and unleashing a deadly string of melee attacks 
before retreating back into the shadows to avoid retaliation. Keep in mind that rogues trade hit points and armor for stealth and mobility.";

        RedMageText = @"Play a Red Mage! Red Mages fight by casting powerful fire spells that can incinerate enemies in seconds. 
Just remember that, after casting a volley of spells, red mages are vulnerable to attack while they are recharging their magic.";

        WarriorText = @"Play a Warrior! Warriors are sturdy fighters, skilled in the usage of many different hand-to-hand weapons. 
They fiercely charge into battle, ready to face down any foe that dares to stand in their way. 
Unfortunately, they are not great when it comes to ranged combat, as they must get close to their targets before they can start dishing out damage.";

        RangerText = @"Play a Ranger! Rangers fight from afar, using bows or crossbows to keep enemies under pressure. 
While arrows and crossbow bolts are not nearly as damaging as fire magic, they are harder to dodge and leave less of an opening between shots. 
Naturally, rangers are at their best when fighting at a distance, so expect serious trouble if any enemies manage to get close.";

        BardText = @"Play a Bard!"; // TODO: Write a description

        BlackMageText = @"Play a Black Mage"; // TODO: Write a description

    }

    public void StartDialogue (Dialogue dialogue)
	{

		nameText.text = dialogue.name;

        //creating a TREE structure.
        Composite root = new Composite(DamageText);
        Composite com1 = new Composite("No"); // No to being a Damage Dealer class
        Composite com2 = new Composite("Yes"); // Yes to Damage Dealer class
        Composite com3 = new Composite(TankText);
        Composite com4 = new Composite(DamageAttStyleText);
        Composite com5 = new Composite("No"); // No to being a Tank class
        Composite com6 = new Composite("Yes"); // Yes to being a Tank class
        Composite com7 = new Composite("Burst"); // "Burst" response to being asked Attack Style of Damage Dealer class
        Composite com8 = new Composite("DPS"); // "DPS" response to being asked Attack Style of Damage Dealer class
        Composite com9 = new Composite(HealerText);
        Composite com10 = new Composite(BurstAttDistText);
        Composite com11 = new Composite(DPSAttDistText);
        Composite com12 = new Composite("No"); // No to being a Healer class
        Composite com13 = new Composite("Yes"); // Yes to being a Healer class
        Composite com14 = new Composite("Melee"); // "Melee" response to being asked Attack Distance for Burst style Damage Dealer
        Composite com15 = new Composite("Range"); // "Range" response to being asked Attack Distance for Burst style Damage Dealer
        Composite com16 = new Composite("Melee"); // "Melee" response to being asked Attack Distance for DPS style Damage Dealer
        Composite com17 = new Composite("Range"); // "Melee" response to being asked Attack Distance for DPS style Damage Dealer
        Composite com18 = new Composite(BuffDebuffText);
        Composite com19 = new Composite("Buff"); // "Buff" response to being asked Buff or Debuff based class
        Composite com20 = new Composite("Debuff"); // "Debuff" response to being asked Buff or Debuff based class

        Leaf l1 = new Leaf(KnightText);
        Leaf l2 = new Leaf(WhiteMageClericText);
        Leaf l3 = new Leaf(RogueText);
        Leaf l4 = new Leaf(RedMageText);
        Leaf l5 = new Leaf(WarriorText);
        Leaf l6 = new Leaf(RangerText);
        Leaf l7 = new Leaf(BardText);
        Leaf l8 = new Leaf(BlackMageText);


        //Build tree
        root.AddChild(com1);
        root.AddChild(com2);
        com1.AddChild(com3);
        com2.AddChild(com4);
        com3.AddChild(com5);
        com3.AddChild(com6);
        com4.AddChild(com7);
        com4.AddChild(com8);
        com5.AddChild(com9);
        com6.AddChild(l1);
        com7.AddChild(com10);
        com8.AddChild(com11);
        com9.AddChild(com12);
        com9.AddChild(com13);
        com10.AddChild(com14);
        com10.AddChild(com15);
        com11.AddChild(com16);
        com11.AddChild(com17);
        com12.AddChild(com18);
        com13.AddChild(l2);
        com14.AddChild(l3);
        com15.AddChild(l4);
        com16.AddChild(l5);
        com17.AddChild(l6);
        com18.AddChild(com19);
        com18.AddChild(com20);
        com19.AddChild(l7);
        com20.AddChild(l8);

        curComp = root;
        
        DisplayNextSentence();

    }

    public void ContinueDialogue1(Dialogue dialogue)
    {
        curComp = curComp.getChild(1).getChild(1);
        DisplayNextSentence();

    }

    public void ContinueDialogue2(Dialogue dialogue)
    {
        curComp = curComp.getChild(2).getChild(1);
        DisplayNextSentence();

    }

    public void DisplayNextSentence ()
	{
        string NPC_sentence = "";
        string Player_choice1 = "";
        string Player_choice2 = "";

        if (curComp.isLeaf() == false)
        {
            NPC_sentence = curComp.getValue();
            Player_choice1 = curComp.getChild(1).getValue();
            Player_choice2 = curComp.getChild(2).getValue();
        }
        else
        {
            NPC_sentence = curComp.getValue();
            Player_choice1 = "";
            Player_choice2 = "";

            StopAllCoroutines();
            StartCoroutine(NPCTypeSentence(NPC_sentence));
            StartCoroutine(PlayerTypeSentence1(Player_choice1));
            StartCoroutine(PlayerTypeSentence2(Player_choice2));

            EndDialogue();
            return;
        }
        StopAllCoroutines();
		StartCoroutine(NPCTypeSentence(NPC_sentence));
        StartCoroutine(PlayerTypeSentence1(Player_choice1));
        StartCoroutine(PlayerTypeSentence2(Player_choice2));
    }

    IEnumerator NPCTypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}
    IEnumerator PlayerTypeSentence1(string sentence)
    {
        playerDialogueText1.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            playerDialogueText1.text += letter;
            yield return null;
        }
    }
    IEnumerator PlayerTypeSentence2(string sentence)
    {
        playerDialogueText2.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            playerDialogueText2.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
	{

    }

}
