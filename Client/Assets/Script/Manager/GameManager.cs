using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google.Protobuf.Protocol;

public class GameManager : MonoSingleton<GameManager>, IObserver
{
    public const float SPEED = 100.0f;
    public Player MyPlayer { get; private set; }

    void IObserver.RefrashObserver(ObserverMessage id, object[] message)
    {
        switch (id)
        {
            case ObserverMessage.RestartApp:
                {
                    break;
                }
        }
    }


    protected override void Awake()
    {
        base.Awake();

        //string tt2 = "{\"order\":\"profile\",\"result\":0,\"msg\":\"성공\",\"pid\":\"p0g900D@A\",\"actorInfos\":\"[{\\\"index\\\":0,\\\"name\\\":\\\"\\\",\\\"isRegist\\\":false,\\\"status\\\":{},\\\"inventory\\\":{}},{\\\"index\\\":1,\\\"name\\\":\\\"\\\",\\\"isRegist\\\":false,\\\"status\\\":{},\\\"inventory\\\":{}},{\\\"index\\\":2,\\\"name\\\":\\\"\\\",\\\"isRegist\\\":false,\\\"status\\\":{},\\\"inventory\\\":{}}]\"}";
        //string tt2 = "{\"order\":\"profile\",\"result\":0,\"msg\":\"성공\",\"pid\":\"p0g900D@A\",\"actorInfos\":\"[{\"index\\\":0,\"name\":\"\",\"isRegist\":false,\"status\":{},\"inventory\":{}},{\"index\\\":1,\"name\":\"\",\"isRegist\":false,\"status\":{},\"inventory\":{}},{\"index\\\":2,\"name\":\"\",\"isRegist\":false,\"status\":{},\"inventory\":{}}]\"}";

        ////var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<WebResponseProfile[]>(tt);
        ////var test2 = Newtonsoft.Json.JsonConvert.DeserializeObject<WebResponseProfile>(tt2);
        //var test2 = JsonUtility.FromJson<WebResponseProfile>(tt2);
        DontDestroyOnLoad(this);
        ObserverHandler.Instance.AddObserver(ObserverMessage.RestartApp, this);

        FlowManager.Instance.ChangeFlow(new LogoFlow());
    }


    public void LoginMyPlayer(string pid)
    {
        MyPlayer = new Player();
        MyPlayer.pid = pid;
    }

    public void SetMyPlayerCharacters(ActorInfo[] actorInfos)
    {
        MyPlayer.AllActorInfos = actorInfos;
    }

    public void CreateMyPlayerCharacter(ActorInfo actorInfo, int slotIndex)
    {
        MyPlayer.AllActorInfos[slotIndex] = actorInfo;
    }

    public void SetPlayer()
    {
        // 나중에는 선택한 캐릭터 정보를 넘겨받아 Player를 완성시켜야 한다.
        // 1. 스텟
        // 2. 스킬
        MyPlayer = new Player();
        MyPlayer.createTime = 0;
        MyPlayer.pid = "";

        ActorInfo actorInfo = new ActorInfo();
        CharacterTableData characterTableData = CharacterTable.Instance.FindToIndex(1);

        actorInfo.Costume = new Costume[(int)CostumeType.Max];
        actorInfo.DefaultCostume = new Costume[(int)CostumeType.Max];
        for (int i = 0; i < characterTableData.DefaultCostumeIndex.Length; ++i)
        {
            int index = characterTableData.DefaultCostumeIndex[i];
            CostumeTableData costumeTableData = CostumeTable.Instance.FindToIndex(index);
            Costume costume = new Costume(i, costumeTableData.Index, costumeTableData.Name, costumeTableData.Path,
                                          costumeTableData.Path[0], costumeTableData.CostumeType);
            costume.SetCount(1);
            costume.SetWear(true);
            actorInfo.DefaultCostume[(int)costume.CostumeType] = costume;
        }
        
        actorInfo.Equips = new Equip[(int)EquipType.Max];
        actorInfo.Skills = new Skill[1];
        actorInfo.Status = characterTableData.status;
        actorInfo.IsMy = true;
        actorInfo.Name = "다람쥐";
        actorInfo.Path = characterTableData.path;
        actorInfo.Tag = "Player";
        MyPlayer.SelectActorInfo = actorInfo;


        // 캐릭터 정보와 함께 아래 정보들을 받아와야 한다.
        // 1. 인벤토리
        // 2. 현재 위치 (필드 Index, 좌표 Vector)
        // 3. 미션
        // 4, 길드
        // 5. 친구
        // 6. 도감
        EquipManager.Instance.TestSetEquip();
    }

    public static T CreatePrefabs<T>(GameObject obj, Transform parentTarget)
    {
        GameObject load = Instantiate(obj, parentTarget);
        load.layer = parentTarget.gameObject.layer;
        return load.GetComponent<T>();
    }

    public static T CreatePrefabs<T>(T obj, Transform parentTarget) where T : MonoBehaviour
    {
        GameObject load = Instantiate(obj.gameObject, parentTarget);
        load.layer = parentTarget.gameObject.layer;
        return load.GetComponent<T>();
    }
}
