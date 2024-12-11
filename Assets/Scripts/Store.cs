using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] public GameManager gameManager;
    [SerializeField] GameObject StoreItemContainer;
    [SerializeField] GameObject StorePanelPrefab;
    public Hex selectedHex;

    private void Start()
    {
        foreach (GameObject lifeform in gameManager.lifeforms)
        {
            GameObject storePref = Instantiate(StorePanelPrefab, StoreItemContainer.transform);
            StoreItem storePrefScript = storePref.GetComponent<StoreItem>();
            storePrefScript.lifeform = lifeform;
            storePrefScript.store = this;
        }

        HideStore();
    }

    public void HideStore() { 
        Panel.SetActive(false);
    }
    public void ShowStore(Hex hex) {
        Panel.SetActive(true);
        selectedHex = hex;
    }
}
