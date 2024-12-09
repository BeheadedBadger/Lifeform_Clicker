using UnityEngine;

public class LifeformNeeds
{ 
    public string name;
    public float need;
    public bool requires;
    public bool produces;
    public Sprite sprite;

    public enum Need { Water, Plants, Meat, Bugs, Biowaste, Fungi };

    public LifeformNeeds(string name, float need, bool requires, bool produces, Sprite sprite)
    {
        this.name = name;
        this.need = need;
        this.requires = requires;
        this.produces = produces;
        this.sprite = sprite;
    }
}
