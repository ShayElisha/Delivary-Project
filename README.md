# Delivary-Project
 
We have a project here that deals with the creation, organization and management of deliveries, which also includes updating and tracking the exporters from the collection point to the delivery point, including the manager's tracking of the drivers and deliveries.
It is possible to export shipments automatically by date and according to the amount that each driver can carry.
In order for all the features to work, you need WEB.CONFIG
with these features and you have to enter your information
<configuration>
  <appSettings>
  <add key="SecretKey" value="GOOGLE_MAPS_API_KEY" />
  </appSettings>
  <connectionStrings>
  <add name="connString" connectionString="connectionString of your database" />
  </connectionStrings>
</configuration>
