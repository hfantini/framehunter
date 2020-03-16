<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: FHScore.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 11:22:00 AM              
		|   ==   SINCE: 0.0.1a				                         
		|	==  AUTHOR: Henrique Fantini                             
		|   == CONTACT: codecraft@outlook.com                        
		|   ====================================                     
		|   ==                                                       

		
	*/

	// == IMPORT LIBS
	// ==========================================================================================

	include_once("FHUserIdentification.php");
	
	// == CLASS
	// ==========================================================================================

	class FHScore
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================
		
		private $scoID;
		private $scoScore;
		private $scoLevel;
		private $scoTime;
		private $userIdentification;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		public function __construct()
		{
			$this->scoID = 0;
			$this->scoScore = 0;
			$this->userIdentification = new FHUserIdentification();
		}		
		
		// == CLASS METHODS
		// ======================================================================================
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
		
		// == LEADER ID
	
		public function getScoID()
		{
			return $this->scoID;
		}
		
		public function setScoID($scoID)
		{
			$this->scoID = $scoID;
		}
		
		// == LEADER SCORE
	
		public function getScoScore()
		{
			return $this->scoScore;
		}
		
		public function setScoScore($scoScore)
		{
			$this->scoScore = $scoScore;
		}		
		
		// == LEADER LEVEL
		
		public function getScoLevel()
		{
			return $this->scoLevel;
		}
		
		public function setScoLevel($scoLevel)
		{
			$this->scoLevel = $scoLevel;
		}			
		
		// == LEADER TIME
		
		public function getScoTime()
		{
			return $this->scoTime;
		}
		
		public function setScoTime($scoTime)
		{
			$this->scoTime = $scoTime;
		}			
		
		// == USER INDENTIFICATION
	
		public function getUserIdentification()
		{
			return $this->userIdentification;
		}
					
	}

?>
