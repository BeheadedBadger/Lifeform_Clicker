using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Coins
    public int lifeCoins = 0;
    [SerializeField] TMP_Text coinText;

    [SerializeField] public GameObject[] lifeforms;

    //Other resources
    public int water = 0;
    public bool waterUnlocked;
    public int plants = 0;
    public bool plantsUnlocked;
    public int meat = 0;
    public bool meatUnlocked;
    public int bugs = 0;
    public bool bugsUnlocked;
    public int biowaste = 0;
    public bool biowasteUnlocked;
    public int fungi = 0;
    public bool fungiUnlocked;

    //Coins
    public void AddLifeCoin() { 
        lifeCoins++;
    }

    public void AddLifeCoins(int coinsToAdd) {
        lifeCoins += coinsToAdd;
    }

    public void RemoveLifeCoins(int coinsToRemove) {
        lifeCoins -= coinsToRemove;
    }

    void Update() {
        coinText.text = lifeCoins.ToString();
    }

    //Other resources
    public void addWater(int amount) { water += amount; }
    public void removeWater(int amount) { water -= amount; }
    public void addPlants(int amount) { plants += amount; }
    public void removePlants(int amount) {plants -= amount; }
    public void addMeat(int amount) { meat += amount; }
    public void removeMeat(int amount) {meat -= amount; }
    public void addBugs(int amount) { bugs += amount; }
    public void removeBugs(int amount) { bugs -= amount; }
    public void addBiowaste(int amount) {  biowaste += amount; }
    public void removeBiowaste(int amount) { biowaste -= amount; }
    public void addFungi(int amount) { fungi += amount; }
    public void removeFungi(int amount) {fungi -= amount; }
}
