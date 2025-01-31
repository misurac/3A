﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? page, int? categoryFilterApplied, int? typeFilterApplied)
        {
            var itemsOnPage = 5;

            var events = await _service.GetEventItemsAsync(page ?? 0, itemsOnPage, categoryFilterApplied, typeFilterApplied);

            var vm = new EventIndexViewModel
            {
                EventItems = events.Data,
                Types = await _service.GetEventTypesAsync(),
                Categories = await _service.GetCategoriesAsync(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = events.PageSize,
                    TotalItems = events.Count,
                    TotalPages = (int)Math.Ceiling((decimal)events.Count / itemsOnPage)
                },
                CategoryFilterApplied = categoryFilterApplied ?? 0,
                TypeFilterApplied = typeFilterApplied ?? 0
            };

            return View(vm);
        }

        //[Authorize]
        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";


        //    return View();
        //}
    }
}