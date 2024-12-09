using Mono.Cecil;
using System;
using TMPro;
using UnityEngine;
using static LifeformNeeds;

public class BottomPanelButton : MonoBehaviour
{
    [SerializeField] LifeformNeeds.Need resourceType;
    [SerializeField] GameManager gameManager;

    public int cost = 999;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text amountText;

    public bool isUnlocked = false;
    [SerializeField] GameObject Unlocked;
    [SerializeField] GameObject Locked;

    void Start() {
        if (resourceType == LifeformNeeds.Need.Water && gameManager.waterUnlocked == true) { isUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Plants && gameManager.plantsUnlocked == true) { isUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Meat && gameManager.meatUnlocked == true) { isUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Bugs && gameManager.bugsUnlocked == true) { isUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Biowaste && gameManager.biowasteUnlocked == true) { isUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Fungi && gameManager.fungiUnlocked == true) { isUnlocked = true; }

        if (!isUnlocked) {
            costText.text = cost.ToString();
        }

        if (isUnlocked) {
            setUnlockedGraphics();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnlocked) { 
            if (resourceType == LifeformNeeds.Need.Water) { amountText.text = $"x{gameManager.water.ToString()}"; }
            if (resourceType == LifeformNeeds.Need.Plants){ amountText.text = $"x{gameManager.plants.ToString()}"; }
            if (resourceType == LifeformNeeds.Need.Meat) { amountText.text = $"x{gameManager.meat.ToString()}"; }
            if (resourceType == LifeformNeeds.Need.Bugs) { amountText.text = $"x{gameManager.bugs.ToString()}"; }
            if (resourceType == LifeformNeeds.Need.Biowaste) { amountText.text = $"x{gameManager.biowaste.ToString()}"; }
            if (resourceType == LifeformNeeds.Need.Fungi) { amountText.text = $"x{gameManager.fungi.ToString()}"; }
        }
    }

    public void OnClick() { 
        if (!isUnlocked && gameManager.lifeCoins > cost) {
            gameManager.RemoveLifeCoins(cost);
            Unlock();
            setUnlockedGraphics();
        }
    }

    private void Unlock()
    {
        if (resourceType == LifeformNeeds.Need.Water) { gameManager.waterUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Plants) { gameManager.plantsUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Meat) { gameManager.meatUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Bugs) { gameManager.bugsUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Biowaste) { gameManager.biowasteUnlocked = true; }
        if (resourceType == LifeformNeeds.Need.Fungi) { gameManager.fungiUnlocked = true; }
        isUnlocked = true;
    }

    private void setUnlockedGraphics()
    {
        Locked.SetActive(false);
        Unlocked.SetActive(true); 
    }
}
