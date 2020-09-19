using Core.DependencyInjection.Attributes;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCenter.InfraStructure.Models;
using ShoppingCenter.InfraStructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ShoppingCenter.InfraStructure.Implementations
{
	public class InMemoryRepository<TDocument> : IRepository<TDocument> where TDocument : IDocument
	{
		private IList<TDocument> entities = new List<TDocument>();

		public InMemoryRepository()
		{
		}

		public virtual bool Contains(Expression<Func<TDocument, bool>> filterExpression)
		{
			return AsQueryable().Where(filterExpression).Any();
		}

		public virtual long Count()
		{
			return entities.Count();
		}

		public virtual Task<long> CountAsync()
		{
			return Task.FromResult(Count());
		}

		public virtual Task<bool> ContainsAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.FromResult(AsQueryable().FirstOrDefault(filterExpression) != null);
		}


		public virtual IQueryable<TDocument> AsQueryable()
		{
			return entities.AsQueryable();
		}

		public virtual IEnumerable<TDocument> FilterBy(
			Expression<Func<TDocument, bool>> filterExpression)
		{
			return AsQueryable().Where(filterExpression);
		}

		public virtual IEnumerable<TProjected> FilterBy<TProjected>(
			Expression<Func<TDocument, bool>> filterExpression,
			Expression<Func<TDocument, TProjected>> projectionExpression)
		{
			throw new NotImplementedException();
		}


		public virtual Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			var result = AsQueryable().Where(filterExpression).ToList().AsEnumerable();
			return Task.FromResult(result);
		}

		public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
		{
			return AsQueryable().Where(filterExpression).FirstOrDefault();
		}

		public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.FromResult(FindOne(filterExpression));
		}

		public virtual TDocument FindById(string id)
		{
			return AsQueryable().Where(w => w.Id.ToString() == id).SingleOrDefault();
		}

		public virtual async Task<TDocument> FindByIdAsync(string id)
		{
			return await Task.FromResult(FindById(id));
		}


		public virtual void InsertOne(TDocument document, bool newId = true)
		{
			document.CreatedAt = DateTime.Now;
			if (newId || document.Id.Equals(ObjectId.Empty))
			{
				document.Id = ObjectId.GenerateNewId();
			}
			entities.Add(document);
		}

		public virtual Task InsertOneAsync(TDocument document, bool newId = true)
		{
			return Task.Run(() => InsertOne(document, newId));
		}

		public void InsertMany(ICollection<TDocument> documents)
		{
			foreach (var item in documents)
			{
				InsertOne(item);
			}
		}

		public virtual Task InsertManyAsync(ICollection<TDocument> documents)
		{
			return Task.Run(() => InsertMany(documents));
		}

		public void ReplaceOne(TDocument document)
		{
			document.UpdatedAt = DateTime.Now;
			var item = entities.FirstOrDefault(w => w.Id == document.Id);
			if (item.Id != null)
			{
				entities[entities.IndexOf(item)] = document;
			}
		}

		public virtual Task ReplaceOneAsync(TDocument document)
		{
			return Task.Run(() => ReplaceOne(document));
		}

		public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
		{
			var item = AsQueryable().FirstOrDefault(filterExpression);
			if (item != null)
			{
				entities.Remove(item);
			}
		}

		public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.Run(() => DeleteOne(filterExpression));
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
			entities.Remove(entities.FirstOrDefault(w => w.Id == objectId));
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
			var deleted = AsQueryable().Where(filterExpression)?.Select(w => w.Id).ToList();
			entities = entities.Where(w => !deleted.Contains(w.Id)).ToList();
		}

		public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
		{
			return Task.Run(() => DeleteMany(filterExpression));
		}

		public IEnumerable<TDocument> GetAll()
		{
			//Builders<TDocument>.Filter.Empty
			return AsQueryable()
				.ToList();
		}

		public Task<IEnumerable<TDocument>> GetAllAsync()
		{
			return Task.Run(() => GetAll());
		}

		public IEnumerable<TDocument> GetAllWithPaging(int pageSize = 20, int pageNumber = 1, bool checkDelete = true)
		{
			return AsQueryable()
				.OrderByDescending(o => o.CreatedAt)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
		}
	}
}
