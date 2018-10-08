# API Management Demos

Ninja Store is a retail shop for all things ninja.  The developers at Ninja Store have built three API services that they would like to expose to their customers' developers.  

The first API service is an API that creates, updates, deletes and retrieves informaiton about a product.  The API is buils using a) n Azure Function (https://azure.microsoft.com/en-us/services/functions/) that uses an OpenAPI definition (https://swagger.io/specification/) to document that API.

The second API service is an API that submits an order to the Ninja Store's backend system.  The API uses a Logic App (https://azure.microsoft.com/en-us/services/logic-apps/) to save the order and then check the inventory level of the product ordered.

The third API uses a WCF service that searches and returns the orders from the Ninja Store database.  The WCF SOAP call will use the SOAP to REST functionality (https://docs.microsoft.com/en-us/azure/api-management/restify-soap-api) within API Management.

