﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Products.Models;

namespace Products.Repositories
{
    internal class ProductRepository
    {
        #region Data Members

        private readonly DocumentClient _documentClient;
        private readonly DocumentDbSettings _documentDbSettings;

        #endregion

        #region Constructors

        internal ProductRepository(DocumentDbSettings settings)
        {
            // Save the document db settings into a private data member and 
            // instantiate an instance of the client.
            _documentDbSettings = settings;
            _documentClient = new DocumentClient(new Uri(_documentDbSettings.Endpoint),
                _documentDbSettings.AuthKey);
        }

        public ProductRepository() : this(DocumentDbSettings.GetDbSettings())
        {
        }

        #endregion

        #region Public Methods

        public async Task Initialize()
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateCollectionIfNotExistsAsync();

            var product1 = new Product() { Id = "1", Count = 5, Name = "Ninja Stars", Price = 5.99};
            CreateProductDocumentIfNotExists(product1).Wait();

            var product2 = new Product() { Id = "2", Count = 12, Name = "Sword", Price = 199.99 };
            CreateProductDocumentIfNotExists(product2).Wait();

            var product3 = new Product() { Id = "3", Count = 12, Name = "Nunchucks", Price = 24.79 };
            CreateProductDocumentIfNotExists(product3).Wait();
        }

        #endregion

        #region Private Methods

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                var databaseUri = UriFactory.CreateDatabaseUri(_documentDbSettings.DatabaseId);
                await _documentClient.ReadDatabaseAsync(databaseUri);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var database = new Database { Id = _documentDbSettings.DatabaseId };
                    await _documentClient.CreateDatabaseAsync(database);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);
                await _documentClient.ReadDocumentCollectionAsync(collectionUri);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var databaseUri = UriFactory.CreateDatabaseUri(_documentDbSettings.DatabaseId);
                    await _documentClient.CreateDocumentCollectionAsync(databaseUri,
                        new DocumentCollection { Id = _documentDbSettings.CollectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateProductDocumentIfNotExists(Product product)
        {
            try
            {
                var documentUri = UriFactory.CreateDocumentUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId, product.Id);
                await _documentClient.ReadDocumentAsync(documentUri);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var documentCollectionUri = UriFactory.CreateDocumentCollectionUri(_documentDbSettings.DatabaseId, _documentDbSettings.CollectionId);
                    await _documentClient.CreateDocumentAsync(documentCollectionUri, product);
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion
    }
}
