# API Management Demo Setup

Setup:  Create Azure Storage Account, CosmosDB, Azure Search

<b>You must go through API Mangagement Setup at least once before the following demo will work.  API Mangement can take up to an hour to create.</b>

## Use template to create API Managment, Logic App, Azure Function and Web Service

1. Click Deploy to Azure button below.

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fcodingwithsasquatch%2Fintegration-demos%2Fmaster%2FAPI_Management_Demo%2Fdeploy.json)

1. Fill in the values of the template, giving the resource group a name and location.  Be sure to enter in your email address for the API Management.

    ![Build Template](images/template_settings.png "Build Template")

1. Select the terms and agreement check box and select purchase.

    ![Build Template](images/template_purchase.png "Build Template")

1. When the deployment finishes, we need to add the CosmosDB account key to the web services. Click on the "go to resource" button of the deployment notification

    ![Build Template](images/template_goto_resource.png "Build Template")

## Add CosmosDB Key to WCF Service

1. In the Resource Group, select the Azure CosmosDB Account.

    ![Cosmos DB Account](images/select_cosmos_db_account.png "Cosmos DB Account")

1. Select the Keys option from the left hand side.

    ![Cosmos DB Keys](images/select_cosmosdb_keys.png "Cosmos DB Keys")

1. In the middle of the screen click the copy button of the Primary Key.  Save this value.

    ![CosmosDB Primary Key](images/copy_cosmosdb_primary_key.png "CosmosDB Primary Key")

1. Go to the Resource Group (select the Resource Group breadcrumb from the top of the screen)

    ![Go to Resource Group](images/resource_group_breadcrumb.png "Go to Resource Group")

1. Select the App Service website that holds the WCF Service

    ![Select WCF Service](images/select_wcf_service.png "Select WCF Service")

1. From the left hand side, select Application Settings

    ![WCF Application Settings](images/wcf_service_app_settings.png "WCF Application Settings")

1. In middle screen, scroll down to the Application Settings section

    ![WCF Application Settings](images/wcf_app_settings.png "WCF Application Settings")

1. Select the Show Values button.

    ![Show Values](images/wcf_show_values.png "Show Values")

1. Select the AuthorizationKey value text box, and paste the CosmosDB Key into the box.

    ![Authorization Key](images/wcf_key_value.png "Authorization Key")

1. Press save.

    ![Save](images/wcf_save.png "Save")

## Add CosmosDB Key to Azure Function

1. Go to the Resource Group (select the Resource Group breadcrumb from the top of the screen)

    ![Go to Resource Group](images/resource_group_breadcrumb.png "Go to Resource Group")

1. Select the Azure Function

    ![Select Azure Function](images/select_azure_function.png "Select Azure Function")

1. In the middle of the screen, under Configured Features, select Application settings

    ![Function Application Settings](images/function_app_settings.png "Function Application Settings")

1. In middle screen, scroll down to the Application Settings section

    ![Function Application Settings](images/function_app_setting_value.png "WCF Application Settings")

1. Select the Show Values button.

    ![Show Values](images/wcf_show_values.png "Show Values")

1. Select the DocumentDbAuthKey value text box, and paste the CosmosDB Key into the box.

    ![Authorization Key](images/function_key_value.png "Authorization Key")

1. Press save.

    ![Save](images/function_setting_save.png "Save")

## Add CosmosDB Key to Logic App API Connection

1. Go to the Resource Group (select the Resource Group breadcrumb from the top of the screen)

    ![Go to Resource Group](images/resource_group_breadcrumb.png "Go to Resource Group")

1. Select the API Connection

    ![Select Azure Function](images/api_connection.png "Select Azure Function")

1. On the left hand side, Select Edit API Connection

    ![Edit API Connection](images/api_connection_settings.png "Edit API Connection")

1. Paste the CosmosDB Key into the 'Access Key to your Azure CosmosDB account' textbox.

    ![Authorization Key](images/api_connection_key_value.png "Authorization Key")

1. Press save.

    ![Save](images/api_connection_save.png "Save")