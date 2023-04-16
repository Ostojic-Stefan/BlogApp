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