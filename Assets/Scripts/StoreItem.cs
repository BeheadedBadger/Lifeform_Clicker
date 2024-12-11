using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] public GameObject lifeform;
    [SerializeField] TMP_Text price;
    [SerializeField] TMP_Text coinsPerSecond;
    [SerializeField] Image resourceIcon;
    [SerializeField] Image icon;
    private Lifeform lifeformscript;
    [SerializeField] GameObject[] needs;
    [SerializeField] GameObject[] productionSprites;
    public Store store;

    private void Start()
    {
        //loading icon to be set to visible here
        StartCoroutine("Content");
    }

    IEnumerator Content()
    {
        yield return new WaitForSeconds(0.01f);
        lifeformscript = lifeform.GetComponent<Lifeform>();
        lifeformscript.WakeUp();

        yield return new WaitForSeconds(0.01f);
        //loading icon to be set to invisible here
        price.text = lifeformscript.cost.ToString();

        Color Inactive = new Color(0.8f, 0.8f, 0.8f, 0.2f);
        Color Active = new Color(0.8f, 0.8f, 0.8f, 1);
        var enumList = (LifeformNeeds.Need[])Enum.GetValues(typeof(LifeformNeeds.Need));

        for (int i = 0; i < enumList.Length; i++)
        {
            Image child = needs[i].GetComponent<Image>();
            if (lifeformscript.needs != null)
            {
                if (lifeformscript.needs[i].name == enumList[i].ToString() && lifeformscript.needs[i].requires == true)
                {
                    child.color = Active;
                }
            }
            else
            {
                child.color = Inactive;
            }

        }

        for (int i = 0; i < enumList.Length; i++)
        {
            if (lifeformscript.needs != null)
            {
                if (lifeformscript.needs[i].name == enumList[i].ToString() && lifeformscript.needs[i].produces == true)
                {
                    productionSprites[i].SetActive(true);
                    resourceIcon.sprite = lifeformscript.needs[i].sprite;
                    //resourcePerSecond.text = $"+   /s";
                }
            }

            coinsPerSecond.text = $"+{lifeformscript.currencyProductionRate}/s";
            icon.sprite = lifeformscript.image.sprite;
        }
    }

    public void OnPurchase() {
        if (store.gameManager.lifeCoins > lifeformscript.cost) {
            store.gameManager.RemoveLifeCoins(lifeformscript.cost);
            store.selectedHex.addLifeform(lifeform);
            store.HideStore();
        }
    }
}
