using UnityEngine;
using System.Collections;
using System.Linq;

public class MonsterGenerator : MonoBehaviour
{
    public int monsterCount;
    public Object monster;
    private Transform[] WayPoints = null;

    private void Awake()
    {
        monster = Resources.Load("Enemy");

        //Find all gameobjects with waypoint
        GameObject[] Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //Select all transform components from waypoints using Linq
        WayPoints = (from GameObject GO in Waypoints
                     select GO.transform).ToArray();
    }

    private void Start()
    {
        StartCoroutine(MonsterGenerate());
    }

    IEnumerator MonsterGenerate()
    {
        int count = 0;

        while(true)
        {
            if(count >= monsterCount)
            {
                yield return null;
                continue;
            }

            Instantiate(monster, WayPoints[Random.Range(0, WayPoints.Length)].position, Quaternion.identity, transform);
            monsterCount++;
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
        }
    }


}
