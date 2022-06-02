namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Posts;
using WebApi.Models.Users;

public interface IPostService
{
    //AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<Post> GetAll();
    IEnumerable<Post> GetAllByUser(int id);
    Post GetById(int id);
    
    void Register(PostRequest model, int id);
    void Update(int id, PostUpdateRequest model);
    void Delete(int id);
}

public class PostService : IPostService
{
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public PostService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    //public AuthenticateResponse Authenticate(AuthenticateRequest model)
    //{
    //    var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

    //    // validate
    //    if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
    //        throw new AppException("Username or password is incorrect");

    //    // authentication successful
    //    var response = _mapper.Map<AuthenticateResponse>(user);
    //    response.Token = _jwtUtils.GenerateToken(user);
    //    return response;
    //}

    public IEnumerable<Post> GetAll()
    {
        return _context.Posts;
    }

    public Post GetById(int id)
    {
        return getPost(id);
    }

    public void Register(PostRequest model, int id)
    {
        // validate
        if (model.Content.Length < 1)
            throw new AppException("Post is empty");

        // map model to new user object
        var post = _mapper.Map<Post>(model);
        post.Author = _context.Users.FirstOrDefault(x => x.Id == id);
        // hash password
        //user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void Update(int id, PostUpdateRequest model)
    {
        var post = getPost(id);

        // validate
        if (model.Content.Length < 1)
            throw new AppException("Post is empty");

        //// hash password if it was entered
        //if (!string.IsNullOrEmpty(model.Password))
        //    user.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, post);
        _context.Posts.Update(post);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var post = getPost(id);
        _context.Posts.Remove(post);
        _context.SaveChanges();
    }

    // helper methods

    private Post getPost(int id)
    {
        var post = _context.Posts.Find(id);
        if (post == null) throw new KeyNotFoundException("Post not found");
        return post;
    }

    IEnumerable<Post> IPostService.GetAllByUser(int id)
    {
        var posts = _context.Posts.Where(x => x.Author.Id == id).ToList();
        return posts;
    }
}