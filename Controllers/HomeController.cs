using MediatR;
using Microsoft.AspNetCore.Mvc;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.Client.Create;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.Client.Delete;
using SzkolenieTechniczne2.AutoSerwis.Domain.Command.RepairOrders.Register;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetAllClientsQuery;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetClientCategories;
using SzkolenieTechniczne2.AutoSerwis.Domain.Query.Client.GetClientQuery;
using AutoSerwis.Mvc.UI.Models;

namespace AutoSerwis.Mvc.UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /Client
        public async Task<IActionResult> Index()
        {
            var clients = await _mediator.Send(new GetAllClientsQuery());
            return View(clients);
        }

        // GET /Client/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetClientCategoriesQuery());
            var model = new CreateClientViewModel { Categories = categories };
            return View(model);
        }

        // POST /Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _mediator.Send(new GetClientCategoriesQuery());
                return View(model);
            }

            var command = new CreateClientCommand(model.Name, model.RegistrationYear, model.ServiceDuration, model.ClientCategoryId);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Klient został dodany pomyślnie!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.Message);

            model.Categories = await _mediator.Send(new GetClientCategoriesQuery());
            return View(model);
        }

        // GET /Client/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var client = await _mediator.Send(new GetClientQuery(id));
            if (client == null)
                return NotFound();

            var vm = new ClientDetailsViewModel
            {
                Client = client,
                RepairOrderModel = new RegisterRepairOrderViewModel { ClientId = id }
            };
            return View(vm);
        }

        // POST /Client/AddRepairOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRepairOrder([Bind(Prefix = "RepairOrderModel")] RegisterRepairOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _mediator.Send(new GetClientQuery(model.ClientId));
                var vm = new ClientDetailsViewModel
                {
                    Client = client!,
                    RepairOrderModel = model
                };
                return View("Details", vm);
            }

            var command = new RegisterRepairOrderCommand(model.ClientId, model.RepairOrderDate);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                TempData["RepairOrderSuccess"] = "Zlecenie naprawy zostało dodane pomyślnie!";
                return RedirectToAction(nameof(Details), new { id = model.ClientId });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.Message);

            var clientDetails = await _mediator.Send(new GetClientQuery(model.ClientId));
            var detailsVm = new ClientDetailsViewModel
            {
                Client = clientDetails!,
                RepairOrderModel = model
            };
            return View("Details", detailsVm);
        }

        // POST /Client/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            await _mediator.Send(new DeleteClientCommand(id));
            TempData["Success"] = "Klient został usunięty.";
            return RedirectToAction(nameof(Index));
        }
    }
}