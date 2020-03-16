/*

	+ =====================================================================
	|                                                            
	|	== CODECASTLE STUDIOS @ 2016                              
	|   == FRAME HUNTER - GAME                                   
	|	====================================                     
	|   ==    FILE: MenuContainer.cs           						 
	|	==    DATE (YYYY-MM-DD): 2017-07-14 | TIME (HH:MM:SS): 09:01:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
*/

// == IMPORT LIBS
// ==========================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// == CLASS
// ==========================================================================================

public abstract class MenuContainer
{
    // == DECLARATION
    // ======================================================================================

    // == CONST =============================================================================

    // == VAR ===============================================================================

    // == STATES
    protected EnumMenuContainerState _menuContainerState;

    // == SPRITES
    protected GameObject _containerContents;

    // == VALUES
    protected MonoBehaviour _parent;

    // == CLASS CONSTRUCTOR(S)
    // ======================================================================================

    public MenuContainer(MonoBehaviour parent)
    {
        this._parent = parent;
    }

    // == CLASS METHODS
    // ======================================================================================

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void enable(bool value)
    {
        _containerContents.SetActive(value);
    }

	// == CLASS EVENTS
	// ======================================================================================
	
	// == GETTERS AND SETTERS
	// ======================================================================================	

    public EnumMenuContainerState menuContainerState
    {
        get { return this._menuContainerState; }
        set { this._menuContainerState = value; }
    }
}
