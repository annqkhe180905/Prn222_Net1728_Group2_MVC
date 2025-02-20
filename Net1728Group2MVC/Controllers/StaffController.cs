using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        public StaffController(ICategoryService categoryService, IMapper mapper, IAccountService accountService, ITagService tagServcie, INewsArticleService newsArticleService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _accountService = accountService;
            _tagService = tagServcie;
            _newsArticleService = newsArticleService;
        }

        public async Task<IActionResult> News(string? search)
        {
            ViewBag.Header = "News Management";
            var categories = await _categoryService.GetAllAsync(null) ?? new List<CategoryVM>();
            var tags = await _tagService.GetAllTagsAsync(null) ?? new List<TagVM>();
            var newsArticles = await _newsArticleService.GetAllArticle(search);

            var viewModel = new NewsModel
            {
                Categories = categories,
                Tags = tags,
                NewsArticles = newsArticles
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Category(string? search)
        {
            ViewBag.Header = "Category Management";
            var categoryModal = new CategoryModal()
            {
                Categories = await _categoryService.GetAllAsync(search)
            };

            return View(categoryModal);
        }
        public async Task<IActionResult> Profile()
        {
            ViewBag.Header = "Profile Management";
            var userJson = HttpContext.Session.GetString("User");
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");

            if (string.IsNullOrEmpty(userJson) || isAuthenticated != "True")
            {
                return RedirectToAction("Login", "Account");  
            }

            var user = JsonConvert.DeserializeObject<AccountModel>(userJson);

            var account = await _accountService.GetAccountByIdAsync((short)user.AccountId);

            if (account == null)
            {
                return NotFound();
            }

            // Tạo model để truyền vào view với dữ liệu lấy từ DB
            var model = new AccountModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword // Lưu ý: xử lý mật khẩu an toàn
            };

            return View(model);
        }

        public async Task<IActionResult> GetAccountById(short accountId)
        {
            // Lấy thông tin tài khoản từ service bằng accountId
            var account = await _accountService.GetAccountByIdAsync(accountId);

            // Nếu không tìm thấy tài khoản, trả về lỗi hoặc trang khác
            if (account == null)
            {
                return NotFound();
            }

            // Chuyển dữ liệu từ SystemAccount về AccountModel để truyền vào view
            var accountModel = new AccountModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword // Cẩn thận khi hiển thị mật khẩu
            };

            return View(accountModel);
        }


        private readonly IAccountService _accountService;

     

        // GET: Edit Profile
        [HttpGet]
        public async Task<IActionResult> EditProfile(short accountId)
        {


            // Lấy thông tin tài khoản từ AccountService
            var account = await _accountService.GetAccountByIdAsync(accountId);



            // Nếu không tìm thấy tài khoản, có thể hiển thị thông báo lỗi
            if (account == null)
            {
                return NotFound();
            }

            var accountVM = new AccountModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword
            };

            return View(accountVM);
        }

        // POST: Save Profile Changes
        [HttpPost]
        public async Task<IActionResult> EditProfile(AccountModel updatedAccount)
        {
            if (ModelState.IsValid)
            {
                // Chuyển AccountModel thành SystemAccountVM
                var systemAccountVM = new SystemAccountVM
                {
                    AccountId = (short)updatedAccount.AccountId,
                    AccountName = updatedAccount.AccountName,
                    AccountEmail = updatedAccount.AccountEmail,
                    AccountPassword = updatedAccount.AccountPassword // Lưu ý xử lý mật khẩu (hashing, encryption)
                };

                // Gọi service để cập nhật thông tin
                await _accountService.UpdateProfile(systemAccountVM);

                // Sau khi cập nhật, chuyển hướng về trang profile
                return RedirectToAction("Profile");
            }

            // Nếu có lỗi validation, hiển thị lại trang edit với thông tin đã nhập
            return View(updatedAccount);
        }
        public async Task<IActionResult> NewsTag(string? search)
        {
            ViewBag.Header = "News Tag Management";
            var tagModel = new TagModel { 
                Tag = await _tagService.GetAllTagsAsync(search)
            };
            return View(tagModel);
        }

    }
}

