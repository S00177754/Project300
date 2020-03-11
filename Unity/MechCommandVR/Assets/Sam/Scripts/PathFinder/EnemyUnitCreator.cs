using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitCreator : MonoBehaviour
{
    public GameObject UnitPrefab;
    Transform parentPosition;
    Vector3 NewUnitPositon;
    
    List<UnitComponent> unitsWaiting;
    [SerializeField]
    PathNode[] staringNodes = new PathNode[3];
    PathNode SelectedRoute;
    public BaseController Base;
    Random Random;
    int id;

    public float CurrentRescource;
    public float RequiredRescource;
    public float RescourceGenerationRate;

    // Start is called before the first frame update
    void Start()
    {
        unitsWaiting = new List<UnitComponent>();
        Random = new Random();
        parentPosition = GetComponent<Transform>();
        NewUnitPositon.x = parentPosition.position.x + Random.Range(-2f, 2f);
        NewUnitPositon.z = parentPosition.position.z + Random.Range(-2f, 2f);
        id = 1;
        //Base = GetComponent<BaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameState.Battling)
        {
            CurrentRescource += (Time.deltaTime * RescourceGenerationRate);
            if (CurrentRescource >= RequiredRescource)
            {
                NewUnitPositon.x = parentPosition.position.x + Random.Range(-2f, 2.1f);
                NewUnitPositon.z = parentPosition.position.z + Random.Range(-2f, 2.1f);
                NewUnitPositon.y = 0;


                //Create new unit
                GameObject go = Instantiate(UnitPrefab, NewUnitPositon, Quaternion.identity);
                UnitDetails newUnit = go.GetComponent<UnitDetails>();
                //Set details of new unit
                //ID for enemy units only instanciated here, ID is managed internally 
                newUnit.SetDetails(Base.Owner, id, 1f, 0.2f, 1f);
                id++;
                //Add to list
                unitsWaiting.Add(go.GetComponent<UnitComponent>());
                //Remove cost of unit
                CurrentRescource -= RequiredRescource;
            }

            if (unitsWaiting.Count >= 3)
            {
                //Assign random starting node
                SelectedRoute = staringNodes[Random.Range(0, 3)];
                //REMOVE BEFORE RELEASE
                //SelectedRoute = staringNodes[0];
                //Set starting node
                //unitsWaiting.ForEach(uw => uw.GetComponent<AIPathFollower>().CurrentNode = SelectedRoute);
                foreach (var uw in unitsWaiting)
                {
                    if(uw != null)
                    uw.GetComponent<AIPathFollower>().CurrentNode = SelectedRoute;
                    uw.GetComponent<AIPathFollower>().Invoke("MoveToPathNode", 1);
                }
                
                //Tell units to start moving
                //unitsWaiting.ForEach(uw => uw.GetComponent<AIPathFollower>().Invoke("MoveToPathNode", 1));
                //Clear waiting list
                unitsWaiting.Clear();
            }
        }
    }
}
