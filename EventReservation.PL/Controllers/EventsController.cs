using AutoMapper;
using EventReservation.BLL.InterFaces;
using EventReservation.BLL.Repositories;
using EventReservation.DAL.Models;
using EventReservation.PL.Helper;
using EventReservation.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReservation.PL.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventsController(IEventRepository eventRepository,  IMapper mapper )
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
          
        }
        // GET: HomeController1
        public async Task<IActionResult> Index(string searchInput)
        {
            var events = Enumerable.Empty<Events>();
            if (string.IsNullOrEmpty(searchInput))
            {
                events = await  _eventRepository.GetAll();
            }
            else
            {
                events = await _eventRepository.GetByName(searchInput);
            }

            var result = _mapper.Map<IEnumerable<EventsViewModel>>(events);

            return View(result);
        }



        // GET: HomeController1/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventsViewModel model)
        {

            model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
            var _event = _mapper.Map<Events>(model);
            var count = _eventRepository.Add(_event);
             if(count > 0 )
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
                
        
        }
        
        public async Task<IActionResult> Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
            {
                return BadRequest();//400
            }
            var events = await _eventRepository.Get(Id.Value);

            var eventViewModel = _mapper.Map<EventsViewModel>(events);

            if (events is null)
            {
                return NotFound();//404
            }


            return View(ViewName, eventViewModel);
        }
        [HttpGet]
        public Task<IActionResult> Edit(int? Id)
        {
            return Details(Id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Edit([FromRoute] int? id, EventsViewModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            if (model.ImageName is not null)
            {
                DocumentSettings.DeleteFile(model.ImageName, "images");
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

            }
            else
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
            }

                var _event = _mapper.Map<Events>(model);
                var count = _eventRepository.Update(_event);
                if (count > 0) { return RedirectToAction(nameof(Index)); }
                else { return View(model); }
            
            
        }
        [HttpGet]
        public Task<IActionResult> Delete(int? Id)
        {
            return Details(Id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Delete([FromRoute] int? id, EventsViewModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            var events = _mapper.Map<Events>(model);
            if (ModelState.IsValid)//server side validation
            {
                int count =  _eventRepository.Delete(events);
            if(count > 0)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                
                
            }

            return View(model);
        }

       
    }
}

