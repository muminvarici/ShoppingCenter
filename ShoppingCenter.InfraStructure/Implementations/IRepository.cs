using MongoDB.Bson;
using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ShoppingCenter.InfraStructure.Implementations
{
	public interface IRepository<TDocument> where TDocument : IDocument
	{
		IQueryable<TDocument> AsQueryable();
		bool Contains(Expression<Func<TDocument, bool>> filterExpression);
		Task<bool> ContainsAsync(Expression<Func<TDocument, bool>> filterExpression);
		IEnumerable<TDocument> FilterBy(
			Expression<Func<TDocument, bool>> filterExpression);

		IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression,
			Expression<Func<TDocument, TProjected>> projectionExpression);

		Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression);

		TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

		Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

		TDocument FindById(string id);

		Task<TDocument> FindByIdAsync(string id);

		void InsertOne(TDocument document, bool newId = true);

		Task InsertOneAsync(TDocument document, bool newId = true);

		void InsertMany(ICollection<TDocument> documents);

		Task InsertManyAsync(ICollection<TDocument> documents);

		void ReplaceOne(TDocument document);

		Task ReplaceOneAsync(TDocument document);

		void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

		Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

		void DeleteById(string id);

		Task DeleteByIdAsync(string id);

		void DeleteById(BsonObjectId id);

		Task DeleteByIdAsync(BsonObjectId id);

		void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

		Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

		IEnumerable<TDocument> GetAll();

		Task<IEnumerable<TDocument>> GetAllAsync();

		IEnumerable<TDocument> GetAllWithPaging(int pageSize = 20, int pageNumber = 1, bool checkDelete = true);

		long Count();
		Task<long> CountAsync();
	}
}
