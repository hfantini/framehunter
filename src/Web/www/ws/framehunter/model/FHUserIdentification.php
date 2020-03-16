<?php 

/*

	+ =====================================================================
	|                                                            
	|	== CODECITADEL STUDIOS @ 2016                              
	|   == FRAME HUNTER - WEBSERVICES                                   
	|	====================================                     
	|   ==    FILE: FHUserIdentification.php          						 
	|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 09:33:00 AM              
	|   ==   SINCE: 0.0.1a				                         
	|	==  AUTHOR: Henrique Fantini                             
	|   == CONTACT: codecraft@outlook.com                        
	|   ====================================                     
	|   ==                                                       

	
*/

// == IMPORT LIBS
// ==========================================================================================

include_once("FHPlatform.php");

// == CLASS
// ==========================================================================================

class FHUserIdentification
{
	// == DECLARATION
	// ======================================================================================
	
	// == CONST =============================================================================
	
	// == VAR ===============================================================================
	
	private $uidID;
	private $uidName;
	private $uidIPADDR;
	private $uidMACADDR;
	private $platform;
	
	// == CLASS CONSTRUCTOR(S)
	// ======================================================================================
	
	public function __construct()
	{
        $this->uidID = 0;
		$this->uidName = "";
		$this->uidIPADDR = "";
		$this->uidMACADDR = "";
		$this->platform = new FHPlatform();
	}
		
	// == CLASS METHODS
	// ======================================================================================
		
	// == CLASS EVENTS
	// ======================================================================================
	
	// == GETTERS AND SETTERS
	// ======================================================================================	
	
	// == UID
	
	public function getUidID()
	{
		return $this->uidID;
	}
	
	public function setUidID($uidID)
	{
		$this->uidID = $uidID;
	}
	
	// == NAME
	
	public function getUidName()
	{
		return $this->uidName;
	}
	
	public function setUidName($uidName)
	{
		$this->uidName = $uidName;
	}	
	
	// == IP ADDRESS
	
	public function getUidIPADDR()
	{
		return $this->uidIPADDR;
	}
	
	public function setUidIPADDR($uidIPADDR)
	{
		$this->uidIPADDR = $uidIPADDR;
	}		
	
	// == MAC ADDRESS
	
	public function getUidMACADDR()
	{
		return $this->uidMACADDR;
	}
	
	public function setUidMACADDR($uidMACADDR)
	{
		$this->uidMACADDR = $uidMACADDR;
	}			
	
	// == PLATFORM
	
	public function getPlatform()
	{
		return $this->platform;
	}
				
}
