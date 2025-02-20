﻿using AutoMapper;
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
        private readonly IReportService _reportService;


        public AdminController(IAccountService accountService, IMapper mapper, IReportService reportService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _reportService = reportService;
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
            if (string.IsNullOrWhiteSpace(account.AccountPassword))
            {
                TempData["ErrorMessage"] = "Password is required for new accounts.";
                return RedirectToAction("Account");
            }

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
                TempData["SuccessMessage"] = "Account created successfully!";
                return RedirectToAction("Account");
            }

            TempData["ErrorMessage"] = "Invalid input data!";
            return RedirectToAction("Account");
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
            var existingAccount = await _accountService.GetAccountByIdAsync(account.AccountId ?? 0);
            if (existingAccount == null)
            {
                return NotFound();
            }

            var existingEmailAccount = await _accountService.GetAccountByEmailAsync(account.AccountEmail);
            if (existingEmailAccount != null && existingEmailAccount.AccountId != account.AccountId)
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
                    AccountRole = account.AccountRole,
                    AccountPassword = !string.IsNullOrWhiteSpace(account.AccountPassword)
                        ? account.AccountPassword 
                        : existingAccount.AccountPassword
                };

                await _accountService.UpdateAccountAsync(systemAccountVM);
                TempData["SuccessMessage"] = "Account updated successfully!";
                return RedirectToAction("Account");
            }

            return View(account);
        }


        [HttpPost]
        public async Task<IActionResult> DisableAccount(short id)
        {
            Console.WriteLine($"Disabling account with ID: {id}");
            await _accountService.DisableAccount(id);
            TempData["SuccessMessage"] = "Account disabled successfully!";
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

        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Header = "Report Management";

            if (!startDate.HasValue || !endDate.HasValue)
            {
                startDate = DateTime.Today.AddDays(-30);
                endDate = DateTime.Today;
            }

            var reportDTO = await _reportService.GenerateReport(startDate.Value, endDate.Value);

            var viewModel = new ReportModel
            {
                ReportPeriod = $"{reportDTO.StartDate:dd/MM/yyyy} - {reportDTO.EndDate:dd/MM/yyyy}",
                TotalNews = reportDTO.TotalNews,
                StatusReports = reportDTO.NewsByStatus.Select(s => new StatusReport
                {
                    Status = s.Key,
                    Count = s.Value
                }).ToList(),
                CategoryReports = reportDTO.NewsByCategory.Select(c => new CategoryReport
                {
                    CategoryName = c.Key,
                    Count = c.Value
                }).ToList(),
                CreatorReports = reportDTO.NewsByCreator.Select(cr => new CreatorReport
                {
                    CreatorName = cr.Key,
                    CreatorEmail = cr.Value.Email,
                    Count = cr.Value.Count   
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
