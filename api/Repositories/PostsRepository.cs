using System.Data;
using api.Dtos;
using api.Models;
using Dapper;

namespace api.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDbConnection _connection;

        public PostsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Post> AddPost(AddPostDto addPostDto)
        {
            var sql = "INSERT INTO posts (title, body) VALUES (@title, @body); SELECT last_insert_rowid()";

            var parameters = new DynamicParameters();
            parameters.Add("title", addPostDto.Title, DbType.String);
            parameters.Add("body", addPostDto.Body, DbType.String);

            var createdPostId = await _connection.QuerySingleAsync<int>(sql, parameters);

            return new Post
            {
                Title = addPostDto.Title,
                Body = addPostDto.Body,
                Id = createdPostId,
            };
        }

        public async Task DeletePost(int id)
        {
            var sql = "DELETE FROM posts WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var sql = "SELECT * FROM posts";
            var posts = await _connection.QueryAsync<Post>(sql);
            return posts;
        }

        public async Task<Post?> GetPost(int id)
        {
            var sql = "SELECT * FROM posts WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            var post = await _connection.QuerySingleOrDefaultAsync<Post>(sql, parameters);
            return post;
        }
    }
}