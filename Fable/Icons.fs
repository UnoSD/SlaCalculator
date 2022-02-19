module SlaCalculator.Icons



module Azure =
    // https://docs.microsoft.com/en-us/azure/architecture/icons/
    // find . -type f | cut -d'/' -f2- | sed 's/\(.*\/\)\([0-9]*-icon-service-\)\(.*\)\(\.svg\)/\"\3\", \"\1\2\3\4\"/' >> ../Icons.fs

    let iconsMap =
        [
            "Analysis-Services", "Azure/Analytics/10148-icon-service-Analysis-Services.svg"
            "Stream-Analytics-Jobs", "Azure/Analytics/00042-icon-service-Stream-Analytics-Jobs.svg"
            "Azure-Synapse-Analytics", "Azure/Analytics/00606-icon-service-Azure-Synapse-Analytics.svg"
            "Azure-Databricks", "Azure/Analytics/10787-icon-service-Azure-Databricks.svg"
            "HD-Insight-Clusters", "Azure/Analytics/10142-icon-service-HD-Insight-Clusters.svg"
            "Data-Lake-Store-Gen1", "Azure/Analytics/10150-icon-service-Data-Lake-Store-Gen1.svg"
            "Log-Analytics-Workspaces", "Azure/Analytics/00009-icon-service-Log-Analytics-Workspaces.svg"
            "Event-Hubs", "Azure/Analytics/00039-icon-service-Event-Hubs.svg"
            "Event-Hub-Clusters", "Azure/Analytics/10149-icon-service-Event-Hub-Clusters.svg"
            "Updates", "Azure/Azure Stack/10115-icon-service-Updates.svg"
            "Offers", "Azure/Azure Stack/10110-icon-service-Offers.svg"
            "User-Subscriptions", "Azure/Azure Stack/10111-icon-service-User-Subscriptions.svg"
            "Plans", "Azure/Azure Stack/10113-icon-service-Plans.svg"
            "Infrastructure-Backup", "Azure/Azure Stack/10108-icon-service-Infrastructure-Backup.svg"
            "Multi-Tenancy", "Azure/Azure Stack/00965-icon-service-Multi-Tenancy.svg"
            "Azure-Stack", "Azure/Azure Stack/10114-icon-service-Azure-Stack.svg"
            "Capacity", "Azure/Azure Stack/10109-icon-service-Capacity.svg"
            "Device-Security-Apple", "Azure/Intune/00399-icon-service-Device-Security-Apple.svg"
            "Azure-AD-Roles-and-Administrators", "Azure/Intune/10340-icon-service-Azure-AD-Roles-and-Administrators.svg"
            "Device-Security-Windows", "Azure/Intune/00399-icon-service-Device-Security-Windows.svg"
            "Intune", "Azure/Intune/10329-icon-service-Intune.svg"
            "Intune-For-Education", "Azure/Intune/10343-icon-service-Intune-For-Education.svg"
            "Device-Security-Google", "Azure/Intune/00399-icon-service-Device-Security-Google.svg"
            "System-Topic", "Azure/Integration/02073-icon-service-System-Topic.svg"
            "Azure-Data-Catalog", "Azure/Integration/10216-icon-service-Azure-Data-Catalog.svg"
            "Event-Grid-Domains", "Azure/Integration/10215-icon-service-Event-Grid-Domains.svg"
            "API-Management-Services", "Azure/Integration/10042-icon-service-API-Management-Services.svg"
            "SQL-Data-Warehouses", "Azure/Integration/00036-icon-service-SQL-Data-Warehouses.svg"
            "Logic-Apps", "Azure/Integration/10201-icon-service-Logic-Apps.svg"
            "Azure-API-for-FHIR", "Azure/Integration/10212-icon-service-Azure-API-for-FHIR.svg"
            "Relays", "Azure/Integration/10209-icon-service-Relays.svg"
            "Software-as-a-Service", "Azure/Integration/10213-icon-service-Software-as-a-Service.svg"
            "Event-Grid-Topics", "Azure/Integration/10206-icon-service-Event-Grid-Topics.svg"
            "Event-Grid-Subscriptions", "Azure/Integration/10221-icon-service-Event-Grid-Subscriptions.svg"
            "Partner-Topic", "Azure/Integration/02072-icon-service-Partner-Topic.svg"
            "Integration-Accounts", "Azure/Integration/10218-icon-service-Integration-Accounts.svg"
            "Outbound-Connection", "Azure/Blockchain/10364-icon-service-Outbound-Connection.svg"
            "ABS-Member", "Azure/Blockchain/10374-icon-service-ABS-Member.svg"
            "Consortium", "Azure/Blockchain/10375-icon-service-Consortium.svg"
            "Azure-Token-Service", "Azure/Blockchain/10367-icon-service-Azure-Token-Service.svg"
            "Azure-Blockchain-Service", "Azure/Blockchain/10366-icon-service-Azure-Blockchain-Service.svg"
            "Azure-Database-MariaDB-Server", "Azure/Databases/10123-icon-service-Azure-Database-MariaDB-Server.svg"
            "Azure-SQL", "Azure/Databases/02390-icon-service-Azure-SQL.svg"
            "Azure-SQL-VM", "Azure/Databases/10124-icon-service-Azure-SQL-VM.svg"
            "Azure-Synapse-Analytics", "Azure/Databases/00606-icon-service-Azure-Synapse-Analytics.svg"
            "Azure-Database-MySQL-Server", "Azure/Databases/10122-icon-service-Azure-Database-MySQL-Server.svg"
            "Data-Factory", "Azure/Databases/10126-icon-service-Data-Factory.svg"
            "SQL-Data-Warehouses", "Azure/Databases/00036-icon-service-SQL-Data-Warehouses.svg"
            "Instance-Pools", "Azure/Databases/10139-icon-service-Instance-Pools.svg"
            "Virtual-Clusters", "Azure/Databases/10127-icon-service-Virtual-Clusters.svg"
            "SQL-Managed-Instance", "Azure/Databases/10136-icon-service-SQL-Managed-Instance.svg"
            "SSIS-Lift-And-Shift-IR", "Azure/Databases/02392-icon-service-SSIS-Lift-And-Shift-IR.svg"
            "Azure-Cosmos-DB", "Azure/Databases/10121-icon-service-Azure-Cosmos-DB.svg"
            "SQL-Database", "Azure/Databases/10130-icon-service-SQL-Database.svg"
            "Azure-Database-PostgreSQL-Server", "Azure/Databases/10131-icon-service-Azure-Database-PostgreSQL-Server.svg"
            "SQL-Elastic-Pools", "Azure/Databases/10134-icon-service-SQL-Elastic-Pools.svg"
            "Azure-Database-Migration-Services", "Azure/Databases/10133-icon-service-Azure-Database-Migration-Services.svg"
            "Managed-Database", "Azure/Databases/10135-icon-service-Managed-Database.svg"
            "Azure-Data-Explorer-Clusters", "Azure/Databases/10145-icon-service-Azure-Data-Explorer-Clusters.svg"
            "SQL-Server", "Azure/Databases/10132-icon-service-SQL-Server.svg"
            "Cache-Redis", "Azure/Databases/10137-icon-service-Cache-Redis.svg"
            "Elastic-Job-Agents", "Azure/Databases/10128-icon-service-Elastic-Job-Agents.svg"
            "Azure-SQL-Server-Stretch-Databases", "Azure/Databases/10137-icon-service-Azure-SQL-Server-Stretch-Databases.svg"
            "Cost-Management-and-Billing", "Azure/Management + Governance/00004-icon-service-Cost-Management-and-Billing.svg"
            "Metrics", "Azure/Management + Governance/00020-icon-service-Metrics.svg"
            "Service-Providers", "Azure/Management + Governance/00025-icon-service-Service-Providers.svg"
            "Diagnostics-Settings", "Azure/Management + Governance/00008-icon-service-Diagnostics-Settings.svg"
            "Advisor", "Azure/Management + Governance/00003-icon-service-Advisor.svg"
            "Compliance", "Azure/Management + Governance/00011-icon-service-Compliance.svg"
            "Azure-Arc", "Azure/Management + Governance/00756-icon-service-Azure-Arc.svg"
            "Blueprints", "Azure/Management + Governance/00006-icon-service-Blueprints.svg"
            "Operation-Log-(Classic)", "Azure/Management + Governance/00024-icon-service-Operation-Log-(Classic).svg"
            "Monitor", "Azure/Management + Governance/00001-icon-service-Monitor.svg"
            "Education", "Azure/Management + Governance/00026-icon-service-Education.svg"
            "Activity-Log", "Azure/Management + Governance/00007-icon-service-Activity-Log.svg"
            "Managed-Applications-Center", "Azure/Management + Governance/10313-icon-service-Managed-Applications-Center.svg"
            "Application-Insights", "Azure/Management + Governance/00012-icon-service-Application-Insights.svg"
            "Azure-Lighthouse", "Azure/Management + Governance/00471-icon-service-Azure-Lighthouse.svg"
            "Solutions", "Azure/Management + Governance/00021-icon-service-Solutions.svg"
            "Log-Analytics-Workspaces", "Azure/Management + Governance/00009-icon-service-Log-Analytics-Workspaces.svg"
            "MachinesAzureArc", "Azure/Management + Governance/10450-icon-service-MachinesAzureArc.svg"
            "Policy", "Azure/Management + Governance/10316-icon-service-Policy.svg"
            "Alerts", "Azure/Management + Governance/00002-icon-service-Alerts.svg"
            "Automation-Accounts", "Azure/Management + Governance/00022-icon-service-Automation-Accounts.svg"
            "User-Privacy", "Azure/Management + Governance/10303-icon-service-User-Privacy.svg"
            "Resource-Graph-Explorer", "Azure/Management + Governance/10318-icon-service-Resource-Graph-Explorer.svg"
            "My-Customers", "Azure/Management + Governance/00014-icon-service-My-Customers.svg"
            "Recovery-Services-Vaults", "Azure/Management + Governance/00017-icon-service-Recovery-Services-Vaults.svg"
            "Route-Tables", "Azure/Networking/10082-icon-service-Route-Tables.svg"
            "Virtual-Networks-(Classic)", "Azure/Networking/10075-icon-service-Virtual-Networks-(Classic).svg"
            "Service-Endpoint-Policies", "Azure/Networking/10085-icon-service-Service-Endpoint-Policies.svg"
            "ExpressRoute-Circuits", "Azure/Networking/10079-icon-service-ExpressRoute-Circuits.svg"
            "Connections", "Azure/Networking/10081-icon-service-Connections.svg"
            "DDoS-Protection-Plans", "Azure/Networking/10072-icon-service-DDoS-Protection-Plans.svg"
            "Public-IP-Addresses-(Classic)", "Azure/Networking/10068-icon-service-Public-IP-Addresses-(Classic).svg"
            "Application-Gateways", "Azure/Networking/10076-icon-service-Application-Gateways.svg"
            "Virtual-WANs", "Azure/Networking/10353-icon-service-Virtual-WANs.svg"
            "Private-Link-Service", "Azure/Networking/01105-icon-service-Private-Link-Service.svg"
            "Private-Link", "Azure/Networking/00427-icon-service-Private-Link.svg"
            "Web-Application-Firewall-Policies(WAF)", "Azure/Networking/10362-icon-service-Web-Application-Firewall-Policies(WAF).svg"
            "Reserved-IP-Addresses-(Classic)", "Azure/Networking/10371-icon-service-Reserved-IP-Addresses-(Classic).svg"
            "Virtual-Network-Gateways", "Azure/Networking/10063-icon-service-Virtual-Network-Gateways.svg"
            "IP-Groups", "Azure/Networking/00701-icon-service-IP-Groups.svg"
            "Network-Watcher", "Azure/Networking/10066-icon-service-Network-Watcher.svg"
            "DNS-Zones", "Azure/Networking/10064-icon-service-DNS-Zones.svg"
            "Network-Interfaces", "Azure/Networking/10080-icon-service-Network-Interfaces.svg"
            "Front-Doors", "Azure/Networking/10073-icon-service-Front-Doors.svg"
            "Traffic-Manager-Profiles", "Azure/Networking/10065-icon-service-Traffic-Manager-Profiles.svg"
            "Public-IP-Addresses", "Azure/Networking/10069-icon-service-Public-IP-Addresses.svg"
            "Virtual-Networks", "Azure/Networking/10061-icon-service-Virtual-Networks.svg"
            "Proximity-Placement-Groups", "Azure/Networking/10365-icon-service-Proximity-Placement-Groups.svg"
            "Azure-Firewall-Manager", "Azure/Networking/00271-icon-service-Azure-Firewall-Manager.svg"
            "Network-Security-Groups", "Azure/Networking/10067-icon-service-Network-Security-Groups.svg"
            "Route-Filters", "Azure/Networking/10071-icon-service-Route-Filters.svg"
            "Firewalls", "Azure/Networking/10084-icon-service-Firewalls.svg"
            "NAT", "Azure/Networking/10310-icon-service-NAT.svg"
            "Load-Balancers", "Azure/Networking/10062-icon-service-Load-Balancers.svg"
            "CDN-Profiles", "Azure/Networking/00056-icon-service-CDN-Profiles.svg"
            "Local-Network-Gateways", "Azure/Networking/10077-icon-service-Local-Network-Gateways.svg"
            "Public-IP-Prefixes", "Azure/Networking/10372-icon-service-Public-IP-Prefixes.svg"
            "Storage-Container", "Azure/General/10839-icon-service-Storage-Container.svg"
            "Reservations", "Azure/General/10003-icon-service-Reservations.svg"
            "Web-Slots", "Azure/General/10849-icon-service-Web-Slots.svg"
            "Cost-Management-and-Billing", "Azure/General/00004-icon-service-Cost-Management-and-Billing.svg"
            "Dev-Console", "Azure/General/10796-icon-service-Dev-Console.svg"
            "Production-Ready-Database", "Azure/General/10829-icon-service-Production-Ready-Database.svg"
            "Branch", "Azure/General/10782-icon-service-Branch.svg"
            "Bug", "Azure/General/10784-icon-service-Bug.svg"
            "Input-Output", "Azure/General/10813-icon-service-Input-Output.svg"
            "Scheduler", "Azure/General/10833-icon-service-Scheduler.svg"
            "Guide", "Azure/General/10810-icon-service-Guide.svg"
            "Search", "Azure/General/10834-icon-service-Search.svg"
            "Browser", "Azure/General/10783-icon-service-Browser.svg"
            "Subscriptions", "Azure/General/10002-icon-service-Subscriptions.svg"
            "Quickstart-Center", "Azure/General/10010-icon-service-Quickstart-Center.svg"
            "Resource-Groups", "Azure/General/10007-icon-service-Resource-Groups.svg"
            "Tag", "Azure/General/10014-icon-service-Tag.svg"
            "Storage-Azure-Files", "Azure/General/10838-icon-service-Storage-Azure-Files.svg"
            "Mobile-Engagement", "Azure/General/10823-icon-service-Mobile-Engagement.svg"
            "Journey-Hub", "Azure/General/10814-icon-service-Journey-Hub.svg"
            "Error", "Azure/General/10798-icon-service-Error.svg"
            "Cost-Analysis", "Azure/General/10792-icon-service-Cost-Analysis.svg"
            "Cache", "Azure/General/10786-icon-service-Cache.svg"
            "Image", "Azure/General/10812-icon-service-Image.svg"
            "Free-Services", "Azure/General/10016-icon-service-Free-Services.svg"
            "Globe", "Azure/General/10806-icon-service-Globe.svg"
            "Tags", "Azure/General/10842-icon-service-Tags.svg"
            "Preview", "Azure/General/10827-icon-service-Preview.svg"
            "Media", "Azure/General/10854-icon-service-Media.svg"
            "Service-Health", "Azure/General/10004-icon-service-Service-Health.svg"
            "Globe-Error", "Azure/General/10807-icon-service-Globe-Error.svg"
            "Website-Staging", "Azure/General/10848-icon-service-Website-Staging.svg"
            "Controls", "Azure/General/10789-icon-service-Controls.svg"
            "Resource-Explorer", "Azure/General/10349-icon-service-Resource-Explorer.svg"
            "Heart", "Azure/General/10811-icon-service-Heart.svg"
            "Download", "Azure/General/10797-icon-service-Download.svg"
            "Workbooks", "Azure/General/10851-icon-service-Workbooks.svg"
            "Blob-Page", "Azure/General/10781-icon-service-Blob-Page.svg"
            "Help-and-Support", "Azure/General/10013-icon-service-Help-and-Support.svg"
            "Cost-Alerts", "Azure/General/10791-icon-service-Cost-Alerts.svg"
            "Module", "Azure/General/10855-icon-service-Module.svg"
            "Location", "Azure/General/10818-icon-service-Location.svg"
            "Folder-Blank", "Azure/General/10802-icon-service-Folder-Blank.svg"
            "Biz-Talk", "Azure/General/10779-icon-service-Biz-Talk.svg"
            "Launch-Portal", "Azure/General/10815-icon-service-Launch-Portal.svg"
            "Log-Streaming", "Azure/General/10819-icon-service-Log-Streaming.svg"
            "Globe-Success", "Azure/General/10808-icon-service-Globe-Success.svg"
            "Controls-Horizontal", "Azure/General/10790-icon-service-Controls-Horizontal.svg"
            "Resource-Linked", "Azure/General/10831-icon-service-Resource-Linked.svg"
            "Gear", "Azure/General/10805-icon-service-Gear.svg"
            "File", "Azure/General/10800-icon-service-File.svg"
            "Marketplace", "Azure/General/10008-icon-service-Marketplace.svg"
            "Web-Test", "Azure/General/10850-icon-service-Web-Test.svg"
            "Table", "Azure/General/10841-icon-service-Table.svg"
            "Website-Power", "Azure/General/10847-icon-service-Website-Power.svg"
            "Blob-Block", "Azure/General/10780-icon-service-Blob-Block.svg"
            "Process-Explorer", "Azure/General/10828-icon-service-Process-Explorer.svg"
            "Extensions", "Azure/General/10799-icon-service-Extensions.svg"
            "Versions", "Azure/General/10845-icon-service-Versions.svg"
            "Cubes", "Azure/General/10795-icon-service-Cubes.svg"
            "Workflow", "Azure/General/10852-icon-service-Workflow.svg"
            "Code", "Azure/General/10787-icon-service-Code.svg"
            "FTP", "Azure/General/10804-icon-service-FTP.svg"
            "Powershell", "Azure/General/10825-icon-service-Powershell.svg"
            "SSD", "Azure/General/10837-icon-service-SSD.svg"
            "All-Resources", "Azure/General/10001-icon-service-All-Resources.svg"
            "Power-Up", "Azure/General/10826-icon-service-Power-Up.svg"
            "Resource-Group-List", "Azure/General/10830-icon-service-Resource-Group-List.svg"
            "Counter", "Azure/General/10794-icon-service-Counter.svg"
            "Management-Groups", "Azure/General/10011-icon-service-Management-Groups.svg"
            "Commit", "Azure/General/10788-icon-service-Commit.svg"
            "Service-Bus", "Azure/General/10836-icon-service-Service-Bus.svg"
            "Learn", "Azure/General/10816-icon-service-Learn.svg"
            "Information", "Azure/General/10005-icon-service-Information.svg"
            "Scale", "Azure/General/10832-icon-service-Scale.svg"
            "Management-Portal", "Azure/General/10820-icon-service-Management-Portal.svg"
            "Files", "Azure/General/10801-icon-service-Files.svg"
            "Search-Grid", "Azure/General/10856-icon-service-Search-Grid.svg"
            "Mobile", "Azure/General/10822-icon-service-Mobile.svg"
            "Folder-Website", "Azure/General/10803-icon-service-Folder-Website.svg"
            "Cost-Budgets", "Azure/General/10793-icon-service-Cost-Budgets.svg"
            "Storage-Queue", "Azure/General/10840-icon-service-Storage-Queue.svg"
            "Media-File", "Azure/General/10821-icon-service-Media-File.svg"
            "Toolbox", "Azure/General/10844-icon-service-Toolbox.svg"
            "TFS-VC-Repository", "Azure/General/10843-icon-service-TFS-VC-Repository.svg"
            "Globe-Warning", "Azure/General/10809-icon-service-Globe-Warning.svg"
            "Dashboard", "Azure/General/10015-icon-service-Dashboard.svg"
            "Cost-Management", "Azure/General/10019-icon-service-Cost-Management.svg"
            "Load-Test", "Azure/General/10817-icon-service-Load-Test.svg"
            "Server-Farm", "Azure/General/10835-icon-service-Server-Farm.svg"
            "Recent", "Azure/General/10006-icon-service-Recent.svg"
            "Backlog", "Azure/General/10853-icon-service-Backlog.svg"
            "Builds", "Azure/General/10785-icon-service-Builds.svg"
            "Power", "Azure/General/10824-icon-service-Power.svg"
            "RTOS", "Azure/Preview/10778-icon-service-RTOS.svg"
            "Azure-Workbooks", "Azure/Preview/02189-icon-service-Azure-Workbooks.svg"
            "Web-Environment", "Azure/Preview/10846-icon-service-Web-Environment.svg"
            "Azure-Sphere", "Azure/Preview/10190-icon-service-Azure-Sphere.svg"
            "Private-Link-Hub", "Azure/Preview/02209-icon-service-Private-Link-Hub.svg"
            "IoT-Edge", "Azure/Preview/10186-icon-service-IoT-Edge.svg"
            "Static-Apps", "Azure/Preview/01007-icon-service-Static-Apps.svg"
            "Time-Series-Data-Sets", "Azure/Preview/10198-icon-service-Time-Series-Data-Sets.svg"
            "Azure-Cloud-Shell", "Azure/Preview/00559-icon-service-Azure-Cloud-Shell.svg"
            "Data-Lake-Storage-Gen1", "Azure/Storage/10090-icon-service-Data-Lake-Storage-Gen1.svg"
            "Storage-Accounts-(Classic)", "Azure/Storage/10087-icon-service-Storage-Accounts-(Classic).svg"
            "Data-Box", "Azure/Storage/10094-icon-service-Data-Box.svg"
            "Data-Shares", "Azure/Storage/10098-icon-service-Data-Shares.svg"
            "Azure-Stack-Edge", "Azure/Storage/00691-icon-service-Azure-Stack-Edge.svg"
            "Azure-HCP-Cache", "Azure/Storage/00776-icon-service-Azure-HCP-Cache.svg"
            "Storage-Accounts", "Azure/Storage/10086-icon-service-Storage-Accounts.svg"
            "Azure-NetApp-Files", "Azure/Storage/10096-icon-service-Azure-NetApp-Files.svg"
            "Data-Share-Invitations", "Azure/Storage/10097-icon-service-Data-Share-Invitations.svg"
            "StorSimple-Data-Managers", "Azure/Storage/10092-icon-service-StorSimple-Data-Managers.svg"
            "Import-Export-Jobs", "Azure/Storage/10100-icon-service-Import-Export-Jobs.svg"
            "Storage-Sync-Services", "Azure/Storage/10093-icon-service-Storage-Sync-Services.svg"
            "Data-Box-Edge", "Azure/Storage/10095-icon-service-Data-Box-Edge.svg"
            "StorSimple-Device-Managers", "Azure/Storage/10089-icon-service-StorSimple-Device-Managers.svg"
            "Recovery-Services-Vaults", "Azure/Storage/00017-icon-service-Recovery-Services-Vaults.svg"
            "Azure-Media-Service", "Azure/Web/10309-icon-service-Azure-Media-Service.svg"
            "Notification-Hub-Namespaces", "Azure/Web/10053-icon-service-Notification-Hub-Namespaces.svg"
            "Service-Fabric-Clusters", "Azure/Containers/10036-icon-service-Service-Fabric-Clusters.svg"
            "Batch-Accounts", "Azure/Containers/10031-icon-service-Batch-Accounts.svg"
            "Container-Instances", "Azure/Containers/10104-icon-service-Container-Instances.svg"
            "Container-Registries", "Azure/Containers/10105-icon-service-Container-Registries.svg"
            "Kubernetes-Services", "Azure/Containers/10023-icon-service-Kubernetes-Services.svg"
            "App-Services", "Azure/Containers/10035-icon-service-App-Services.svg"
            "SAP-Azure-Monitor", "Azure/Monitor/00438-icon-service-SAP-Azure-Monitor.svg"
            "Universal-Print", "Azure/Other/00571-icon-service-Universal-Print.svg"
            "Windows-Virtual-Desktop", "Azure/Other/00327-icon-service-Windows-Virtual-Desktop.svg"
            "ExpressRoute-Direct", "Azure/Other/00903-icon-service-ExpressRoute-Direct.svg"
            "Resource-Mover", "Azure/Other/02200-icon-service-Resource-Mover.svg"
            "Instance-Pools", "Azure/Other/10139-icon-service-Instance-Pools.svg"
            "SSH-Keys", "Azure/Other/00412-icon-service-SSH-Keys.svg"
            "Peering-Service", "Azure/Other/00970-icon-service-Peering-Service.svg"
            "Template-Specs", "Azure/Other/02340-icon-service-Template-Specs.svg"
            "Internet-Analyzer-Profiles", "Azure/Other/00469-icon-service-Internet-Analyzer-Profiles.svg"
            "Detonation", "Azure/Other/00378-icon-service-Detonation.svg"
            "Azure-Backup-Center", "Azure/Other/02360-icon-service-Azure-Backup-Center.svg"
            "Local-Network-Gateways", "Azure/Other/10077-icon-service-Local-Network-Gateways.svg"
            "Stream-Analytics-Jobs", "Azure/IoT/00042-icon-service-Stream-Analytics-Jobs.svg"
            "Device-Provisioning-Services", "Azure/IoT/10369-icon-service-Device-Provisioning-Services.svg"
            "IoT-Central-Applications", "Azure/IoT/10184-icon-service-IoT-Central-Applications.svg"
            "Azure-Maps-Accounts", "Azure/IoT/10185-icon-service-Azure-Maps-Accounts.svg"
            "Notification-Hubs", "Azure/IoT/10045-icon-service-Notification-Hubs.svg"
            "Logic-Apps", "Azure/IoT/10201-icon-service-Logic-Apps.svg"
            "Time-Series-Insights-Event-Sources", "Azure/IoT/10188-icon-service-Time-Series-Insights-Event-Sources.svg"
            "Function-Apps", "Azure/IoT/10029-icon-service-Function-Apps.svg"
            "Event-Hubs", "Azure/IoT/00039-icon-service-Event-Hubs.svg"
            "Time-Series-Insights-Environments", "Azure/IoT/10181-icon-service-Time-Series-Insights-Environments.svg"
            "IoT-Hub", "Azure/IoT/10182-icon-service-IoT-Hub.svg"
            "Search-Services", "Azure/App Services/10044-icon-service-Search-Services.svg"
            "App-Service-Environments", "Azure/App Services/10047-icon-service-App-Service-Environments.svg"
            "Notification-Hubs", "Azure/App Services/10045-icon-service-Notification-Hubs.svg"
            "App-Service-Certificates", "Azure/App Services/00049-icon-service-App-Service-Certificates.svg"
            "API-Management-Services", "Azure/App Services/10042-icon-service-API-Management-Services.svg"
            "App-Service-Domains", "Azure/App Services/00050-icon-service-App-Service-Domains.svg"
            "App-Services", "Azure/App Services/10035-icon-service-App-Services.svg"
            "App-Service-Plans", "Azure/App Services/00046-icon-service-App-Service-Plans.svg"
            "CDN-Profiles", "Azure/App Services/00056-icon-service-CDN-Profiles.svg"
            "AVS", "Azure/Azure VMware Solution/00524-icon-service-AVS.svg"
            "Remote-Rendering", "Azure/Mixed Reality/00698-icon-service-Remote-Rendering.svg"
            "Key-Vaults", "Azure/Security/10245-icon-service-Key-Vaults.svg"
            "Application-Security-Groups", "Azure/Security/10244-icon-service-Application-Security-Groups.svg"
            "Security-Center", "Azure/Security/10241-icon-service-Security-Center.svg"
            "Azure-Defender", "Azure/Security/02247-icon-service-Azure-Defender.svg"
            "Conditional-Access", "Azure/Security/10233-icon-service-Conditional-Access.svg"
            "ExtendedSecurityUpdates", "Azure/Security/10572-icon-service-ExtendedSecurityUpdates.svg"
            "Azure-Sentinel", "Azure/Security/10248-icon-service-Azure-Sentinel.svg"
            "Machine-Learning-Studio-Workspaces", "Azure/AI + Machine Learning/10167-icon-service-Machine-Learning-Studio-Workspaces.svg"
            "Machine-Learning-Studio-(Classic)-Web-Services", "Azure/AI + Machine Learning/00030-icon-service-Machine-Learning-Studio-(Classic)-Web-Services.svg"
            "Bot-Services", "Azure/AI + Machine Learning/10165-icon-service-Bot-Services.svg"
            "Machine-Learning-Studio-Web-Service-Plans", "Azure/AI + Machine Learning/10168-icon-service-Machine-Learning-Studio-Web-Service-Plans.svg"
            "Translator-Text", "Azure/AI + Machine Learning/00800-icon-service-Translator-Text.svg"
            "Cognitive-Services", "Azure/AI + Machine Learning/10162-icon-service-Cognitive-Services.svg"
            "DevTest-Labs", "Azure/DevOps/10264-icon-service-DevTest-Labs.svg"
            "Application-Insights", "Azure/DevOps/00012-icon-service-Application-Insights.svg"
            "Lab-Services", "Azure/DevOps/10265-icon-service-Lab-Services.svg"
            "Azure-DevOps", "Azure/DevOps/10261-icon-service-Azure-DevOps.svg"
            "Time-Series-Insights-Access-Policies", "Azure/Internet of things/10192-icon-service-Time-Series-Insights-Access-Policies.svg"
            "Digital-Twins", "Azure/Internet of Things/01030-icon-service-Digital-Twins.svg"
            "Cost-Management-and-Billing", "Azure/Migrate/00004-icon-service-Cost-Management-and-Billing.svg"
            "Data-Box", "Azure/Migrate/10094-icon-service-Data-Box.svg"
            "Azure-Migrate", "Azure/Migrate/10281-icon-service-Azure-Migrate.svg"
            "Data-Box-Edge", "Azure/Migrate/10095-icon-service-Data-Box-Edge.svg"
            "Recovery-Services-Vaults", "Azure/Migrate/00017-icon-service-Recovery-Services-Vaults.svg"
            "Azure-AD-B2C", "Azure/Identity/10228-icon-service-Azure-AD-B2C.svg"
            "App-Registrations", "Azure/Identity/10232-icon-service-App-Registrations.svg"
            "Azure-Active-Directory", "Azure/Identity/10221-icon-service-Azure-Active-Directory.svg"
            "Azure-AD-Domain-Services", "Azure/Identity/10222-icon-service-Azure-AD-Domain-Services.svg"
            "Active-Directory-Connect-Health", "Azure/Identity/10224-icon-service-Active-Directory-Connect-Health.svg"
            "Groups", "Azure/Identity/10223-icon-service-Groups.svg"
            "Identity-Governance", "Azure/Identity/10235-icon-service-Identity-Governance.svg"
            "Azure-AD-Identity-Protection", "Azure/Identity/10231-icon-service-Azure-AD-Identity-Protection.svg"
            "Managed-Identities", "Azure/Identity/10227-icon-service-Managed-Identities.svg"
            "Enterprise-Applications", "Azure/Identity/10225-icon-service-Enterprise-Applications.svg"
            "Users", "Azure/Identity/10230-icon-service-Users.svg"
            "VM-Scale-Sets", "Azure/Compute/10034-icon-service-VM-Scale-Sets.svg"
            "VM-Images-(Classic)", "Azure/Compute/10040-icon-service-VM-Images-(Classic).svg"
            "Images", "Azure/Compute/10033-icon-service-Images.svg"
            "Service-Fabric-Clusters", "Azure/Compute/10036-icon-service-Service-Fabric-Clusters.svg"
            "Disks", "Azure/Compute/10032-icon-service-Disks.svg"
            "Disk-Encryption-Sets", "Azure/Compute/00398-icon-service-Disk-Encryption-Sets.svg"
            "Virtual-Machines-(Classic)", "Azure/Compute/10028-icon-service-Virtual-Machines-(Classic).svg"
            "Image-Definitions", "Azure/Compute/10037-icon-service-Image-Definitions.svg"
            "Image-Versions", "Azure/Compute/10038-icon-service-Image-Versions.svg"
            "Availability-Sets", "Azure/Compute/10025-icon-service-Availability-Sets.svg"
            "Mesh-Applications", "Azure/Compute/10024-icon-service-Mesh-Applications.svg"
            "OS-Images-(Classic)", "Azure/Compute/10027-icon-service-OS-Images-(Classic).svg"
            "Cloud-Services-(Classic)", "Azure/Compute/10030-icon-service-Cloud-Services-(Classic).svg"
            "Function-Apps", "Azure/Compute/10029-icon-service-Function-Apps.svg"
            "Workspaces", "Azure/Compute/00400-icon-service-Workspaces.svg"
            "Automanaged-VM", "Azure/Compute/02112-icon-service-Automanaged-VM.svg"
            "Batch-Accounts", "Azure/Compute/10031-icon-service-Batch-Accounts.svg"
            "Azure-Spring-Cloud", "Azure/Compute/10370-icon-service-Azure-Spring-Cloud.svg"
            "Container-Instances", "Azure/Compute/10104-icon-service-Container-Instances.svg"
            "Virtual-Machine", "Azure/Compute/10021-icon-service-Virtual-Machine.svg"
            "Disks-(Classic)", "Azure/Compute/10041-icon-service-Disks-(Classic).svg"
            "Kubernetes-Services", "Azure/Compute/10023-icon-service-Kubernetes-Services.svg"
            "Container-Services-(Deprecated)", "Azure/Compute/10049-icon-service-Container-Services-(Deprecated).svg"
            "App-Services", "Azure/Compute/10035-icon-service-App-Services.svg"
            "Disks-Snapshots", "Azure/Compute/10026-icon-service-Disks-Snapshots.svg"
            "Shared-Image-Galleries", "Azure/Compute/10039-icon-service-Shared-Image-Galleries.svg"
        ]
        |> List.map (fun (k, v) -> (k.Replace("es ", "")
                                     .Replace("s ", "")
                                     .Replace(" ", "")
                                     .ToLowerInvariant()
                                     .Replace("azure", "")
                                     .Replace("-", "")
                                     .Replace("sqldatabase", "sql")
                                     .TrimEnd('s'), v))
        |> Map.ofList