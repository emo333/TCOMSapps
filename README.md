# TCOMSapps - Out Of State Title Tracking

## Note (8/1/2025): Do not use this project as-is due to it being based out-of-date .net dependencies.

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

* Configure IIS (note: you can run without IIS but not recommended for security concerns and complexity)
  * Create Virtual Directory  
  * Add Website Files - ALL folders and files in [Release](https://github.com/emo333/TCOMSapps/tree/master/Release) to virtual directory folder
  * Configure App Pool
  * Bind ports
* Configure SQL Server
  * Create Database "TCOMSApps"
  * Configure SQLServer Login for TCOMSapps Database
  * Get Connection String
* Configure WebSite Files
  * Change Connection String setting in [appsettings.json](TCOMSapps/TCOMSapps/appsettings.json) to match your SQLServer Connection string
  * Add County Icon image file by replacing nctcseal.jpg with your county logo file (the file in your IIS virtual directory) Use same file name {this is the image used for letters}
  * OPTIONAL{requires recompiling your own release}: The first run of the application will seed the database with demo data.  You can change this demo data by changing [AppInitializer.cs](TCOMSapps/TCOMSapps/Data/AppInitializer.cs)
  
### Initial TCOMSapps Configuration

* Log in as SuperUser [Default SuperUser Login: a@a.com PW: 1234Abcd!]
* Change SuperUser Default Password (Don't forget!}
* Change County Settings (County Specific Settings such as County Name, Official's Name, Notifications Letters' specific info such as address to be show on letters)
* Change Application Settings (Your SMTP Mail Server Settings to be used to send notification emails to customers)
* Change/Add Locations settings	

### End Users

* User registers a user account
* A user with "TCOMS Apps Administrator" Role adds a registered user to "OOSTitles User" Role
