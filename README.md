# ProjectR
ProjectR

1. FLOW
- 게임의 기본이 되는 구조
- FlowManager에서 각 Flow들을 처리
- ChangeFlow : Flow를 변경 (TownFlow -> DungeonFlow)
- BackToFlow : 이전 Flow로 되돌아감
- LoadingProcess : 플로우 전환 시 로딩 처리를 담당

2 UI
- MVP를 지향하여 구현
- 어떤 UI든 Model, View, Presenter 세개로 구성
(Model이 필요 없는 UI의 경우 제외)
- BasePresenter를 상속받는 Presenter들을 UIController에서 호출하는 구조
- Presenter는 가상 함수 Enter, Loading, Exit 등을 재정의하여 사용
- Enter : UI 진입 시 가장 먼저 처리되어야 하는 작업을 담당
- LoadingProcess : 해당 UI와 관련된 리소스 로딩 처리를 담당
- RefreshUI : 해당 UI를 갱신하기 위해 사용
- GetUIPrefabs : 해당 UI가 어떤 UI인지 타입을 반환

3. Actor
- 상 하체 상태를 분리(와우, FPS 같은), 행동에 대해서는 Action class를 이용하여 구현
- FSM 구조로 구현
- Character, Monster, Npc가 Actor를 상속받으며
각각 CharacterController, MonsterController, NpcController를 통해 제어됌

4. AI
- 현재 미구현
- 각 AI기능들은 AI Class를 상속 받고 각 AIPattern Class에서 조합하여 사용할 예정
예)
AI_Attack : 특정 범위 내에 대상이 있으면 주먹으로 때린다.
AI_Move : 특정 범위 내에 대상이 있으면 다가간다.
AI_NormalPattern(이름 임시) : AI_Attack과 AI_Move를 AI형으로 의존관계를 맺고
우선순위에 따라 순차적으로 실행

5. Server
- TCP/IP
- C#으로 구현됌
- DB는 스프레드시트를 이용
- WEB은 스프레드시트의 스크립트편집기(자바스크립트) 이용
