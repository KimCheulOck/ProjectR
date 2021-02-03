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
    public int attackSpeed = 0;
    public int moveSpeed = 0;
    public int spellSpeed = 0;
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

    public int GetStatus(StatusType statusType)
    {
        switch (statusType)
        {
            case StatusType.Hp:
                return hp;
            case StatusType.Mp:
                return mp;
            case StatusType.Attack:
                return attack;
            case StatusType.Defense:
                return defense;
            case StatusType.Hit:
                return hit;
            case StatusType.Avoid:
                return avoid;
            case StatusType.Critical:
                return critical;
            case StatusType.AttackSpeed:
                return attackSpeed;
            case StatusType.MoveSpeed:
                return moveSpeed;
            case StatusType.SpellSpeed:
                return spellSpeed;
            case StatusType.Elemental:
                return elemental;
            default:
                return 0;
        }
    }

    public void SetStatus(StatusType statusType, int value)
    {
        switch (statusType)
        {
            case StatusType.Hp:
                {
                    hp = value;
                }
                break;
            case StatusType.Mp:
                {
                    mp = value;
                }
                break;
            case StatusType.Attack:
                {
                    attack = value;
                }
                break;
            case StatusType.Defense:
                {
                    defense = value;
                }
                break;
            case StatusType.Hit:
                {
                    hit = value;
                }
                break;
            case StatusType.Avoid:
                {
                    avoid = value;
                }
                break;
            case StatusType.Critical:
                {
                    critical = value;
                }
                break;
            case StatusType.AttackSpeed:
                {
                    attackSpeed = value;
                }
                break;
            case StatusType.MoveSpeed:
                {
                    moveSpeed = value;
                }
                break;
            case StatusType.SpellSpeed:
                {
                    spellSpeed = value;
                }
                break;
            case StatusType.Elemental:
                {
                    elemental = value;
                }
                break;
        }
    }
}
