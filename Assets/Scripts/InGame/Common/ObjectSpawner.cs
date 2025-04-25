using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnItemPattern
{
    Phase1,
    Phase2,
    Phase3,
    Phase4,
    Phase5
}

public class ObjectSpawner : MonoBehaviour
{
    private static readonly float leftOffSetX = -0.8f;
    private static readonly float middleOffSetX = 0f;
    private static readonly float rightOffSetX = 0.8f;

    private static readonly float spaceDoorLeftOffSetX = -0.65f;
    private static readonly float spaceDoorMiddleOffSetX = 0f;
    private static readonly float spaceDoorRightOffSetX = 0.65f;

    [Header("Pattern Delay")]
    [Space]
    [SerializeField] private float ChangingPatternDelay = 3f;
    [SerializeField] private float ItemSpawnRate = 0.5f;
    
    [Header("Item Prefabs")]
    [Space]
    [SerializeField] private GameObject AttackUP;
    [SerializeField] private GameObject Life;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private GameObject GoodSpaceDoor;
    [SerializeField] private GameObject BadSpaceDoor;
    [SerializeField] private GameObject EndPoint;

    private void Start()
    {
        int currentChapterNum = UserDataManager.Instance.GetUserData<UserChapterData>().CurrentChapterNum;

        string filePath = "StageData/Stage" + currentChapterNum;
        StageData stageData = Resources.Load<StageData>(filePath);

        StartCoroutine(RunPatternSequence(stageData.GetPatternList()));
    }

    public void StartSpawnItem(int patternNum)
    {
        string PatternName = "Phase" + patternNum;
        StartCoroutine(PatternName);
    }

    public void StopSpawnItem(int patternNum)
    {
        string PatternName = "Phase" + patternNum;
        StopCoroutine(PatternName);
    }

    private IEnumerator RunPatternSequence(List<int> patternList)
    {
        foreach (var pattern in patternList)
        {
            string coroutineName = "Phase" + pattern;
            StartCoroutine(coroutineName);
            yield return new WaitForSeconds(ChangingPatternDelay);
        }
    }

    private IEnumerator Phase1()
    {
        int count = 5;
        float randomOffset;

        switch (Random.Range(-1, 2))
        {
            case -1:
                randomOffset = leftOffSetX;
                break;
            case 0:
                randomOffset = middleOffSetX;
                break;
            case 1:
                randomOffset = rightOffSetX;
                break;
            default:
                randomOffset = middleOffSetX;
                break;
        }

        for (int i = 0; i < count - 1; i++)
        {
            Instantiate(AttackUP, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
            yield return new WaitForSeconds(ItemSpawnRate);
        }

        Instantiate(Life, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
    }

    private IEnumerator Phase2()
    {
        int count = 5;
        float randomOffset;

        switch (Random.Range(-1, 2))
        {
            case -1:
                randomOffset = leftOffSetX;
                break;
            case 0:
                randomOffset = middleOffSetX;
                break;
            case 1:
                randomOffset = rightOffSetX;
                break;
            default:
                randomOffset = middleOffSetX;
                break;
        }

        for (int i = 0; i < count - 1; i++)
        {
            Instantiate(AttackUP, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
            yield return new WaitForSeconds(ItemSpawnRate);
        }

        Instantiate(Bomb, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
    }

    private IEnumerator Phase3()
    {
        int count = 5;
        float randomOffset;

        switch (Random.Range(-1, 2))
        {
            case -1:
                randomOffset = leftOffSetX;
                break;
            case 0:
                randomOffset = middleOffSetX;
                break;
            case 1:
                randomOffset = rightOffSetX;
                break;
            default:
                randomOffset = middleOffSetX;
                break;
        }

        for (int i = 0; i < count; i++)
        {
            Instantiate(AttackUP, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
            yield return new WaitForSeconds(ItemSpawnRate);
        }
    }

    private IEnumerator Phase4()
    {
        float randomOffset;

        switch (Random.Range(-1, 2))
        {
            case -1:
                randomOffset = spaceDoorLeftOffSetX;
                break;
            case 0:
                randomOffset = spaceDoorMiddleOffSetX;
                break;
            case 1:
                randomOffset = spaceDoorRightOffSetX;
                break;
            default:
                randomOffset = spaceDoorMiddleOffSetX;
                break;
        }

        switch (Random.Range(0,1))
        {
            case 0:
                Instantiate(GoodSpaceDoor, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(BadSpaceDoor, transform.position + new Vector3(randomOffset, 0), Quaternion.identity);
                break;
            default:
                break;
        }

        yield return null;
    }

    private IEnumerator Phase5()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
            Instantiate(GoodSpaceDoor, transform.position + new Vector3(spaceDoorLeftOffSetX, 0), Quaternion.identity);
        else
            Instantiate(BadSpaceDoor, transform.position + new Vector3(spaceDoorLeftOffSetX, 0), Quaternion.identity);

        randNum = Random.Range(0, 2);
        if (randNum == 0)
            Instantiate(GoodSpaceDoor, transform.position + new Vector3(spaceDoorRightOffSetX, 0), Quaternion.identity);
        else
            Instantiate(BadSpaceDoor, transform.position + new Vector3(spaceDoorRightOffSetX, 0), Quaternion.identity);

        yield return null;
    }

    private IEnumerator Phase6()
    {
        Instantiate(EndPoint, transform.position, Quaternion.identity);
        yield return null;
    }
}
