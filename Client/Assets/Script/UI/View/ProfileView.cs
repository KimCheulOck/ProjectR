using UnityEngine;

public class ProfileView : BaseView
{
    [SerializeField]
    private ProfileCharacterUnit[] profileCharacterUnits = null;

    private ActorInfo[] actorInfos = null;

    private System.Action onEventExit = null;

    public void AddEvent(System.Action onEventExit)
    {
        this.onEventExit = onEventExit;
    }

    public void SetCharacter(ActorInfo[] actorInfos)
    {
        this.actorInfos = actorInfos;
    }

    public void Show()
    {
        SetCharacterSlot();
    }

    public void OnClickExit()
    {
        onEventExit();
    }

    private void SetCharacterSlot()
    {
        for(int i =0; i< profileCharacterUnits.Length; ++i)
        {
            profileCharacterUnits[i].Initialize(actorInfos[i], i);
            profileCharacterUnits[i].Show();
        }
    }
}
