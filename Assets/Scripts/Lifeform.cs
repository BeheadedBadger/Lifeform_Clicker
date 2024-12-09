using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.XR;
using static LifeformNeeds;

public class Lifeform : MonoBehaviour
{
    float needsRate = 0.1f;
    float productionRate = 0.01f;
    float timeToProduce = 0;
    int resilianceRate = 20;

    //Add hex, assign lifeforms's hex on instantiate
    //Move to hex
    public Hex hex;
    //[SerializeField] Image indicator;
    //[SerializeField] public Image indicatorIcon;

    [Header("Requirements")]
    [SerializeField] bool requiresWater;
    [SerializeField] bool requiresPlants;
    [SerializeField] bool requiresMeat;
    [SerializeField] bool requiresBugs;
    [SerializeField] bool requiresBiowaste;
    [SerializeField] bool requiresFungi;

    [Header("Production")]
    [SerializeField] bool producesWater;
    [SerializeField] bool producesPlants;
    [SerializeField] bool producesMeat;
    [SerializeField] bool producesBugs;
    [SerializeField] bool producesBiowaste;
    [SerializeField] bool producesFungi;

    public LifeformNeeds[] needs;
    [SerializeField] Sprite[] Sprites;

    [Header("Currency")]
    [SerializeField] int currencyProductionRate = 1; 
    private int nextCurrencyProductionTime = 1;
    [SerializeField] int cost;

    GameManager gameManager;

    void Start() {
        needs = new LifeformNeeds[] {
            new LifeformNeeds(LifeformNeeds.Need.Water.ToString(), 0, requiresWater, producesWater, Sprites[0]),
            new LifeformNeeds(LifeformNeeds.Need.Plants.ToString(), 0, requiresPlants, producesPlants, Sprites[1]),
            new LifeformNeeds(LifeformNeeds.Need.Meat.ToString(), 0, requiresMeat, producesMeat, Sprites[2]),
            new LifeformNeeds(LifeformNeeds.Need.Bugs.ToString(), 0, requiresBugs, producesBugs, Sprites[3]),
            new LifeformNeeds(LifeformNeeds.Need.Biowaste.ToString(), 0, requiresBiowaste, producesBiowaste, Sprites[4]),
            new LifeformNeeds(LifeformNeeds.Need.Fungi.ToString(), 0, requiresFungi, producesFungi, Sprites[5])
        };

        GameObject obj = GameObject.Find("GameManager");
        gameManager = obj.GetComponent<GameManager>();
    }

    void Update() {
        CalculateNeeds();
        CheckRequirements();
        CheckProduction();
    }

