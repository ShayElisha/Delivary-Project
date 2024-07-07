# Delivery-Project

## Overview
This project is designed for the creation, organization, and management of deliveries. It includes features for updating and tracking shipments from the collection point to the delivery point, as well as management tools for tracking drivers and deliveries. Shipments can be automatically assigned by date and according to each driver's carrying capacity.

## Features
- Create and manage deliveries.
- Track shipments from collection to delivery.
- Manage drivers and monitor their deliveries.
- Automatically assign shipments by date and driver capacity.

## Configuration

To enable all features, you need to configure the `WEB.CONFIG` file with the following settings:

```xml
<appSettings>
  <add key="SecretKey" value="GOOGLE_MAPS_API_KEY" />
</appSettings>
<connectionStrings>
  <add name="connString" connectionString="connectionString of your database" />
</connectionStrings>
