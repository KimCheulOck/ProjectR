public class CreateCharacterModel
{
    public string Pid { get; private set; }
    public int SlotIndex { get; private set; }

    public CreateCharacterModel(params object[] param)
    {
        Pid = GameManager.Instance.MyPlayer.pid;
        SlotIndex = (int)param[0];
    }
}