    private void CheckProduction()
    {
        if (Time.time >= nextCurrencyProductionTime)
        {
            Debug.Log(Time.time + ">=" + nextCurrencyProductionTime);
            nextCurrencyProductionTime = Mathf.FloorToInt(Time.time) + 1;
        }

        timeToProduce += (productionRate) * Time.deltaTime;
        if (timeToProduce > 1) {
            gameManager.AddLifeCoins(currencyProductionRate);
            foreach (LifeformNeeds need in needs) {
                switch (need.name)
                {
                    case "Water":
                        if (gameManager.waterUnlocked && need.produces)
                        {
                            gameManager.addWater(1);
                        }
                        break;
                    case "Plants":
                        if (gameManager.plantsUnlocked && need.produces)
                        {
                            gameManager.addPlants(1);
                        }
                        break;
                    case "Meat":
                        if (gameManager.meatUnlocked && need.produces)
                        {
                            gameManager.addMeat(1);
                        }
                        break;
                    case "Bugs":
                        if (gameManager.bugsUnlocked && need.produces)
                        {
                            gameManager.addBugs(1);
                        }
                        break;
                    case "Biowaste":
                        if (gameManager.biowasteUnlocked && need.produces)
                        {
                            gameManager.addBiowaste(1);
                        }
                        break;
                    case "Fungi":
                        if (gameManager.fungiUnlocked && need.produces)
                        {
                            gameManager.addFungi(1);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void AddCoin() {
        gameManager.AddLifeCoins(currencyProductionRate); 
    }

    private void CheckRequirements() {
        LifeformNeeds highestNeed = needs[0];
        Color red = new Color(0.84f, 0.25f, 0.34f, 1f);
        Color orange = new Color(1f, 0.83f, 0.49f, 1f);
        Color black = new Color(0.22f, 0.22f, 0.22f, 1);
        Color invisible = new Color(0.22f, 0.22f, 0.22f, 0);

        Debug.Log(highestNeed.need);

        foreach (LifeformNeeds need in needs) {
            if (need.need > highestNeed.need) {
                highestNeed = need;
            }
        }

        if (highestNeed.need < (resilianceRate / 4) && hex.indicator.color != invisible) {
            hex.indicator.color = Color.Lerp(hex.indicator.color, invisible, Mathf.PingPong(Time.time, 1));
            hex.indicatorIcon.color = Color.Lerp(hex.indicatorIcon.color, invisible, Mathf.PingPong(Time.time, 1));
        }
        if (highestNeed.need > (resilianceRate/4) && highestNeed.need < (resilianceRate/2)) {
            hex.indicator.color = Color.Lerp(hex.indicator.color, orange, Mathf.PingPong(Time.time, 1));
            hex.indicatorIcon.sprite = highestNeed.sprite;
            hex.indicatorIcon.color = Color.Lerp(hex.indicatorIcon.color, black, Mathf.PingPong(Time.time, 1));
        }
        if (highestNeed.need > (resilianceRate/2) && highestNeed.need < (resilianceRate/1.5)) {
            hex.indicator.color = Color.Lerp(hex.indicator.color, red, Mathf.PingPong(Time.time, 1));
        }
        if (highestNeed.need > (resilianceRate/1.5) && highestNeed.need < resilianceRate) {
            Color lerpedColor;
            bool redlerpCompleted = true;
            bool blacklerpCompleted = false;

            //if indicator is red, switch to black
            if (redlerpCompleted) {
                lerpedColor = Color.Lerp(red, Color.white, Mathf.PingPong(Time.time, 1));
                hex.indicator.color = lerpedColor;
                if (hex.indicator.color == black) {
                    blacklerpCompleted = true;
                    redlerpCompleted = false;
                }
                return;
            }

            else if (blacklerpCompleted) {
                lerpedColor = Color.Lerp(Color.white, red, Mathf.PingPong(Time.time, 1));
                hex.indicator.color = lerpedColor;
                if (hex.indicator.color == red)
                {
                    blacklerpCompleted = false;
                    redlerpCompleted = true;
                }
            }
        }
        if (highestNeed.need > resilianceRate) {
            Destroy(this.gameObject);
        }
    }

    private void CalculateNeeds() {
        foreach (LifeformNeeds need in needs) {
            if (need.requires == true) {
                if (need.name == LifeformNeeds.Need.Water.ToString()) {
                    need.need += needsRate * Time.deltaTime;
                }
                if (need.name == LifeformNeeds.Need.Plants.ToString() || need.name == LifeformNeeds.Need.Meat.ToString() || need.name == LifeformNeeds.Need.Fungi.ToString()) {
                    need.need += (needsRate / 2) * Time.deltaTime;
                }
                if (need.name == LifeformNeeds.Need.Biowaste.ToString()) {
                    need.need += (needsRate / 4) * Time.deltaTime;
                }
            }
        }
    }

    public void OnClick() {
        bool needFound = false;

        foreach (LifeformNeeds need in needs) {
            if (need.need > (resilianceRate / 4)) {
                needFound = true;
                ResetNeed(need.name);
            }
        }

        if (!needFound) {
            gameManager.AddLifeCoin();
        }
    }

    public void ResetNeed(string Need)
    {
        switch (Need)
        {
            case "Water":
                if (gameManager.water > 0)
                {
                    gameManager.removeWater(1);
                    foreach (LifeformNeeds need in needs) {
                        if (need.name == LifeformNeeds.Need.Water.ToString()) {
                            need.need = 0;
                        } 
                    }
                }
                break;
            case "Plants":
                if (gameManager.plants > 0)
                {
                    gameManager.removePlants(1);
                    foreach (LifeformNeeds need in needs)
                    {
                        if (need.name == LifeformNeeds.Need.Plants.ToString())
                        {
                            need.need = 0;
                        }
                    }
                }
                break;
            case "Meat":
                if (gameManager.meat > 0)
                {
                    gameManager.removeMeat(1);
                    foreach (LifeformNeeds need in needs)
                    {
                        if (need.name == LifeformNeeds.Need.Meat.ToString())
                        {
                            need.need = 0;
                        }
                    }
                }
                break;
            case "Bugs":
                if (gameManager.bugs > 0)
                {
                    gameManager.removeBugs(1);
                    foreach (LifeformNeeds need in needs)
                    {
                        if (need.name == LifeformNeeds.Need.Bugs.ToString())
                        {
                            need.need = 0;
                        }
                    }
                }
                break;
            case "Biowaste":
                if (gameManager.biowaste > 0)
                {
                    gameManager.removeBiowaste(1);
                    foreach (LifeformNeeds need in needs)
                    {
                        if (need.name == LifeformNeeds.Need.Biowaste.ToString())
                        {
                            need.need = 0;
                        }
                    }
                }
                break;
            case "Fungi":
                if (gameManager.fungi > 0)
                {
                    gameManager.removeFungi(1);
                    foreach (LifeformNeeds need in needs)
                    {
                        if (need.name == LifeformNeeds.Need.Fungi.ToString())
                        {
                            need.need = 0;
                        }
                    }
                }
                break;
            default:
                //Implement not enough resources result
                Debug.Log("not enough resources");
                break;
        }
    }
}
