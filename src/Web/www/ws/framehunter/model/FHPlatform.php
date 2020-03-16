<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: FHPlatform.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 11:17:00 AM              
		|   ==   SINCE: 0.0.1a				                         
		|	==  AUTHOR: Henrique Fantini                             
		|   == CONTACT: codecraft@outlook.com                        
		|   ====================================                     
		|   ==                                                       

	*/

	// == IMPORT LIBS
	// ==========================================================================================

	// == CLASS
	// ==========================================================================================

	class FHPlatform
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================
		
		private $plaID;
		private $plaName;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		public function __construct()
		{
			$this->plaID = 0;
			$this->plaName = "";
		}
		
		// == CLASS METHODS
		// ======================================================================================
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
		
		// == PLATFORM ID
	
		public function getPlaID()
		{
			return $this->plaID;
		}
		
		public function setPlaID($plaID)
		{
			$this->plaID = $plaID;
		}
		
		// == PLATFORM NAME
	
		public function getPlaName()
		{
			return $this->plaName;
		}
		
		public function setPlaName($plaName)
		{
			$this->plaName = $plaName;
		}		
	}

?>
