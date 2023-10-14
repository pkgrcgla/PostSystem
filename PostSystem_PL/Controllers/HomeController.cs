using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostSystem_BL.InterfacesOfManagers;
using PostSystem_EL.Entities;
using PostSystem_EL.IdentityModels;
using PostSystem_EL.ViewModels;
using PostSystem_PL.Models;
using System.Diagnostics;

namespace PostSystem_PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserPostManager _userPostManager;
        private readonly IPostMediaManager _postMediaManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IUserPostManager userPostManager, IPostMediaManager postMediaManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _userPostManager = userPostManager;
            _postMediaManager = postMediaManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var posts = _userPostManager.GetAll(x => !x.IsDeleted).Data.OrderByDescending(x => x.InsertedDate).ToList();
            var model=_mapper.Map<List<PostIndexVM>>(posts);
            foreach(var post in model) 
            {
                var media = _postMediaManager.GetAll(x => x.PostId == post.Id).Data;
                post.PostMedias = new List<PostMediaDTO>();
                foreach(var item in media) 
                {
                    post.PostMedias.Add(item);
                }


            }
            return View(model);
        }
        [Authorize]
        public IActionResult PostIndex()
        {
            //useridyi sayfaya gönderelim böylece adres eklemede useridyi metoda aktarabiliriz
            var username = User.Identity?.Name;
            var user = _userManager.FindByNameAsync(username).Result;
            PostIndexVM model = new PostIndexVM();
            model.PostPictures = new List<IFormFile>();
            model.UserId = user.Id;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostIndex(PostIndexVM model)
        {
            try 
            {
                if(!ModelState.IsValid)
                {
                _logger.LogInformation($"Home/PostIndex girilen post: {JsonConvert.SerializeObject(model)}");
                ModelState.AddModelError("", "Lütfen bilgileri eksiksiz giriniz.");
                return View(model);
                }

                var post = _mapper.Map<UserPostDTO>(model);
                post.InsertedDate= DateTime.Now;
                var result = _userPostManager.Add(post);
                if (!result.IsSuccess)
                {
                    _logger.LogError($"Home/PostIndex post model: {JsonConvert.SerializeObject(model)}");
                    ModelState.AddModelError("", "Post kaydedilemedi!");
                    return View(model);
                }
                // resimleri eklenebilir.
                if (model.PostPictures != null) 
                {
                        foreach (var item in model.PostPictures)
                        {
                            if (item.ContentType.Contains("image") && item.Length > 0)
                            {
                                string fileName = $"{item.FileName.Substring(0, item.FileName.IndexOf('.'))}-{Guid.NewGuid().ToString().Replace("-", "")}";

                                string uzanti = Path.GetExtension(item.FileName);

                                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/PostPictures/{fileName}{uzanti}");

                                string directoryPath =
                                   Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/PostPictures/");

                                if (!Directory.Exists(directoryPath))
                                    Directory.CreateDirectory(directoryPath);

                                using var stream = new FileStream(path, FileMode.Create);

                                item.CopyTo(stream);
                                PostMediaDTO p = new PostMediaDTO()
                                {
                                    PostId = (int)result.Data.Id,
                                    MediaPath = $"/PostPictures/{fileName}{uzanti}"
                                };
                                _postMediaManager.Add(p);

                            }
                        } 
                }
                TempData["PostIndexSuccessMsg"] = "Post attınız!";
                _logger.LogInformation($"Home/PostIndex atılan Post: {JsonConvert.SerializeObject(model)}");
                return RedirectToAction("PostIndex", "Home");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"HATA: Home/PostIndex post model:{JsonConvert.SerializeObject(model)}");
                ModelState.AddModelError("", "Beklenmedik bir sorun oluştu!");
                return View(model);
            }
            return View();
        }


    }
}