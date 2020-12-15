[System.Serializable]
public class Status
{
    public int level = 1;
    public int exp = 0;
    public int hp = 0;
    public int mp = 0;
    public int attack = 0;
    public int defense = 0;
    public int hit = 0;
    public int avoid = 0;
    public int critical = 0;
    public float atkSpeed = 0;
    public float moveSpeed = 0;
    public float spellSpeed = 0;
    public int elemental = 0;

    // 체력           : HP 상승
    // 마력           : MP 상승
    // 공격력         : 물리 공격력
    // 방어력         : 물리 방어력
    // 명중률         : 물리 공격 명중률
    // 회피율         : 물리 공격 회피율
    // 치명타 확률    : 물리 공격 치명타 확률
    // 공격 속도      : 물리 공격 속도
    // 이동 속도      : 이동 속도
    // 시전 속도      : 마법 공격 시전 속도
    // 원소력         : 땅, 불, 물, 바람, 전기, 빛, 어둠 저항력
}
