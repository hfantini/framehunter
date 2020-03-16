<?php

	/*

		+ =====================================================================
		|                                                            
		|	== CODECITADEL STUDIOS @ 2016                              
		|   == FRAME HUNTER - WEBSERVICES                                   
		|	====================================                     
		|   ==    FILE: DatabaseConnection.php           						 
		|	==    DATE (YYYY-MM-DD): 2017-07-25 | TIME (HH:MM:SS): 13:13:00 PM       
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

	class DatabaseConnection
	{
		// == DECLARATION
		// ======================================================================================
		
		// == CONST =============================================================================
		
		private $DB_SERVER_NAME = "codecitadelcom.fatcowmysql.com";
		private $DB_SERVER_PORT = "3306";
		private $DB_SCHEMA = "codecitadel_framehunter";
		private $DB_USER = "codecita_user";
		private $DB_PASS = "usercodeciTA@2017#!";
		
		// == VAR ===============================================================================
		
		public static $instance;
		private $dbConnection;
		private $dbSelected;
		
		// == CLASS CONSTRUCTOR(S)
		// ======================================================================================
		
		public function __construct()
		{
			$dbConnection = null;
		}		
		
		// == CLASS METHODS
		// ======================================================================================
			
		public function connect()
		{
			$this->dbConnection = mysql_connect($this->DB_SERVER_NAME, $this->DB_USER, $this->DB_PASS);
			
			if(!$this->dbConnection) 
			{
				die('CONNECTION ERROR : ' . mysql_error());
			}
			
			$this->dbSelected = mysql_select_db("codecitadel_framehunter", $this->dbConnection);
			
			if(!$this->dbSelected ) 
			{
				die (" CAN'T SELECT DB : " . mysql_error());
			}			
		}
		
		public function getConnection()
		{
			if($this->dbConnection == null)
			{
				$this->connect();
			}
			
			return $this->dbConnection;
		}
			
		public function disconnect()
		{
			mysql_close($this->dbConnection);
		}
			
		// == CLASS EVENTS
		// ======================================================================================
		
		// == GETTERS AND SETTERS
		// ======================================================================================	
	}
	
	DatabaseConnection::$instance = new DatabaseConnection();
	
?>
