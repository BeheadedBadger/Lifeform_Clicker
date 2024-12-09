using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour
{
    [SerializeField] Store store;
    [SerializeField] bool active;
    [SerializeField] GameObject LifeFormHolder;
    [SerializeField] public Image indicator;
    [SerializeField] public Image indicatorIcon;
    GameObject currentLifeform;
    GameManager gameManager;

    public void OnClick() {
        if (!active) {
            store.ShowStore();
        };
    }

    public void addLifeform(int id) {
        GameObject lifeform = Instantiate(gameManager.lifeforms[id], LifeFormHolder.transform);
        currentLifeform = lifeform;
        currentLifeform.GetComponent<Lifeform>().hex = this;
        SetActive();
    }

    public void removeLifeform() {
        currentLifeform = null;
        SetInactive();
    }

    public void SetActive() { 
        active = true; 
        //Graphics active true
        //Graphics inactive false
    }
    public void SetInactive() { 
        active = false; 
        //viceversa
    }
}
