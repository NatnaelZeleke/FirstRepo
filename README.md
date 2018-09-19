

#
# AddisBid Installation guide

Contents

AddisBid Installation guide        1

1 Requirements        3

2 Create Virtual Local host website name        3

3 Refresh Local Machine DNS        3

4 Turn on Internet Information Service        3

5 Setup Local IIS Server Manager for this project        4

6 Map Localhost to the [www.AddisBid.com] on Local IIS Server        4

7 Add Access Permission for Visual Studio Project to IIS Server        4

7.1 Add Everyone and then NETWORK SERVICE        4

8 Setup SQL Server Database        5

9 Add SQL Server Login Permissions        5

9.1 From IIS Manager Side        5

9.2 Fromm SQL Server Manager        5

10 Setup Visual Studio        6

**Notice**         6















## 1 Requirements

1. Visual Studio IDE community edition or else
2. SQL Server 2014
3. IIS server Manager
4. .NET 4.5 or latest Versions

Note Before opening the project follow the following steps.

## 2 Create Virtual Local host website name

1. Run Notepad as Administrator
2. Open with the Notepad C:\Windows\System32\drivers\etc\hosts file
3. Add the following to end of the file **127.0.0.1** [**www.AddisBid.com**](http://www.AddisBid.com)
4. Save the file

## 3 Refresh Local Machine DNS

- Open command prompt
- Run the following command, so that it will delete any IP to domain name link:

**ipconfig /flushdns**

## 4 Turn on Internet Information Service

1. Go to Control panel =&gt; Uninstall Programs =&gt; on the left panel Click on

**#Turn Windows features on or off.**

1. Find Internet Information Services, check on it
2. Drop down and check Web Management Tools and World Wide Web Services
3. Drop down on World Wide Web Services and then drop down on

**#Application Development Features, select all features except CGI**

1. Select OK, and wait for it till it finishes and restart if needed.

## 5 Setup Local IIS Server Manager for this project

1. Run IIS server as Administrator
2. On the left, you will see a drop down. Go till you find Sites.
3. Right Click Sites and click Add Websites
4. Give it a site name AddisBid
5. Then click Select, a window will pop up.
6. Under Application pool, select DefaultAppPool and click Ok.
7. For Physical path: give it the root of the AddisBid visual studio project
8. Then click Ok.
9. For Host name, give it the above URL you entered on the hosts file. [[AddisBid.com](http://www.AddisBid.com)]
10. Uncheck Start Websites immediately, then click OK.

## 6 Map Localhost to the [[www.AddisBid.com](http://www.AddisBid.com)] on Local IIS Server

1. Right click Stavimer site on IIS manager and click Edit Binding. And click Add
2. For the hostname give it: localhost
3. It may show warning, just click OK. And make sure when your run the app, port 80 is not taken. Apps like Skype takes the port. Even on the IIS Manager, make sure your started only one site.

## 7 Add Access Permission for Visual Studio Project to IIS Server

### **7.1Add Everyone and then NETWORK SERVICE**

1. Click on the AddisBid, and on the right panel click Edit permissions.
2. Uncheck Read only on the General Tab
3. Go to Security Tab and Click Edit
4. Click Add
5. On the text field insert everyone and click ok.
6. Now click on everyone, and under Permissions for Everyone, check Full Control Allow.
7. Click Ok, then Click Apply (this will ask you to do for all subfolders), then click Ok
8. Finally, click OK.
9. DO THE SAME to add **NETWORK SERVICE** like Everyone

## 8 Setup SQL Server Database

1. To run the SQL file, open SQL Server
2. give it the user name **(local)** including the braces or your SQL Server credentials
3. Right Click on the Databases folder, click new database and give it the name
  - **AddisBidDb**
4. Refresh the Databases folder and **AddisBidDb** will show up on the list
5. Now, Right click **AddisBidDb** and click New Query
6. Copy paste the query file that is included with this documentation and click
7. It might show an error that the database exists but don&#39;t mind. Just refresh **AddisBidDb** , the tables will be created.

## 9 Add SQL Server Login Permissions

### **9.1From IIS Manager Side**

1. Start IIS server as Administrator
2. On the left panel click on Application Pool
3. Select DefaulAppPool and on the right panel select Advance Settings
4. Find Identity and select LocalSystem and click Ok.

### 9.2 Fromm SQL Server Manager

1. Start SQL Server as Administrator
2. Login to Server name where **AddisBidDb** is
3. Go to Security under the connection name
4. Drop down Security and the drop down on logins
5. Right click on NT AUTHORITY\SYSTEM and select Properties
6. Click on Server role on the left panel
7. Check sysadmin
8. Click OK.





## 10 Setup Visual Studio

1. Run Visual Studio as Administrator:
2. Open AddisBid project
3. Right Click on AddisBid.Web, then click properties
4. Click Web
5. Under Servers sections
  - On the drop down choose Local IIS.
  - On Project URL text field, add the above website URL you entered to hosts file. In this case [[AddisBid.com](http://www.AddisBid.com)]
  - Then click Create Virtual Directory button. It may ask you to remap it, just click ok.

### Notice

After you did the above setups, whenever you start Visual Studio run it as Administrator because it is going to access IIS server manager and to access IIS server manager you have to have an administrator privilege and make sure port 80 is not taken by other applications.

This installation guide was taken from my brilliant friend Ashenafi goitom. Prepared by Natnael Zeleke

AddisBid Sep, 21, 2017

