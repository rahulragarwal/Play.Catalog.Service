using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Repository
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, MongoDbSettings mongoDbSettings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            services.AddSingleton(ServiceProvider =>
            {
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(ServiceSettings.SettingName);
            });
            return services;

        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string CollectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>, MongoRepository<T>>(
                ServiceProvider =>
                {
                    var database = ServiceProvider.GetService<IMongoDatabase>();
                    return new MongoRepository<T>(CollectionName, database);
                }
            );
            return services;
        }
    }
}
