﻿[System.Serializable]
public class ActorInfo
{
    public Equip[] Equips;
    public Skill[] Skills;
    public Status Status;
    public Costume[] Costume;
    public Costume[] DefaultCostume;

    public bool IsMy;
    public bool IsRegist;

    public string Name;
    public string Path;
    public string Tag;
}