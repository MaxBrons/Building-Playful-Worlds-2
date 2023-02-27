using SimpleDungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        None,
        Started,
        Active,
        Paused,
        Ended
    }

    [SerializeField] private GameObject m_Player;

    private GameState m_GameState = GameState.None;
    private DungeonGeneratorSimple m_DungeonGenerator;


    // Start is called before the first frame update
    void Start()
    {
        m_DungeonGenerator = FindObjectOfType<DungeonGeneratorSimple>();

        if (!m_Player || !m_DungeonGenerator)
            return;

        StartCoroutine(SpawnPlayerDelayed());
    }

    public GameState GetGameState() => m_GameState;

    private IEnumerator SpawnPlayerDelayed()
    {
        while (m_DungeonGenerator.roomList.Count == 0)
            yield return null;

        Instantiate(m_Player, (Vector3)m_DungeonGenerator.roomList[0].GetRandomPositionInRoom(), Quaternion.identity);
    }
}
