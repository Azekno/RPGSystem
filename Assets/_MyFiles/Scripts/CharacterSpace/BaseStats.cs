using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{

////////////
//This is just the base script before edits for the attribute stats script is editted.
////////////





    //*/// <summary>
    /// All apart from health can be "turned off" meaning that different games can be made using this rpg system by changing the stats used
    /// i.e. Melee combat only game, so mana and any magic related stat can be disabled
    /// </summary>
    public int baseHealth;
    public int currentHealth;
    [SerializeField] protected int maxHealth;

    public int baseMana;
    public int currentMana;
    [SerializeField] protected int maxMana;

    //public int weaponPhysicalDamage;
    //public int weaponMagicalDamage;

    public int physicalDamage;
    public int magicalDamage;
    public int physicalDefense;
    public int magicalDefense;

    //primary stats
    public int strength;
    public int vitality;
    public int agility;
    public int dexterity;
    public int intelligence;
    public int wisdom;

    //Resistances
    public float currentMeleeResistance;
    public float maxMeleeResistance = 100;

    public float currentMagicResistance;
    public float maxMagicResistance = 200;

    public float currentFireResistance;
    public float maxFireResistance = 200;

    public float currentWaterResistance;
    public float maxWaterResistance = 200;

    public float currentEarthResistance;
    public float maxEarthResistance = 200;

    public float currentWindResistance;
    public float maxWindResistance = 200;

    public float meleeResPercentage;
    public float magResPercentage;
    public float fireResPercentage;
    public float waterResPercentage;
    public float earthResPercentage;
    public float windResPercentage;
    //These two stats will be made and implemented when the system for the other stats has been created and working correctly
    /*public int accuracy;
    public int dodge;*/

    public int castability;
    public int criticalDamage;
    public int criticalHitChance;

    //Special Stats!!!!
    public int Charisma;
    public int Weight;
    public int Chaos;

    int damageDice;

    public bool phyiscalAttack = false;
    public bool magicalAttack = false;
    public bool chaosAttack = false;

    //public bool enableStrength = true;
    //public bool enableVitality = true;
    //public bool enableAgility = true;
    //public bool enableDexterity = true;
    //public bool enableIntelligence = true;
    //public bool enableWisdom = true;

    // Start is called before the first frame update
    void Start()
    {
        /*maxHealth = baseHealth + (vitality * 10);
        currentHealth = maxHealth;
        maxMana = baseMana + (intelligence * 10);
        currentMana = maxMana;*/

        physicalDamage = (strength * 10);
        magicalDamage = (intelligence * 10);
        physicalDefense = (vitality * 10);
        magicalDefense = (wisdom * 10);
    }

    // Update is called once per frame
    void Update()
    {


        //If a spell is cast, a spell castability function will get a random value for castability,
        //and then compare that to the users current spell casting ability to determine if the spell can be used.
        if (chaosAttack)
        {
            ChaosAction();
        }
        Resistances();
    }

    void ChaosAction()
    {
        int castDice = (int)Random.Range(0, 100);
        damageDice = (int)Random.Range(0, (float)(magicalDamage * 2));
        if (castability < 50)
        {
            int tempVal1 = ((magicalDamage + damageDice) - magicalDefense);
            int tempVal2 = tempVal1 * ((int)magResPercentage / 100);
            //currentHealth -= tempVal1 - tempVal2;
        }
        else
        {
            magicalDamage += damageDice;
        }
    }

    void Resistances()
    {
        //Gets the current magic resistance by adding the four magic elements together and then divinding them 
        currentMagicResistance = (currentFireResistance + currentWaterResistance + currentEarthResistance + currentWindResistance) / 4;

        meleeResPercentage = (currentMeleeResistance / maxMeleeResistance) * 100;
        //Gets the percentage that is displayed for the player
        magResPercentage = (currentMagicResistance / maxMagicResistance) * 100;
        fireResPercentage = (currentFireResistance / maxFireResistance) * 100;
        waterResPercentage = (currentWaterResistance / maxWaterResistance) * 100;
        earthResPercentage = (currentEarthResistance / maxEarthResistance) * 100;
        windResPercentage = (currentWindResistance / maxWindResistance) * 100;

        //If the current magic resistance exceeds the max magic resistances during the game loop,
        //then it will just make the current magic resistance equal to the max magic resistance. 
        if (currentMeleeResistance > maxMeleeResistance)
        {
            currentMeleeResistance = maxMeleeResistance;
        }
        if (currentMagicResistance > maxMagicResistance)
        {
            currentMagicResistance = maxMagicResistance;
        }
        if (currentFireResistance > maxFireResistance)
        {
            currentFireResistance = maxFireResistance;
        }
        if (currentWaterResistance > maxWaterResistance)
        {
            currentWaterResistance = maxWaterResistance;
        }
        if (currentEarthResistance > maxEarthResistance)
        {
            currentEarthResistance = maxEarthResistance;
        }
        if (currentWindResistance > maxWindResistance)
        {
            currentWindResistance = maxWindResistance;
        }

        //Debug.Log("Magic Resistance: " + (int)magResPercentage + "%");
    }
}
