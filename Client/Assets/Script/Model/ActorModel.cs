using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActorModel
{
    public ActorInfo CreateTableActor(CharacterTableData characterTableData)
    {
        ActorInfo actorInfo = new ActorInfo();
        actorInfo.Costume = new Costume[(int)CostumeType.Max];
        actorInfo.DefaultCostume = new Costume[(int)CostumeType.Max];
        for (int i = 0; i < characterTableData.DefaultCostumeIndex.Length; ++i)
        {
            int index = characterTableData.DefaultCostumeIndex[i];
            CostumeTableData costumeTableData = CostumeTable.Instance.FindToIndex(index);
            Costume costume = new Costume(i, 
                                          costumeTableData.Index,
                                          costumeTableData.Name,
                                          costumeTableData.Path,
                                          costumeTableData.Path[0],
                                          costumeTableData.CostumeType);
            costume.SetCount(1);
            costume.SetWear(true);
            actorInfo.DefaultCostume[(int)costume.CostumeType] = costume;
        }

        actorInfo.Equips = new Equip[(int)EquipType.Max];
        actorInfo.Skills = new Skill[1];
        actorInfo.Status = characterTableData.status;
        actorInfo.IsMy = false;
        actorInfo.Name = "";
        actorInfo.Path = characterTableData.path;
        actorInfo.Tag = "";
        return actorInfo;
    }
}