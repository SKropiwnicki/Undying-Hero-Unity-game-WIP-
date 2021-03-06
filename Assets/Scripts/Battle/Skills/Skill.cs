﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{
    public string name = "none";
    public int APCost;
    public string type = "none";
    protected string displayName = "none";

    public virtual void action(Actor source) { } //Buffy i działające na ciebie skile
    public virtual void action(Actor source, Actor target) { } //
                                                               // public virtual void Action(Actor source, List<Actor> targets) { }

    public void useSkill(Actor source)
    {
        if (source.currentAP < APCost)
        {
            Debug.LogError("NIEPOPRAWNA ILOSC AP. " + source.name + " ma " + source.currentAP + " AP. Wymagane: " + APCost);
        }
        useText(source);
        action(source);
        ButtonManager.instance.DestroyOldButtons();
        source.calculateStats();
        TurnManagement.instance.nextTurnCor();
    }

    public void useSkill(Actor source, Actor target)
    {
        if (source.currentAP < APCost)
        {
            Debug.LogError("NIEPOPRAWNA ILOSC AP. " + source.name + " ma " + source.currentAP + " AP. Wymagane: " + APCost);
        }
        useText(source);
        action(source, target);
        source.calculateStats();
        target.calculateStats();
        TurnManagement.instance.nextTurnCor();
    }

    private void useText(Actor source)
    {
        TextSpawner.instance.spawn(source.transform, displayName, Color.white, 40);
    }

    protected bool isCriticalHit (int critChance)
    {
        if (critChance >= Random.Range(0, 100)) return true;
        else return false;
    }

    public bool hasEnoughAP(int i)
    {
        if (i >= APCost) return true;
        else return false;
    }
}