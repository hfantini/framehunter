/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: Level7.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-08-16 | TIME (HH:MM:SS): 09:20:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
*/

// == IMPORT LIBS
// ==========================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// == CLASS
// ==========================================================================================

public class Level7 : Level
{

    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    private const float MIN_CAMERAAIM_POS_X = -1;
    private const float MAX_CAMERAAIM_POS_X = 1;
    private const float VEL_CAMERAAIM_X = 5f;
    private const float MAX_WAGONS = 25;

    // == VAR ===============================================================================

    private Vector3 movementDirection;
    private Vector3 movementDirectionAIM;
    private Vector3 originalAimPosition;
    private Boolean calcFlip;
    private float flipPointX;
    private GameObject _targetAuxLoc;
    private GameObject _targetAuxWagon;
    private List<GameObject> _wagonList;
    private int velX;
    private int createdWagons = 0;
    private float percentUnit = 0;
    private float currentChance = 0;
    private bool targetWagonPlaced = false;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    // == CLASS METHODS
    // ======================================================================================

    protected override void loadLevel()
    {
        _levelNumber = 7;
        _levelBackground = Resources.Load<Sprite>("Sprite/sprLevel_7");
        _startPosition = new Vector3(-10f, 0.9f, sprTarget.transform.position.z);

        velX = 8;

        _targetAuxLoc = GameObject.Find("sprLevel7AuxLoc");
        _targetAuxLoc.GetComponent<SpriteRenderer>().enabled = true;
        _targetAuxLoc.transform.position = new Vector3(-50f, 1.74f, _targetAuxLoc.transform.position.z);

        _targetAuxWagon = GameObject.Find("sprLevel7AuxWagon");
        _targetSprite = Resources.Load<Sprite>("Sprite/sprLevel_7_target");

        _wagonList = new List<GameObject>();

        // CALC PERCENT UNIT
        percentUnit = 1; //100f / MAX_WAGONS;


    }

    public override void resetLevel()
    {
        base.resetLevel();

        _targetAuxLoc.transform.position = new Vector3(-50f, 1.74f, _targetAuxLoc.transform.position.z);

        foreach (GameObject obj in _wagonList)
        {
            if (obj.name != "sprLevelTarget")
            {
                script.destroySprite(obj);
            }
        }

        _wagonList.Clear();

        createdWagons = 0;
        currentChance = 0;
        targetWagonPlaced = false;
    }

    public override void update()
    {
        if(_wagonList.Count == 0 )
        {
            if (!targetWagonPlaced)
            {
                createNewWagon();
            }
            else
            {
                script.curState = EnumGameState.GAMESTATE_CHECKSNAP;
            }
        }
        else
        {
            if (_wagonList[0].transform.position.x >= -Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).x )
            {
                createNewWagon();
            }

            if(_wagonList[_wagonList.Count - 1].transform.position.x - _wagonList[_wagonList.Count - 1].GetComponent<SpriteRenderer>().bounds.size.x >= Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).x)
            {
                GameObject wagon = _wagonList[_wagonList.Count - 1];
                _wagonList.RemoveAt(_wagonList.Count - 1);

                if (wagon.name != "sprLevelTarget")
                {
                    script.destroySprite(wagon);
                }
            }
        }

        _targetAuxLoc.transform.Translate((Vector3.right * velX) * Time.deltaTime, Space.World);

        foreach (GameObject obj in _wagonList)
        {
            obj.transform.Translate( (Vector3.right * velX) * Time.deltaTime, Space.World);
        }
    }

    private void createNewWagon()
    {
        if (createdWagons <= MAX_WAGONS)
        {
            GameObject lastWagon = null;
            GameObject createdWagon = null;
            bool isTargetWagon = false;

            if (targetWagonPlaced == false)
            {
                // == RANDOMIZE TARGET WAGON
                currentChance += percentUnit;
                int randonFactor = UnityEngine.Random.Range(0, 101);

                if (randonFactor < currentChance)
                {
                    isTargetWagon = true;
                }
                else if (createdWagons == MAX_WAGONS - 1)
                {
                    isTargetWagon = true;
                }
            }

            // == CREATE WAGON

            if (_wagonList.Count == 0)
            {
                lastWagon = _targetAuxLoc;

                if (!isTargetWagon)
                {
                    createdWagon = GameObject.Instantiate(_targetAuxWagon, _targetAuxWagon.transform.parent);
                    createdWagon.transform.position = new Vector3(lastWagon.transform.position.x - createdWagon.GetComponent<SpriteRenderer>().bounds.size.x / 2, lastWagon.transform.position.y - 0.25f, lastWagon.transform.position.z);
                }
                else
                {
                    createdWagon = sprTarget;
                    createdWagon.transform.position = new Vector3(lastWagon.transform.position.x - createdWagon.GetComponent<SpriteRenderer>().bounds.size.x / 2, lastWagon.transform.position.y - 0.25f, lastWagon.transform.position.z);
                    targetWagonPlaced = true;
                }
            }
            else
            {
                lastWagon = _wagonList[0];

                if (!isTargetWagon)
                {
                    createdWagon = GameObject.Instantiate(_targetAuxWagon, _targetAuxWagon.transform.parent);
                    createdWagon.transform.position = new Vector3(lastWagon.transform.position.x - lastWagon.GetComponent<SpriteRenderer>().bounds.size.x, lastWagon.transform.position.y, lastWagon.transform.position.z);
                }
                else
                {
                    createdWagon = sprTarget;
                    createdWagon.transform.position = new Vector3(lastWagon.transform.position.x - lastWagon.GetComponent<SpriteRenderer>().bounds.size.x, lastWagon.transform.position.y, lastWagon.transform.position.z);
                    targetWagonPlaced = true;
                }
            }

            createdWagon.GetComponent<SpriteRenderer>().enabled = true;
            _wagonList.Insert(0, createdWagon);

            createdWagons++;
        }
    }

    // == CLASS EVENTS
    // ======================================================================================

    // == GETTERS AND SETTERS
    // ======================================================================================	
}
