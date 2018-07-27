# TCOMSapps - Out Of State Title Tracking

### Requirements

 #### Recommended:
 * Windows Server 2012+ 
   * IIS 7+ 
   * SQL Server Express
 
 #### Optional:
 * Windows 7+
   * IIS 7+
   * SQL Server Express
   * SQL Server Standard on separate server
 * Linux
   * SQL Server Standard on separate server
  
### Install Steps

* Configure IIS  
  * Create Virtual Directory  
  * Add Website Files
  * Configure App Pool
  * Bind ports
* Configure SQL Server
  * Create Database "TCOMSApps"
  * Configure SQLServer Login for TCOMSapps Database
  * Get Connection String
* Configure WebSite Files
  * Change Connection String setting in [appsettings.json](TCOMSapps/TCOMSapps/appsettings.json) to match your SQLServer Connection string
  * Add County Icon image file by replacing logo.jpg with your county logo file
  ** OPTIONAL{requires recompiling your own release}: The first run of the application will seed the database with demo data.  You can change this demo data by changing [AppInitializer.cs](TCOMSapps/TCOMSapps/Data/AppInitializer.cs)
  
### Initial TCOMSapps Configuration

* Log in as SuperUser [Default SuperUser Login: a@a.com PW: 1234Abcd!]
* Change SuperUser Default Password (Don't forget!}
* Change County Settings (County Specific Settings such as County Name, Official's Name, Notifications Letters' specific info such as address to be show on letters)
* Change Application Settings (Your SMTP Mail Server Settings to be used to send notification emails to customers)
* Change/Add Locations settings	

### End Users

* User registers a user account
* A user with "TCOMS Apps Administrator" Role adds a registered user to "OOSTitles User" Role
