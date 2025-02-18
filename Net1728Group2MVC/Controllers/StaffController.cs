using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Controllers
{
    public class StaffController : BaseController
    {
        public StaffController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            // Lấy thông tin người dùng từ session
            var userJson = HttpContext.Session.GetString("User");
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");

            if (string.IsNullOrEmpty(userJson) || isAuthenticated != "True")
            {
                return RedirectToAction("Login", "Account");  // Nếu không tìm thấy thông tin người dùng hoặc chưa đăng nhập
            }

            // Giải mã thông tin người dùng từ chuỗi JSON
            var user = JsonConvert.DeserializeObject<AccountModel>(userJson);

            // Sử dụng AccountId từ user để tìm thông tin tài khoản trong DB
            var account = await _accountService.GetAccountByIdAsync((short)user.AccountId);

            if (account == null)
            {
                return NotFound();  // Nếu không tìm thấy tài khoản, trả về lỗi 404
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
                return RedirectToAction("News");
            }

            // Nếu có lỗi validation, hiển thị lại trang edit với thông tin đã nhập
            return View(updatedAccount);
        }

    }
}

