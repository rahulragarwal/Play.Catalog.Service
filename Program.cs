using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IItemsRepository, ItemsRepository>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(MongoDbSettings.SettingName));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection(ServiceSettings.SettingName));


BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
