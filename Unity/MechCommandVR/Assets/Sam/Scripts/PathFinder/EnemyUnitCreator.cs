using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitCreator : MonoBehaviour
{
    public GameObject UnitPrefab;
    Transform parentPosition;
    Vector3 NewUnitPositon;
    UnitComponent newUnit;
    List<UnitComponent> unitsWaiting;
    [SerializeField]
    PathNode[] staringNodes = new PathNode[3];
    PathNode SelectedRoute;
    Random Random;

    public float CurrentRescource;
    public float RequiredRescource;
    public float RescourceGenerationRate;

    // Start is called before the first frame update
    void Start()
    {
        unitsWaiting = new List<UnitComponent>();
        Random = new Random();
        parentPosition = GetComponentInParent<Transform>();
        NewUnitPositon.x = parentPosition.position.x + Random.Range(-2f, 2f);
        NewUnitPositon.z = parentPosition.position.z + Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            CurrentRescource += 5f;
        CurrentRescource += (Time.deltaTime * RescourceGenerationRate);
        if(CurrentRescource >= RequiredRescource)
        {
            NewUnitPositon.x = parentPosition.position.x + Random.Range(-2f, 2f);
            NewUnitPositon.z = parentPosition.position.z + Random.Range(-2f, 2f);
            NewUnitPositon.y = 0;
            //Create new unit
            newUnit = Instantiate(UnitPrefab, NewUnitPositon, Quaternion.identity).GetComponent<UnitComponent>();
            //Add to list
            unitsWaiting.Add(newUnit);
            //Remove cost of unit
            CurrentRescource -= RequiredRescource;
        }

        if(unitsWaiting.Count >= 3)
        {
            //Assign random starting node
            SelectedRoute = staringNodes[Random.Range(0, 2)];
            //REMOVE BEFORE RELEASE
            //SelectedRoute = staringNodes[0];
            //Set starting node
            unitsWaiting.ForEach(uw => uw.GetComponentInParent<AIPathFollower>().CurrentNode = SelectedRoute);
            //Tell units to start moving
            unitsWaiting.ForEach(uw => uw.GetComponentInParent<AIPathFollower>().Invoke("MoveToPathNode", 1));
            //Clear waiting list
            unitsWaiting.Clear();
        }
    }
}
