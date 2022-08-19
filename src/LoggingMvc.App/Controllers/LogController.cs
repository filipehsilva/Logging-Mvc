using AutoMapper;
using LoggingMvc.App.Utils;
using LoggingMvc.App.ViewModels;
using LoggingMvc.Business.Interfaces;
using LoggingMvc.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoggingMvc.App.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(IMapper mapper, 
                             ILogService logService,
                             INotifier notifier) : base(notifier)
        {
            _mapper = mapper;
            _logService = logService;
        }

        [Route("logs")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            await Audit("Page Index");

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewData["LogTypeSortParam"] = String.IsNullOrEmpty(sortOrder) ? "logType_desc" : "";
            ViewData["DescriptionSortParam"] = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            var logs = _mapper.Map<IEnumerable<LogViewModel>>(await _logService.GetAll());

            if (!String.IsNullOrEmpty(searchString))
                logs = await Search(logs, searchString);
            else
                logs = await ListSortOrder(logs, sortOrder);

            int pageSize = 10;

            return View(await PaginatedList<LogViewModel>.CreateAsync(logs, pageNumber ?? 1, pageSize));
        }

        [Route("log/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var logViewModel = await GetLogById(id);

            if (logViewModel == null)
            {
                return NotFound();
            }

            await Audit("Page Details");

            return View(logViewModel);
        }

        [Route("new-log")]
        public IActionResult Create()
        {
            ViewBag.LogType = LogType();
            return View();
        }

        [HttpPost]
        [Route("new-log")]
        public async Task<IActionResult> Create([Bind("Id, Date, Type, Description")] LogViewModel logViewModel)
        {
            if (!ModelState.IsValid) return View(logViewModel);

            var log = _mapper.Map<Log>(logViewModel);
            await _logService.Add(log);

            if (!IsValid()) return View(logViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("log")]
        public async Task<IActionResult> CreateFromWebServices([FromBody] LogViewModel logViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var log = _mapper.Map<Log>(logViewModel);
            await _logService.Add(log);

            if (!IsValid()) return BadRequest(logViewModel);

            return Ok(log);
        }

        [Route("edit-log/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.LogType = LogType();
            var logViewModel = await GetLogById(id);

            if (logViewModel == null)
                return NotFound();

            return View(logViewModel);
        }

        [HttpPost]
        [Route("edit-log/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, LogViewModel logViewModel)
        {
            if (id != logViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(logViewModel);

            var log = _mapper.Map<Log>(logViewModel);
            await _logService.Update(log);

            if (!IsValid()) return View(await GetLogById(id));

            return RedirectToAction("Index");
        }

        private async Task<LogViewModel> GetLogById(Guid id)
        {
            return _mapper.Map<LogViewModel>(await _logService.GetById(id));
        }

        private List<SelectListItem> LogType()
        {
            List<SelectListItem> logs = new()
            {
                new SelectListItem { Value = "Error", Text = "Error"},
                new SelectListItem { Value = "Info", Text = "Info"},
                new SelectListItem { Value = "Warn", Text = "Warn"}
            };

            return logs;
        }

        private async Task<IEnumerable<LogViewModel>> ListSortOrder(IEnumerable<LogViewModel> logs, string sortOrder)
        {
            return logs = sortOrder switch
            {
                "date" => logs.OrderBy(l => l.Date),
                "logType_desc" => logs.OrderByDescending(l => l.Type),
                "description_desc" => logs.OrderByDescending(l => l.Description),
                _ => logs.OrderByDescending(l => l.Date),
            };
        }

        private async Task<IEnumerable<LogViewModel>> Search(IEnumerable<LogViewModel> logs, string search)
            => logs.Where(l => l.Type.ToLower().Contains(search.ToLower()) || l.Description.ToLower().Contains(search.ToLower()));
        private async Task Audit(string message)
        {
            ModelState.Clear();
            var newLog = new LogViewModel { Type = "Info", Description = $"{message}" };
            await Create(newLog);
        }
    }
}
