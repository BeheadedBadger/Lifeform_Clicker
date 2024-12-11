using System;
using UnityEngine;

public class LifeformNeeds
{ 
    public string name;
    public float need;
    public bool requires;
    public bool produces;
    public Sprite sprite;

    [Flags] public enum Need { Water=0, Plants=1, Meat=2, Bugs=3, Biowaste=4, Fungi=5 };

    public LifeformNeeds(string name, float need, bool requires, bool produces, Sprite sprite)
    {
        this.name = name;
        this.need = need;
        this.requires = requires;
        this.produces = produces;
        this.sprite = sprite;
    }
}
