namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Posts;
using WebApi.Services;
using WebApi.Entities;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private IPostService _postService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public PostController(
        IPostService postService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _postService = postService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    //[AllowAnonymous]
    //[HttpPost("authenticate")]
    //public IActionResult Authenticate(AuthenticateRequest model)
    //{
    //    var response = _userService.Authenticate(model);
    //    return Ok(response);
    //}

    //[AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(PostRequest model)
    {
        var id = ((User)HttpContext.Items["User"]).Id; // Få inloggad användare + Använd HttpContext
        _postService.Register(model,id);
        return Ok(new { message = "Post successful" });
    }
    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _postService.GetAll();
        return Ok(users);
    }
    [HttpGet("postByUser")]
    public IActionResult GetAllFromUser()
    {
        var id = ((User)HttpContext.Items["User"]).Id;
        var posts = _postService.GetAllByUser(id);

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _postService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PostUpdateRequest model)
    {
        _postService.Update(id, model);
        return Ok(new { message = "Post updated successfully", status = 200 });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _postService.Delete(id);
        return Ok(new { message = "Post deleted successfully" });
    }
}