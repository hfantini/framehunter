<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: FHBanlistMAC.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 11:30:00 AM              
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

	class FHBanlistMAC
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		// == VAR ===============================================================================
		
		private $banID;
		private $banMACADDR;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		public function __construct()
		{
			$this->banID = 0;
			$this->banMACADDR = "00:00:00:00:00:00";
		}				
		
		// == CLASS METHODS
		// ======================================================================================
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
		
		// == BANLIST ID
	
		public function getBanID()
		{
			return $this->banID;
		}
		
		public function setBanID($banID)
		{
			$this->banID = $banID;
		}		
		
		// == BANLIST MAC ADDRESS
	
		public function getBanMACADDR()
		{
			return $this->banMACADDR;
		}
		
		public function setBanMACADDR($banMACADDR)
		{
			$this->banMACADDR = $banMACADDR;
		}			
	}

?>
