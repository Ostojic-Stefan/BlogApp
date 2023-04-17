using System.Data;
using api.Dtos;
using api.Dtos.Post;
using api.Models;
using Dapper;

namespace api.Repositories.PostsRepository
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDbConnection _connection;

        public PostsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Post> AddPost(Post post)
        {
            var sql = "INSERT INTO posts (title, body, imageUrl, userId) VALUES (@title, @body, @imageUrl, @userId); SELECT last_insert_rowid()";

            var parameters = new DynamicParameters();
            parameters.Add("title", post.Title, DbType.String);
            parameters.Add("body", post.Body, DbType.String);
            parameters.Add("imageUrl", post.ImageUrl, DbType.String);
            parameters.Add("userId", post.UserId, DbType.Int32);

            var createdPostId = await _connection.QuerySingleAsync<int>(sql, parameters);

            return new Post
            {
                Id = createdPostId,
                Title = post.Title,
                Body = post.Body,
                ImageUrl = post.ImageUrl
            };
        }

        public async Task DeletePost(int id)
        {
            var sql = "DELETE FROM posts WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPosts(int userId)
        {
            var sql = "SELECT title, body, username FROM posts JOIN users ON users.id = posts.userid WHERE userId = @userId";
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId, DbType.Int32);
            var posts = await _connection.QueryAsync<PostResponseDto>(sql, parameters);
            return posts;
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPosts()
        {
            var sql = "SELECT title, body, username FROM posts JOIN users ON users.id = posts.userid";
            var posts = await _connection.QueryAsync<PostResponseDto>(sql);
            return posts;
        }

        public async Task<PostResponseDto?> GetPost(int id)
        {
            var sql = "SELECT * FROM posts WHERE id = @id JOIN users ON users.id = posts.userid";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            var post = await _connection.QuerySingleOrDefaultAsync<PostResponseDto>(sql, parameters);
            return post;
        }
    }
}