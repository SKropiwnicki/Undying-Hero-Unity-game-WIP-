using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{
    public string name = "none";
    public int cooldown = 0;

    public virtual void action(Actor source) { } //Buffy i działające na ciebie skile
    public virtual void action(Actor source, Actor target) { } //
   // public virtual void Action(Actor source, List<Actor> targets) { }
}