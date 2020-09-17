using Core.DependencyInjection.Attributes;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ShoppingCenter.InfraStructure
{
	public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
	{
		private readonly IMongoCollection<TDocument> _collection;
		public MongoRepository(IOptions<DbSettings> settings)
		{
			if (settings?.Value == null || string.IsNullOrWhiteSpace(settings.Value.ConnectionString) || string.IsNullOrWhiteSpace(settings.Value.DatabaseName))
			{
				throw new ArgumentNullException(nameof(settings));
			}
			var database = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName);
			_collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
		}

		private string GetCollectionName(Type documentType)
		{
			var preferedName = ((BsonCollectionAttribute)documentType.GetCustomAttributes(
					typeof(BsonCollectionAttribute),
					true)
				.FirstOrDefault())?.CollectionName;
			if (string.IsNullOrWhiteSpace(preferedName))
			{
				return documentType.Name;
			}
			return preferedName;
		}

		public virtual bool Contains(
			Expression<Func<TDocument, bool>> filterExpression)
		{
			return _collection.Find(filterExpression).Any();
		}

		public virtual async Task<bool> ContainsAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return await (await _collection.FindAsync(filterExpression)).AnyAsync();
		}

		public virtual IEnumerable<TDocument> GeoLocation(string filed, double lng, double lat)
		{
			var point = new GeoJson2DGeographicCoordinates(lng, lat);
			var pnt = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(point);
			var builder = Builders<TDocument>.Filter;
			var filter = builder.GeoIntersects(filed, pnt);

			return _collection.Find(filter).ToEnumerable();
		}

		public virtual IQueryable<TDocument> AsQueryable()
		{
			return _collection.AsQueryable();
		}

		public virtual IEnumerable<TDocument> FilterBy(
			Expression<Func<TDocument, bool>> filterExpression)
		{
			return _collection.Find(filterExpression).ToEnumerable();
		}

		public virtual IEnumerable<TProjected> FilterBy<TProjected>(
			Expression<Func<TDocument, bool>> filterExpression,
			Expression<Func<TDocument, TProjected>> projectionExpression)
		{
			return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
		}


		public virtual async Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return (await _collection.FindAsync(filterExpression)).ToEnumerable();
		}

		public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
		{
			return _collection.Find(filterExpression).FirstOrDefault();
		}

		public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
		}

		public virtual TDocument FindById(string id)
		{
			var objectId = new ObjectId(id);
			var filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
			return _collection.Find(filter).SingleOrDefault();
		}

		public virtual async Task<TDocument> FindByIdAsync(string id)
		{
			var objectId = new ObjectId(id);
			var filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
			return await (await _collection.FindAsync(filter)).SingleOrDefaultAsync();
		}


		public virtual void InsertOne(TDocument document)
		{
			document.CreatedAt = DateTime.Now;
			_collection.InsertOne(document);
		}

		public virtual Task InsertOneAsync(TDocument document)
		{
			document.CreatedAt = DateTime.Now;
			return Task.Run(() => _collection.InsertOneAsync(document));
		}

		public void InsertMany(ICollection<TDocument> documents)
		{
			_collection.InsertMany(documents.Select(document =>
			{
				document.CreatedAt = DateTime.Now;
				return document;
			}));
		}

		public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
		{
			await _collection.InsertManyAsync(documents.Select(document =>
			{
				document.CreatedAt = DateTime.Now;
				return document;
			}));
		}

		public void ReplaceOne(TDocument document)
		{
			document.UpdatedAt = DateTime.Now;
			var filter = Builders<TDocument>.Filter.Eq(doc => doc._id, document._id);
			_collection.FindOneAndReplace(filter, document);
		}

		public virtual async Task ReplaceOneAsync(TDocument document)
		{
			document.UpdatedAt = DateTime.Now;
			var filter = Builders<TDocument>.Filter.Eq(doc => doc._id, document._id);
			await _collection.FindOneAndReplaceAsync(filter, document);
		}

		public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
		{
			_collection.FindOneAndDelete(filterExpression);
		}

		public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
		}

		public void DeleteById(string id)
		{
			var objectId = new ObjectId(id);
			DeleteById(objectId);
		}

		public async Task DeleteByIdAsync(string id)
		{
			var objectId = new ObjectId(id);
			await DeleteByIdAsync(objectId);
		}


		public void DeleteById(BsonObjectId objectId)
		{
			var filter = Builders<TDocument>.Filter.Eq(doc => doc._id, objectId);
			_collection.FindOneAndDelete(filter);
		}

		public Task DeleteByIdAsync(BsonObjectId objectId)
		{
			return Task.Run(() =>
			{
				DeleteById(objectId);
			});
		}

		public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
		{
			_collection.DeleteMany(filterExpression);
		}

		public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
		}

		public IEnumerable<TDocument> GetAll()
		{
			//Builders<TDocument>.Filter.Empty
			return _collection
				.AsQueryable()
				.ToList();
		}

		public async Task<IEnumerable<TDocument>> GetAllAsync()
		{
			return await (await _collection.FindAsync(Builders<TDocument>.Filter.Empty)).ToListAsync();
		}

		public IEnumerable<TDocument> GetAllWithPaging(int pageSize = 20, int pageNumber = 1, bool checkDelete = true)
		{
			return _collection
				.AsQueryable()
				.OrderByDescending(o => o.CreatedAt)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}
}
