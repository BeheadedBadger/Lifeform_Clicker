using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    public void HideStore() { 
        Panel.SetActive(false);
    }
    public void ShowStore() {
        Panel.SetActive(true);
    }
}
