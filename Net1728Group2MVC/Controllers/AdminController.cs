using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AdminController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<IActionResult> AccountAsync()
        {
            try
            {
                var accounts = await _accountService.GetAllAccountsAsync();

                var accountModels = accounts.Select(account => new AccountModel
                {
                    AccountId = account.AccountId,
                    AccountName = account.AccountName,
                    AccountEmail = account.AccountEmail,
                    AccountPassword = account.AccountPassword,
                    AccountRole= account.AccountRole
                }).ToList();

                ViewBag.Header = "Account Management";
                return View(accountModels);
            }
            catch (Exception ex)
            {
                return View("Error", new { message = "An error occurred while fetching the accounts." });
            }
        }


        

        [HttpPost]
        public async Task<IActionResult> Create(AccountModel account)
        {

            if (!await _accountService.IsEmailUniqueAsync(account.AccountEmail))
            {
                TempData["ErrorMessage"] = "Email already exists!";
                return RedirectToAction("Account");
            }


            if (ModelState.IsValid)
            {
                var systemAccountVM = new SystemAccountVM
                {

                    AccountName = account.AccountName,
                    AccountEmail = account.AccountEmail,
                    AccountPassword = account.AccountPassword,
                    AccountRole = account.AccountRole 
                };

                await _accountService.CreateAccountAsync(systemAccountVM);
                return RedirectToAction("Account");
            }
            return RedirectToAction("r");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(short id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            var accountVM = new AccountModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword,
                AccountRole = account.AccountRole
            };
            if (account == null)
            {
                return NotFound();
            }
            return View(accountVM);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(AccountModel account)
        {

            var existingAccount = await _accountService.GetAccountByEmailAsync(account.AccountEmail);
            if (existingAccount != null && existingAccount.AccountId != account.AccountId)
            {
                TempData["ErrorMessage"] = "Email already in use by another account!";
                return RedirectToAction("Account");
            }

            if (ModelState.IsValid)
            {
                var systemAccountVM = new SystemAccountVM
                {
                    AccountId = (short)account.AccountId,
                    AccountName = account.AccountName,
                    AccountEmail = account.AccountEmail,
                    AccountPassword = account.AccountPassword,
                    AccountRole = account.AccountRole 
                };


                await _accountService.UpdateAccountAsync(systemAccountVM);
                return RedirectToAction("Account");
            }
            return View(account);
        }


        [HttpPost]
        public async Task<IActionResult> DisableAccount(short id)
        {
            Console.WriteLine($"Disabling account with ID: {id}");
            await _accountService.DisableAccount(id);
            return RedirectToAction("Account");
        }





        public IActionResult Search(string name, int? role)
        {
            var accounts = _accountService.SearchAccounts(name, role);
            var accountModels = accounts.Select(account => new AccountModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword,
                AccountRole = account.AccountRole
            }).ToList();
            return View("Account", accountModels);
        }
        public IActionResult Report()
        {
            ViewBag.Header = "Report Management";

            return View();
        }
    }
}
