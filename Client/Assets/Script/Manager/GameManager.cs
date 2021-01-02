using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public const float SPEED = 100.0f;
    public Player MyPlayer { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);

        FlowManager.Instance.ChangeFlow(new LogoFlow());
    }

    public void SetPlayer()
    {
        // 나중에는 선택한 캐릭터 정보를 넘겨받아 Player를 완성시켜야 한다.
        // 1. 스텟
        // 2. 스킬
        MyPlayer = new Player();
        MyPlayer.createTime = 0;
        MyPlayer.dbIndex = 0;

        ActorInfo actorInfo = new ActorInfo();
        CharacterTableData characterTableData = CharacterTable.Instance.FindToIndex(1);

        actorInfo.Costume = new Costume[(int)CostumeType.Max];
        actorInfo.DefaultCostume = new Costume[(int)CostumeType.Max];
        for (int i = 0; i < characterTableData.DefaultCostumeIndex.Length; ++i)
        {
            int index = characterTableData.DefaultCostumeIndex[i];
            CostumeTableData costumeTableData = CostumeTable.Instance.FindToIndex(index);
            Costume costume = new Costume();
            costume.index = costumeTableData.Index;
            costume.Name = costumeTableData.Name;
            costume.Path = costumeTableData.Path;
            costume.SerialIndex = 0;
            costume.CostumeType = costumeTableData.CostumeType;
            actorInfo.DefaultCostume[(int)costume.CostumeType] = costume;
        }
        
        actorInfo.Equips = new Equip[(int)EquipType.Max];
        actorInfo.Skills = new Skill[1];
        actorInfo.Status = characterTableData.status;
        actorInfo.IsMy = true;
        actorInfo.Name = "다람쥐";
        actorInfo.Path = characterTableData.path;
        actorInfo.Tag = "Player";
        MyPlayer.ActorInfo = actorInfo;


        // 캐릭터 정보와 함께 아래 정보들을 받아와야 한다.
        // 1. 인벤토리
        // 2. 현재 위치 (필드 Index, 좌표 Vector)
        // 3. 미션
        // 4, 길드
        // 5. 친구
        // 6. 도감
        EquipManager.Instance.TestSetEquip();
    }
}
