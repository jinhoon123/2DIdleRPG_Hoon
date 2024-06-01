using CHV;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Roots

    [SerializeField] public Transform mainCharacterRoot;
    [SerializeField] public Transform monsterRoot;
    [SerializeField] public Transform skillRoot;

    #endregion
    
    #region Handler

    public BackgroundHandler backHandler;
    public BackgroundHandler groundHandler;
    
    #endregion
    
    public GameObject mainCharacterPrefab;
    public GameObject monsterPrefab;
    
    private void Awake()
    {
        SessionManager.I.Initialize();
        SessionManager.I.RequestSession(GameEnums.eSession.Game);
        
        // 임시 코드
        backHandler.OnScrollCompletedEvent += () =>
        {
            SessionManager.I.RequestSession(GameEnums.eSession.Game);
        };
    }
}