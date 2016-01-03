using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill : MonoBehaviour
{
    public virtual void Action(Actor source) { } //Buffy i działające na ciebie skile
    public virtual void Action(Actor source, Actor target) { } //
   // public virtual void Action(Actor source, List<Actor> targets) { }
}