using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Hex : MonoBehaviour
{
    [SerializeField] Store store;
    [SerializeField] bool active;
    [SerializeField] GameObject LifeFormHolder;
    [SerializeField] public Image indicator;
    [SerializeField] public Image indicatorIcon;
    GameObject currentLifeform;
    GameManager gameManager;
    [SerializeField] GameObject activeGraphics;
    Lifeform lifeformscript;


    public void OnClick() {
        if (!active) {
            store.gameObject.SetActive(true);
            store.ShowStore(this);
        };
        if (active)
        {
            lifeformscript.OnClick();
        }
    }

    public void addLifeform(GameObject lifeform) {
        if (!active) {
            currentLifeform = lifeform;
            currentLifeform.GetComponent<Lifeform>().hex = this;

            GameObject inst = Instantiate(lifeform, LifeFormHolder.transform);
            Lifeform lifeformscript = lifeform.GetComponent<Lifeform>();
            lifeformscript.hex = this;

            SetActive();
        }
    }

    public void removeLifeform() {
        currentLifeform = null;
        SetInactive();
    }

    public void SetActive() { 
        active = true;
        activeGraphics.SetActive(true);
    }
    public void SetInactive() { 
        active = false;
        activeGraphics.SetActive(false);
    }
}
